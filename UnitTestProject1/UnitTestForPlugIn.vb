Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports KskPlugInSharedObject
<TestClass> Public Class UnitTestForPlugIn
    <TestMethod> Public Sub TestInventory()
        Dim parameter As ProcessData = New ProcessData
        Dim dbconstr As String
        Dim parts As List(Of String)
        parameter.Data.Add("db", dbconstr)
        parameter.Data.Add("parts", parts)


    End Sub

End Class
