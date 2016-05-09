Public Class Commands
    Dim lstCommands As New List(Of Command)
    Public Sub RegisterCommand(ByVal Commando As String, ByVal Executor As String)
        Dim Cmd As New Command
        Cmd.SetCommando(Commando)
        Cmd.SetExecutor(Executor)
        lstCommands.Add(Cmd)
    End Sub
#Region "Instances"
    Private FileManager As New FileManager
    Private ProcessManager As New ProcessManager
#End Region
    Public Sub ExecuteCommand(ByVal Commando As String, ByVal Args() As String)
        For Each Cmd As Command In lstCommands
            If Cmd.GetCommando = Commando Then
                Select Case Cmd.GetExecutor
                    Case "rfm-SetAttributes"
                        FileManager.SetAttributes(Args(0), Args(1))
                    Case "rfm-RenameDirectory"
                        FileManager.RenameDirectory(Args(0), Args(1))
                    Case "rfm-RenameFile"
                        FileManager.RenameFile(Args(0), Args(1))
                    Case "rfm-ReadTextFile"
                        Dim Response As String = FileManager.ReadTextFile(Args(0))
                    Case "rfm-DeleteDirectory"
                        FileManager.DeleteDirectory(Args(0))
                    Case "rfm-DeleteFile"
                        FileManager.DeleteFile(Args(0))
                    Case "rfm-GetDirectorys/rfm-GetFiles"
                        Dim Response As String = FileManager.GetDirectorys(Args(0)) &
                                                FileManager.GetFiles(Args(1))
                    Case "rpm-StopProcess"
                        ProcessManager.StopProcess(Args(0))
                    Case "rpm-NewProcess"
                        ProcessManager.NewProcess(Args(0))
                    Case "rpm-GetProcesses"
                        Dim Response As String = ProcessManager.GetProcesses()
                End Select
            End If
        Next
    End Sub
End Class
