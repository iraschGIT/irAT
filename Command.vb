Public Class Command
    Private Commando As String = Nothing
    Private Executor As String = Nothing

    Public Sub SetCommando(ByVal x As String)
        Commando = x
    End Sub
    Public Function GetCommando() As String
        Return Commando
    End Function

    Public Sub SetExecutor(ByVal x As String)
        Executor = x
    End Sub
    Public Function GetExecutor() As String
        Return Executor
    End Function
End Class
