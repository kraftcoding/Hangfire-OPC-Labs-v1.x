# Hangfire-OPC-Labs_.NET-4.8

This solution contains a standard .NET 4.8 MVC project by which the Hangfire server	 is managed. It includes two projects, a console application for tests and a windows service project.

## Requirements

* Visual Studio 2022
* .NET Framework 4.8
* https://github.com/kraftcoding/OPC-Foundation-Labs-.Net-4.8
* https://github.com/kraftcoding/OPC-Foundation-Labs-Server-Client-.Net-4.8

## Others

It is needed to give permissions to the IIS app-pool process to use TCP.
*	netsh http add urlacl url=http://+:62543/ user=Everyone
*	netsh http add urlacl url=http://+:62543/LocalServer/ user=IIS APPPOOL\HangfireJobs
