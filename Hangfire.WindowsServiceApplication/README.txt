New-Service -Name "Hangfire.WindowsServiceApplication.DEBUG" -BinaryPathName "\Hangfire.WindowsServiceApplication\bin\Debug\Hangfire.WindowsServiceApplication.exe"

Get-Service -Name 'Hangfire.WindowsServiceApplication.DEBUG'
Start-Service -Name 'Hangfire.WindowsServiceApplication.DEBUG'

$service = Get-WmiObject -Class Win32_Service -Filter "Name='Hangfire.WindowsServiceApplication.DEBUG'"
$service.delete()

sc config "Hangfire.WindowsServiceApplication.DEBUG" obj= "NT AUTHORITY\NetworkService" password= ""



