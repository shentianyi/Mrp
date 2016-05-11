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
            GenerateNetMrp
            GenerateResult()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Init()
        'clear the working table
        Dim db As MrpDataDataContext = New MrpDataDataContext
        Try
            If db.DatabaseExists Then
                db.DeleteDatabase()
                If Not db.DatabaseExists Then
                    db.CreateDatabase()
                End If
            End If
        Catch ex As Exception
            Throw New Exception("重建工作数据库时出错", ex)
        End Try
    End Sub

    Private Sub DownloadData()
        downloadProductionPlan()
        downloadInventory()
        downloadBom()
        downloadPartVendor()
        downloadOrderedPart()
    End Sub

    Private Sub downloadOrderedPart()
        Throw New NotImplementedException()
    End Sub

    Private Sub downloadProductionPlan()
        Throw New NotImplementedException()
    End Sub

    Private Sub downloadPartVendor()
        Throw New NotImplementedException()
    End Sub

    Private Sub downloadBom()
        Throw New NotImplementedException()
    End Sub

    Private Sub downloadInventory(type As String)
        Throw New NotImplementedException()
    End Sub



    Private Sub GenerateGrossMps()
        downloadProductionPlan()
        Dim db As MrpDataDataContext = New MrpDataDataContext(dbconn)
        Dim grossMpses As List(Of Exe_GrossMp) = New List(Of Exe_GrossMp)
        For Each plan In db.Data_ProductionPlans
            grossMpses.Add(New Exe_GrossMp With {.assemblyPartId =
                           plan.assemblyPartId, .bomId = plan.bomId,
                           .requiredTime = plan.time, .source = plan.planId,
                           .mrpRound = Me.round})
        Next
        db.Exe_GrossMps.InsertAllOnSubmit(grossMpses)
        db.SubmitChanges()
        db.Dispose()
        grossMpses = Nothing
    End Sub

    Private Sub GenerateNetMps()
        downloadInventory("MPS")
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
        invs = Nothing
        nets = Nothing
        db.Dispose()
    End Sub

    Private Sub GenerateGrossMrp()
        downloadBom()



    End Sub

    Private Sub GenerateNetMrp()
        ''获取减去在库库存
        ''获取减去未关闭采购订单
        ''获取减去批次过期库存
        ''获取加上安全库存额
        downloadOrderedPart()
        downloadInventory("MRP")

    End Sub

    Private Sub GenerateResult()

    End Sub
End Class
