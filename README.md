# Hangfire OPC Labs v1.x 

Hangfire OPC Labs v1.x solution that contains a standard .NET 4.8 MVC project by which the OPC UA Hangfire server is managed. It includes two projects, a console application for tests and a windows service project.

For more details, check out the documentation included: https://github.com/kraftcoding/Hangfire-OPC-Labs_.NET-4.8/blob/master/docs/OPC%20Foundation%20Client-Server%20Solution%20-%20Technical%20Document.pdf 

## Requirements

* Visual Studio 2022
* .NET Framework 4.8
* https://github.com/kraftcoding/OPC-Foundation-Labs-.Net-4.8
* https://github.com/kraftcoding/OPC-Foundation-Labs-Server-Client-.Net-4.8

## Others

It is needed to give permissions to the IIS app-pool process to use TCP.
*	netsh http add urlacl url=http://+:62543/ user=Everyone
*	netsh http add urlacl url=http://+:62543/LocalServer/ user=IIS APPPOOL\HangfireJobs
