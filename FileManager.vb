Imports System.IO
Imports System.Text
Public Class FileManager
    Private d As String = "|"
    Public Sub SetAttributes(ByVal Attribute As String, ByVal Path As String)
        If Directory.Exists(Path) Or File.Exists(Path) Then
            Try
                Select Case Attribute.ToUpper
                    Case "ARCHIVE"
                        File.SetAttributes(Path, FileAttribute.Archive)
                    Case "DIRECTORY"
                        File.SetAttributes(Path, FileAttribute.Directory)
                    Case "HIDDEN"
                        File.SetAttributes(Path, FileAttribute.Hidden)
                    Case "NORMAL"
                        File.SetAttributes(Path, FileAttribute.Normal)
                    Case "READONLY"
                        File.SetAttributes(Path, FileAttribute.ReadOnly)
                    Case "SYSTEM"
                        File.SetAttributes(Path, FileAttribute.System)
                    Case "VOLUME"
                        File.SetAttributes(Path, FileAttribute.Volume)
                End Select
            Catch : End Try
        End If
    End Sub
    Public Sub RenameDirectory(ByVal Path As String, ByVal NewDirectoryName As String)
        Try
            If Directory.Exists(Path) Then
                Dim OldPath As String = Path
                Dim OldDirectoryName As String = String.Empty
                If Not OldPath.EndsWith("\") Then
                    OldPath &= "\"
                End If
                Dim Index As Integer = OldPath.Split("\").LongLength - 1
                OldDirectoryName = OldPath.Split("\")(Index - 1)
                Dim NewDirectoryPath As String = OldPath.Replace(OldDirectoryName, NewDirectoryName)
                FileSystem.Rename(OldPath, NewDirectoryPath)
            End If
        Catch : End Try
    End Sub
    Public Sub RenameFile(ByVal Path As String, ByVal NewFileName As String)
        Try
            Dim Directory As String = IO.Path.GetDirectoryName(Path)
            If Not Directory.EndsWith("\") Then
                Directory &= "\"
            End If
            FileSystem.Rename(Path, Directory & NewFileName)
        Catch : End Try
    End Sub
    Public Function ReadTextFile(ByVal Path As String)
        Try
            Return File.ReadAllText(Path)
        Catch : End Try
    End Function
    Public Sub DeleteDirectory(ByVal Path As String)
        Try
            Directory.Delete(Path)
        Catch : End Try
    End Sub
    Public Sub DeleteFile(ByVal Path As String)
        Try
            File.Delete(Path)
        Catch : End Try
    End Sub
    Public Function GetDirectorys(ByVal Location As String) As String
        Dim Sb As New StringBuilder
        Try
            Dim Info As DirectoryInfo
            For Each Directory As String In IO.Directory.GetDirectories(Location)
                Info = New DirectoryInfo(Directory)
                'ICON NAME SIZE LAST_WRITE_TIME
                Sb.AppendLine("DIRECTORY" & d &
                              Info.Name & d &
                              String.Empty & d &
                              Info.LastWriteTime)

            Next
        Catch : End Try
        Return Sb.ToString
    End Function
    Public Function GetFiles(ByVal Location As String) As String
        Dim Sb As New StringBuilder
        Try
            Dim Info As FileInfo
            For Each File As String In IO.Directory.GetFiles(Location)
                Info = New FileInfo(File)
                'ICON NAME SIZE LAST_WRITE_TIME
                Sb.AppendLine(ImageToBase64String(File) & d &
                              Info.Name & d &
                              Info.Length / 1000 & " KB" & d &
                              Info.LastWriteTime)
            Next
        Catch : End Try
        Return Sb.ToString
    End Function
    Public Function ImageToBase64String(ByVal Path As String) As String
        Try
            Dim Base64String As String = String.Empty
            Dim imgList As New ImageList
            Dim iIcon As Icon = Icon.ExtractAssociatedIcon(Path)
            imgList.Images.Add(iIcon.ToBitmap)
            Dim ms As New MemoryStream
            imgList.Images(0).Save(ms, Imaging.ImageFormat.Png)
            Base64String = Convert.ToBase64String(ms.ToArray)
            Return Base64String
        Catch : End Try
    End Function
End Class
