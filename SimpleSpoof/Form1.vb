Imports System.Security.Cryptography
Imports System.Text
Imports Microsoft.Win32

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Function GenerateMac()

        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim r As New Random
        Dim sb As New StringBuilder

        For i As Integer = 1 To 12
            Dim idx As Integer = r.Next(0, 35)
            sb.Append(s.Substring(idx, 1))
        Next
        Return sb.ToString()

    End Function
    Public Function GenerateRandomString(ByRef iLength As Integer) As String
        Dim rdm As New Random()
        Dim allowChrs() As Char = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLOMNOPQRSTUVWXYZ0123456789".ToCharArray()
        Dim sResult As String = ""

        For i As Integer = 0 To iLength - 1
            sResult += allowChrs(rdm.Next(0, allowChrs.Length))
        Next

        Return sResult
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim key As Microsoft.Win32.RegistryKey
        Try
            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e972-e325-11ce-bfc1-08002be10318}\0002",
  "NetworkAddress", GenerateMac)
            ListBox1.Items.Add(">>MAC Address Spoofed")
        Catch ex As Exception
            ListBox1.Items.Add(">>MAC Address Spoofed")
        End Try

        Try
            'SMBIOS
            key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\mssmbios\\Data", True)
            key.DeleteValue("SMBiosData")
            ListBox1.Items.Add(">>SMBIOS Records Cleaned")
        Catch ex As Exception
            ListBox1.Items.Add(">>SMBIOS Records Cleaned")
        End Try

        'Network Cache
        Try

            key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e972-e325-11ce-bfc1-08002be10318}\Configuration", True)
            key.DeleteSubKey("Properties")
            ListBox1.Items.Add(">>Network Cache Cleaned")
        Catch ex As Exception
            ListBox1.Items.Add(">>Network Cache Cleaned")
        End Try


        Try
            'MOTHERBOARD
            CompIDs()
            ListBox1.Items.Add(">>Motherboard Records Cleaned")
        Catch ex As Exception
            ListBox1.Items.Add(">>Motherboard Records Cleaned")
        End Try


        Try
            cleanguid()
            ListBox1.Items.Add(">>GUID Records Cleaned")
        Catch ex As Exception
            ListBox1.Items.Add(">>GUID Records Cleaned")
        End Try



    End Sub
    Private Sub cleanguid()
        Dim random As Byte() = New Byte(99) {}
        Dim rng As RNGCryptoServiceProvider = New RNGCryptoServiceProvider()
        rng.GetBytes(random)
        Dim x As Integer = 0

        Dim g As Guid = Guid.NewGuid()

        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\\CurrentControlSet\\Control\\IDConfigDB\\Hardware Profiles\\0001",
  "HwProfileGuid", g)
        g = Guid.NewGuid
        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\\Microsoft\\Cryptography",
  "MachineGuid", g)
        g = Guid.NewGuid
        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate",
  "AccountDomainSid", g)
        g = Guid.NewGuid
        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate",
  "SusClientId", g)
        g = Guid.NewGuid
        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SystemInformation",
  "ComputerHardwareId", g)
        g = Guid.NewGuid
        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SystemInformation",
  "ComputerHardwareIds", g)
        g = Guid.NewGuid
        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\\Microsoft\\SQMClient",
  "MachineId", g)
        g = Guid.NewGuid
        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion",
  "BuildGUID", g)
        g = Guid.NewGuid
        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion",
  "ProductId", g)
        g = Guid.NewGuid
        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e968-e325-11ce-bfc1-08002be10318}\\0000",
  "_DriverProviderInfo", g)
        g = Guid.NewGuid
        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e968-e325-11ce-bfc1-08002be10318}\\0000",
  "UserModeDriverGUID", g)
    End Sub
    Private Sub CompIDs()
        Using CompIDs As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\HardwareConfig\\Current\\ComputerIds")
            For Each SubKey As String In CompIDs.GetSubKeyNames()
                CompIDs.DeleteValue(SubKey)
            Next
        End Using
    End Sub
End Class



