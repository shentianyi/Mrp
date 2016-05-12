Imports Mrp.MrpMain

Class MainWindow
    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        Dim mrp As Mrp.MrpMain = New Mrp.MrpMain("Data Source=VM08;Initial Catalog=Mrp;User ID=sa;Password=brilliantech123@")
        mrp.Init()
        MsgBox("init done")
    End Sub

    Private Sub button1_Click(sender As Object, e As RoutedEventArgs) Handles button1.Click
        Dim mrp As Mrp.MrpMain = New Mrp.MrpMain("Data Source=VM08;Initial Catalog=Mrp;User ID=sa;Password=brilliantech123@")
        mrp.GenerateGrossMps()
        MsgBox("done")
    End Sub

    Private Sub button2_Click(sender As Object, e As RoutedEventArgs) Handles button2.Click
        Dim mrp As Mrp.MrpMain = New Mrp.MrpMain("Data Source=VM08;Initial Catalog=Mrp;User ID=sa;Password=brilliantech123@")
        mrp.GenerateNetMps()
        MsgBox("done")
    End Sub

    Private Sub button3_Click(sender As Object, e As RoutedEventArgs) Handles button3.Click
        Dim mrp As Mrp.MrpMain = New Mrp.MrpMain("Data Source=VM08;Initial Catalog=Mrp;User ID=sa;Password=brilliantech123@")
        mrp.GenerateGrossMrp()
        MsgBox("done")
    End Sub

    Private Sub button4_Click(sender As Object, e As RoutedEventArgs) Handles button4.Click
        Dim mrp As Mrp.MrpMain = New Mrp.MrpMain("Data Source=VM08;Initial Catalog=Mrp;User ID=sa;Password=brilliantech123@")
        mrp.GenerateNetMrp()
        MsgBox("done")
    End Sub

    Private Sub button5_Click(sender As Object, e As RoutedEventArgs) Handles button5.Click
        Dim mrp As Mrp.MrpMain = New Mrp.MrpMain("Data Source=VM08;Initial Catalog=Mrp;User ID=sa;Password=brilliantech123@")
        mrp.GenerateResult()
        MsgBox("done")
    End Sub
End Class
