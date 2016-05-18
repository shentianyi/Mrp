Imports Mrp.MrpMain

Class MainWindow
    Dim connectStr As String = String.Empty

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        connectStr = My.Settings.DbConnect.ToString

    End Sub

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        Dim mrp As Mrp.MrpMain = New Mrp.MrpMain(connectStr)
        mrp.Init()
        MsgBox("init done")
    End Sub

    Private Sub button1_Click(sender As Object, e As RoutedEventArgs) Handles button1.Click
        Dim mrp As Mrp.MrpMain = New Mrp.MrpMain(connectStr)
        mrp.GenerateGrossMps()
        MsgBox("done")
    End Sub

    Private Sub button2_Click(sender As Object, e As RoutedEventArgs) Handles button2.Click
        Dim mrp As Mrp.MrpMain = New Mrp.MrpMain(connectStr)
        mrp.GenerateNetMps()
        MsgBox("done")
    End Sub

    Private Sub button3_Click(sender As Object, e As RoutedEventArgs) Handles button3.Click
        Dim mrp As Mrp.MrpMain = New Mrp.MrpMain(connectStr)
        mrp.GenerateGrossMrp()
        MsgBox("done")
    End Sub

    Private Sub button4_Click(sender As Object, e As RoutedEventArgs) Handles button4.Click
        Dim mrp As Mrp.MrpMain = New Mrp.MrpMain(connectStr)
        mrp.GenerateNetMrp()
        MsgBox("done")
    End Sub

    Private Sub button5_Click(sender As Object, e As RoutedEventArgs) Handles button5.Click
        Dim mrp As Mrp.MrpMain = New Mrp.MrpMain(connectStr)
        mrp.GenerateResult()
        MsgBox("done")
    End Sub

    Private Sub button6_Click(sender As Object, e As RoutedEventArgs) Handles button6.Click
        Dim mrp As Mrp.MrpMain = New Mrp.MrpMain(connectStr)
        mrp.DownloadInventory("MPS")
        MsgBox("done")
    End Sub

    Private Sub button6_Copy_Click(sender As Object, e As RoutedEventArgs) Handles button6_Copy.Click
        Dim mrp As Mrp.MrpMain = New Mrp.MrpMain(connectStr)
        mrp.DownloadInventory("MRP")
        MsgBox("done")
    End Sub

    Private Sub button7_Click(sender As Object, e As RoutedEventArgs) Handles button7.Click
        Dim mrp As Mrp.MrpMain = New Mrp.MrpMain(connectStr)
        mrp.CreateOrders()
 
        MsgBox("done")
    End Sub
End Class
