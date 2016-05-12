Imports KskPlugInSharedObject
Imports System.IO.Path
Imports System.IO

Public Class MrpMain
    '1 get grassMPS source
    '2 get balance source
    '2 get timing rules
    '3 split bom - get grass MRP
    Private dbconn As String
    Private round As String

    Public Sub New(db As String)
        dbconn = db
        round = Now.ToString("yyyyMMddhhmmss")
    End Sub

    Public Sub Start()
        Try
            Init()
            GenerateGrossMps()
            GenerateNetMps()
            GenerateGrossMrp()
            GenerateNetMrp()
            GenerateResult()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub Init()
        'clear the working table
        Dim db As MrpDataDataContext = New MrpDataDataContext(dbconn)

        Dim clearScript As String = File.ReadAllText(My.Application.Info.DirectoryPath & Combine("\dbscripts", "clearData.txt"))

        Try
            If db.DatabaseExists Then
                db.ExecuteCommand(clearScript)
            Else
                Throw New Exception("指定数据库不存在")
            End If

        Catch ex As Exception
            Throw New Exception("重建工作数据库时出错", ex)
        End Try
    End Sub



    Public Sub DownloadOrderedPart()
        Dim db As MrpDataDataContext = New MrpDataDataContext(dbconn)
        Dim parts As List(Of String) = New List(Of String)
        parts = (From mrpPart In db.Exe_GrossMrps Select mrpPart.partId Distinct).ToList
        Dim data As ProcessData = New ProcessData
        data.Data.Add("db", dbconn)
        excuteAssembly("PO", data)
    End Sub

    Public Sub DownloadProductionPlan()
        Dim data As ProcessData = New ProcessData
        data.Data.Add("db", dbconn)
        excuteAssembly("PP", data)
    End Sub

    Public Sub DownloadPartVendor()
        Dim db As MrpDataDataContext = New MrpDataDataContext(dbconn)
        Dim parts As List(Of String) = New List(Of String)
        parts = (From mrpPart In db.Exe_NetMrps Select mrpPart.partId Distinct).ToList
        Dim data As ProcessData = New ProcessData
        data.Data.Add("db", dbconn)
        data.Data.Add("parts", parts)
        excuteAssembly("VENDOR", data)
    End Sub

    Public Sub DownloadBom()
        Dim db As MrpDataDataContext = New MrpDataDataContext(dbconn)
        Dim parts As List(Of String) = New List(Of String)

        parts = (From mrpPart In db.Exe_NetMps Select mrpPart.assemblyPartId Distinct).ToList



        Dim data As ProcessData = New ProcessData
        data.Data.Add("db", dbconn)
        data.Data.Add("parts", parts)
        excuteAssembly("BOM", data)
    End Sub

    Public Sub DownloadInventory(type As String)
        Dim db As MrpDataDataContext = New MrpDataDataContext(dbconn)
        Dim parts As List(Of String) = New List(Of String)
        If type = "MRP" Then
            parts = (From mrpPart In db.Exe_GrossMrps Select mrpPart.partId Distinct).ToList


        ElseIf type = "MPS" Then
            parts = (From mrpPart In db.Exe_GrossMps Select mrpPart.assemblyPartId Distinct).ToList
        Else
            Throw New Exception("无法启动库存下载，" & type & "无法辨认")
        End If
        Dim data As ProcessData = New ProcessData
        data.Data.Add("db", dbconn)
        data.Data.Add("parts", parts)
        excuteAssembly("INV", data)
    End Sub

    Public Sub excuteAssembly(type As String, data As ProcessData)
        Dim db As MrpDataDataContext = New MrpDataDataContext(dbconn)
        Dim exes As List(Of Sys_Plugin) = (From exers In db.Sys_Plugins Where exers.pluginType = type Select exers Order By exers.seq Ascending).ToList
        If exes.Count = 0 Then
            Throw New Exception("执行外接程序" & type & "时没有找到相应插件")
        Else
            For Each plug In exes
                Dim result As ProcessResult = ExternalProcess.Start(My.Application.Info.DirectoryPath & Combine("\Plugins", plug.assemblyFile), plug.type, plug.method, data)
                If result.ResultCode = 0 Then
                    Throw New Exception("执行插件类型" & plug.pluginType & "时出错:" & result.FormattedErrors)
                End If
            Next
        End If

    End Sub

    Public Sub GenerateGrossMps()
        DownloadProductionPlan()
        Dim db As MrpDataDataContext = New MrpDataDataContext(dbconn)
        Dim grossMpses As List(Of Exe_GrossMp) = New List(Of Exe_GrossMp)
        For Each plan In db.Data_ProductionPlans
            grossMpses.Add(New Exe_GrossMp With {.assemblyPartId =
                           plan.assemblyPartId, .bomId = plan.bomId,
                           .requiredTime = plan.time, .source = plan.planId,
                           .mrpRound = Me.round, .quantity = plan.quantity})
        Next
        db.Exe_GrossMps.InsertAllOnSubmit(grossMpses)
        db.SubmitChanges()
        db.Dispose()
        grossMpses = Nothing
    End Sub

    Public Sub GenerateNetMps()
        DownloadInventory("MPS")
        Dim db As MrpDataDataContext = New MrpDataDataContext(dbconn)
        Dim grossesId As List(Of String) = (From gro In db.Exe_GrossMps
                                            Select gro.assemblyPartId Distinct
                                         ).ToList
        Dim invs As List(Of Data_Inventory) = (From inv In db.Data_Inventories Where grossesId.Contains(inv.partId) Select inv).ToList
        Dim nets As List(Of Exe_NetMp) = New List(Of Exe_NetMp)
        invs.Sort(New Comparison(Of Data_Inventory)(Function(c, e) c.expireDate < e.expireDate))

        For Each gross As Exe_GrossMp In (From gro2 In db.Exe_GrossMps Select gro2 Order By gro2.requiredTime Ascending)
            invs.RemoveAll(Function(c) c Is Nothing)
            For Each existInv In invs
                If String.Compare(existInv.partId, gross.assemblyPartId, True) = 0 Then
                    If existInv.expireDate > gross.requiredTime Then
                        If existInv.quantity >= gross.quantity Then
                            existInv.quantity = existInv.quantity - gross.quantity
                            gross = Nothing
                            Exit For
                        Else
                            gross.quantity = gross.quantity - existInv.quantity
                            existInv = Nothing
                        End If
                    End If
                End If
            Next
            If gross IsNot Nothing Then
                Dim net As Exe_NetMp = New Exe_NetMp With {.assemblyPartId = gross.assemblyPartId,
                 .bomId = gross.bomId, .mrpRound = gross.mrpRound,
                 .requiredTime = gross.requiredTime, .source = gross.source,
                 .quantity = gross.quantity}
                nets.Add(net)
            End If
        Next
        db.Exe_NetMps.InsertAllOnSubmit(nets)
        db.SubmitChanges()
        invs = Nothing
        nets = Nothing
        db.Dispose()
    End Sub

    Public Sub GenerateGrossMrp()
        DownloadBom()
        Dim db As MrpDataDataContext = New MrpDataDataContext(dbconn)
        Dim rowMrps As List(Of Exe_GrossMrp) = New List(Of Exe_GrossMrp)
        '1,net mps one by one loop
        '2.find bom elements
        '3.calculate the raw mrps
        For Each net As Exe_NetMp In db.Exe_NetMps
            Dim boms As IQueryable(Of Data_Bom) = (From bomele _
                                                   In db.Data_Boms
                                                   Where String.Compare(
                                                       bomele.assemblyPartId, net.assemblyPartId) _
                                                       = 0 And
                                                   String.Compare(bomele.bomId, net.bomId) = 0
                                                   Select bomele)
            If boms.Count > 0 Then
                For Each bom In boms
                    rowMrps.Add(New Exe_GrossMrp With {.partId = bom.materialPartId, .quantity = net.quantity * bom.quantity, .requiredDate = net.requiredTime, .sourceDoc = net.source, .sourcePart = net.assemblyPartId})
                Next
            Else
                Throw New Exception("零件" & net.assemblyPartId & "没有找到相应的BOM")
            End If
        Next
        db.Exe_GrossMrps.InsertAllOnSubmit(rowMrps)
        db.SubmitChanges()
        rowMrps = Nothing
        db.Dispose()
    End Sub

    Public Sub GenerateNetMrp()
        ''获取减去在库库存
        ''获取减去未关闭采购订单
        ''获取减去批次过期库存
        ''获取加上安全库存额
        DownloadOrderedPart()
        DownloadInventory("MRP")

        Dim db As MrpDataDataContext = New MrpDataDataContext(dbconn)
        Dim grossesId As List(Of String) = (From gro In db.Exe_GrossMrps
                                            Select gro.partId Distinct
                                         ).ToList
        Dim invs As List(Of Data_Inventory) = (From inv In db.Data_Inventories Where grossesId.Contains(inv.partId) Select inv).ToList
        Dim ordered As List(Of Data_OrderedPart) = (From opart In db.Data_OrderedParts Where grossesId.Contains(opart.partId) Select opart).ToList
        Dim nets As List(Of Exe_NetMrp) = New List(Of Exe_NetMrp)
        invs.Sort(New Comparison(Of Data_Inventory)(Function(c, e) c.expireDate < e.expireDate))

        For Each gross As Exe_GrossMrp In (From gro2 In db.Exe_GrossMrps Select gro2 Order By gro2.requiredDate Ascending)
            invs.RemoveAll(Function(c) c Is Nothing)
            For Each existInv In invs
                If String.Compare(existInv.partId, gross.partId, True) = 0 Then
                    If existInv.expireDate > gross.requiredDate Then
                        If existInv.quantity >= gross.quantity Then
                            existInv.quantity = existInv.quantity - gross.quantity
                            gross = Nothing
                            Exit For
                        Else
                            gross.quantity = gross.quantity - existInv.quantity
                            existInv = Nothing
                        End If
                    End If
                End If
            Next
            If gross IsNot Nothing Then
                For Each existOrder In ordered
                    If String.Compare(existOrder.partId, gross.partId, True) = 0 Then
                        If existOrder.arriveTime <= gross.requiredDate Then
                            If existOrder.quantity >= gross.quantity Then
                                existOrder.quantity = existOrder.quantity - gross.quantity
                                gross = Nothing
                                Exit For
                            Else
                                gross.quantity = gross.quantity - existOrder.quantity
                                existOrder = Nothing
                            End If
                        End If
                    End If
                Next
            End If
            If gross IsNot Nothing Then
                Dim net As Exe_NetMrp = New Exe_NetMrp With {.partId = gross.partId,
                    .requiredDate = gross.requiredDate,
                    .orderDate = gross.requiredDate,
                    .quantity = gross.quantity}
                nets.Add(net)
            End If
        Next
        db.Exe_NetMrps.InsertAllOnSubmit(nets)
        db.SubmitChanges()
        db.Dispose()
        invs = Nothing
        ordered = Nothing
        nets = Nothing
    End Sub

    Public Sub GenerateResult()
        DownloadPartVendor()
        Dim db As MrpDataDataContext = New MrpDataDataContext(dbconn)
        Dim orders As List(Of Exe_MrpOrder) = New List(Of Exe_MrpOrder)
        For Each uniquePart In db.View_SumOfNetMrps
            Dim config As Data_PartVendorConfig = db.Data_PartVendorConfigs.FirstOrDefault(Function(c) String.Compare(c.partId, uniquePart.partId) = 0)
            If config Is Nothing Then
                Throw New Exception("没有找到零件" & uniquePart.partId & "与供应商的关系")
            Else
                orders.Add(New Exe_MrpOrder With {.partId = uniquePart.partId,
                                 .quantity = uniquePart.nrofquantity,
                                 .requiredDate = uniquePart.requiredDate,
                                 .vendorId = config.vendorId,
                                 .orderDate = uniquePart.requiredDate.AddDays(-config.leadTime)
                                 })

            End If
        Next
        db.Exe_MrpOrders.InsertAllOnSubmit(orders)
        db.SubmitChanges()
        orders = Nothing
        db.Dispose()
    End Sub

    Public Sub SendResult()

    End Sub
End Class
