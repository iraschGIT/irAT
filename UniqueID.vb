Imports System.Text
Imports System.Management
Imports System.Security.Cryptography
Public Class UniqueID
    Public Function Generate() As String
        Dim Sb As New StringBuilder
        With Sb
            .AppendLine(VolumeSerialNumber)
            .AppendLine(ProcessorId)
            .AppendLine(BiosVersion)
            .AppendLine(Environment.MachineName)
            .AppendLine(Environment.UserName)
        End With
        Dim md5 As MD5 = md5.Create()
        Dim Data() As Byte = md5.ComputeHash(Encoding.UTF8.GetBytes(Sb.ToString))
        Sb = Nothing
        For i As Integer = 0 To Data.Length - 1
            Sb.Append(Data(i).ToString("x2"))
        Next
        Return Sb.ToString
    End Function
    Public Function Country() As String
        Dim _Country As String = "Unknown"
        Dim Response As String = Network.GetResponse("http://freegeoip.net/json/")
        If Response Then
            Try
                Dim Qout As String = """"
                Dim Result As String = Response.Split(":")(3).Split(",")(0)
                _Country = Result.Replace(Qout, Nothing)
            Catch
                _Country = "Unknown"
            End Try
        End If
        Return _Country
    End Function
    Public Function VolumeSerialNumber() As String
        Return GetManagementProperty("Win32_LogicalDisk", "VolumeSerialNumber")
    End Function
    Public Function BiosVersion() As String
        Return GetManagementProperty("Win32_BIOS", "Version")
    End Function
    Public Function ProcessorId() As String
        Return GetManagementProperty("Win32_Processor", "ProcessorID")
    End Function
    Public Function ProcessorName() As String
        Return GetManagementProperty("Win32_Processor", "Name")
    End Function
    Public Function OperatingSystemName() As String
        Return GetManagementProperty("Win32_OperatingSystem", "Caption")
    End Function
    Public Function OperatingSystemSerialNumber()
        Return GetManagementProperty("Win32_OperatingSystem", "SerialNumber")
    End Function
    Public Function OperatingSystemArchitecture()
        Return GetManagementProperty("Win32_OperatingSystem", "OSArchitecture")
    End Function
    Private Function GetManagementProperty(ByVal MClassName As String, ByVal MPropertyName As String) As String
        Try
            Dim MClass As New ManagementClass(MClassName)
            Dim MObjectCollection As ManagementObjectCollection = MClass.GetInstances
            For Each MObject As ManagementObject In MObjectCollection
                Return MObject.GetPropertyValue(MPropertyName).ToString
            Next
        Catch : End Try
    End Function
End Class
