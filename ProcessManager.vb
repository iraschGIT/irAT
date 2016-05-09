Imports System.Text
Imports System.Management
Public Class ProcessManager
    Inherits FileManager
    Private d As String = "|"
    Public Function GetProcesses() As String
        Dim Sb As New StringBuilder
        For Each Process As Diagnostics.Process In Diagnostics.Process.GetProcesses
            Try
                Sb.AppendLine(ImageToBase64String(GetProcessLocation(Process.Id)) & d &
                              Process.ProcessName & d &
                              Process.Id)
            Catch : End Try
        Next
        Return Sb.ToString
    End Function
    Public Function GetProcessLocation(ByVal Id As Integer) As String
        Try
            Dim Path As String = Process.GetProcessById(Id).MainModule.FileName
            If Path IsNot Nothing Then
                Return Path
            End If
        Catch
            Dim MClass As New ManagementClass("Win32_Process")
            Dim MObjectCollection As ManagementObjectCollection = MClass.GetInstances
            For Each MObject As ManagementObject In MObjectCollection

                Dim CurrentProcessId As String = Convert.ToString(MObject.GetPropertyValue("ProcessID"))
                Dim CurrentProcessLocation As String = Convert.ToString(MObject.GetPropertyValue("ExecutablePath"))
                If CurrentProcessLocation IsNot String.Empty Then
                    Return CurrentProcessLocation
                End If
            Next
        End Try
    End Function
    Public Sub StopProcess(ByVal Id As Integer)
        Try
            Dim Process As Diagnostics.Process = Diagnostics.Process.GetProcessById(Id)
            Process.Kill()
        Catch : End Try
    End Sub
    Public Sub NewProcess(ByVal Path As String)
        Try
            Process.Start(Path)
        Catch : End Try
    End Sub
End Class
