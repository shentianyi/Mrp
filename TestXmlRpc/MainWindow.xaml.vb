Imports CookComputing.XmlRpc
Imports OdooWebSvc

Class MainWindow
    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        Dim odooProxyCommon As IOdooCommon = XmlRpcProxyGen.Create(Of IOdooCommon)
        Dim odooProxyObj As IOdooObject = XmlRpcProxyGen.Create(Of IOdooObject)
        odooProxyCommon.Url = "http://42.121.111.38:8000/xmlrpc/2/common"
        odooProxyObj.Url = "http://42.121.111.38:8000/xmlrpc/2/object"
        Dim userId As Integer = odooProxyCommon.authenticate("cztek", "admin", "123456@", {})
        MsgBox("login Ok:" & userId)
        Dim currentdate As DateTime
        DateTime.TryParse("2016-04-19 08:47:19", currentdate)
        Dim a() As String = {"create_date", "<=", currentdate}
        Dim b() As Array = {a}
        'Dim partner() As Integer = odooProxyObj.Search("cztek", userId, "123456@", "res.partner", "search", {a})
        Dim objs As XmlRpcStruct() = odooProxyObj.SearchAndRead("cztek", userId, "123456@", "mrp.production", "search_read", {b}, New XmlRpcStruct)

    End Sub
End Class
