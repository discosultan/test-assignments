# Stopwatch

An online service that enables to start and view stopwatches through ASp.NET Web API or SignalR.

Requirements are specified in [the API spec](assignment-azure.html).

Authentication is supported through `Authorization` header which allows Basic Access Authentication (`Basic <base64 encoded username:password>`) and API key authentication (`API-Key <key>`). Any input is considered valid for those methods.

## Examples

> GET http://stopwatch1049602881.azurewebsites.net/api/stopwatch/testuser

> POST http://stopwatch1049602881.azurewebsites.net/api/stopwatch

## Setup

### Build Requirements

- Visual Studio 2017+
- .NET Framework 4.6.1+

### Azure Configuration

[The Scripts folder](Scripts) contains helper scripts for setting up Azure Storage Account and App Service using Azure CLI 2.0.

By default, in-memory data storage is used. This can be swapped out in OWIN startup class by replacing `InMemoryTimestampRepository` with `AzureTableStorageTimestampRepository`.

## Solution Structure

- [Varus.Stopwatch](Src/Varus.Stopwatch) - Core domain library containing the logic of calculating elapsed time for a stopwatch.
- [Varus.Stopwatch.AzureTableStorage](Src/Varus.Stopwatch.AzureTableStorage) - Data access supporting Azure Table Storage for keeping track of stopwatch times.
- [Varus.Stopwatch.Web](Src/Varus.Stopwatch.Web) - ASP.NET Web API and SignalR providing a user interface for managing stopwatches.
- [Varus.Stopwatch.Web.Tests](Src/Varus.Stopwatch.Web.Tests) - Integration tests for the web userface to assure correctness of authentication and functional requirements.

