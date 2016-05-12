Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports KskPlugInSharedObject
Imports Mrp
<TestClass> Public Class UnitTestForPlugIn
    <TestMethod> Public Sub TestInventory()
        Dim parameter As ProcessData = New ProcessData
        Dim dbconstr As String
        Dim parts As List(Of String)
        parameter.Data.Add("db", dbconstr)
        parameter.Data.Add("parts", parts)


    End Sub

<<<<<<< HEAD
    <TestMethod> Public Sub TestDb()
        Dim context As MrpDataDataContext = New MrpDataDataContext
        Dim a As IEnumerable(Of Object) = (From db In context.Exe_NetMps Select db.assemblyPartId, db.bomId)
        For Each b As Object In a
            Dim e As String = b("bomId")
            Dim f As String = b("assemblyPartId")
        Next
        Assert.IsNotNull(a)
    End Sub
=======

>>>>>>> 88b2276fcdec0521862dcc60fc1a6909bd445028

End Class
