
Imports System.Messaging
Imports log4net
Imports System.IO

Module Module1

    Sub Main()


        Try
            Dim bizLog As ILog = LogManager.GetLogger("BizLog")
            log4net.Config.XmlConfigurator.ConfigureAndWatch(New FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logConfig.xml")))
            bizLog.Info("MRP任务开始下达")
            If Not MessageQueue.Exists(My.Settings.queuePath) Then
                MessageQueue.Create(My.Settings.queuePath, False)
            End If
            Dim qu As MessageQueue = New MessageQueue(My.Settings.queuePath)
            Dim msg As Message = New Message
            msg.Body = "CzMrp"
            msg.Formatter = New XmlMessageFormatter({GetType(String)})
            qu.Send(msg)
            bizLog.Info("MRP任务下达完成")
        Catch ex As Exception
            Dim bizLog As ILog = LogManager.GetLogger("BizLog")
            bizLog.Fatal("MRP任务执行失败", ex)
        End Try
        Console.WriteLine("MRP任务已经下达,按任意键退出")
        Console.ReadLine()
    End Sub

End Module
