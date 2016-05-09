Imports System.IO
Imports System.Net
Imports System.Text
Public Class Network
    Private Shared UserArgent As String = "irAT-Client"
    Public Shared Function SendRequest(ByVal Method As String, ByVal RequestUrl As String, ByVal Values As String) As String
        Dim Data As Byte() = Encoding.UTF8.GetBytes(Values)
        Try
            Dim Request As HttpWebRequest = WebRequest.Create(RequestUrl)
            With Request
                .ContentType = "application/x-www-form-urlencoded"
                .ContentLength = Data.Length
                .Method = Method
                .UserAgent = Network.UserArgent
            End With
            Dim RequestStream As Stream = Request.GetRequestStream
            With RequestStream
                .Write(Data, 0, Data.Length)
                .Close()
            End With
            Dim Response As HttpWebResponse = Request.GetResponse
            Dim ResponseStreamReader As New StreamReader(Response.GetResponseStream)
            Return ResponseStreamReader.ReadToEnd
        Catch : End Try
    End Function
    Public Shared Function GetResponse(ByVal RequestUrl As String)
        Try
            Dim Request As HttpWebRequest = WebRequest.Create(RequestUrl)
            Request.UserAgent = Network.UserArgent
            Dim Response As HttpWebResponse = Request.GetResponse
            Dim ResponseStreamReader As New StreamReader(Response.GetResponseStream)
            Return ResponseStreamReader.ReadToEnd
        Catch : End Try
    End Function
End Class
