# Merchant Data Enrichment Api

-----
The latest release is [version 1.5.0](1.0/1.5.0.md)

[Release Index](release-index.md)

## Overview

This application is responsible to process customer payments data from different sources and persist in the database after aggregation with merchant data. Design document is available in [Confluence](https://zipmoney.atlassian.net/wiki/spaces/DEV/pages/1577943209/Merchant+Data+Enrichment)

## Prerequirements

* Visual Studio 2019 / Visual Studio Code
* .NET Core SDK 3.1
* PostgreSQL

## How To Run

* Open solution in Visual Studio 2019
* Set Zip.Api.MerchantDataEnrichment project as Startup Project and build the project.
* Run the application.
* Import postman collections from the doc folder.

OR

* Run ```dotnet run ``` from the solution folder.

## Technology used

* Docker
* Kubernetes
* Liquibase
* PostgreSQL
* ASP.NET CORE

## Packages used

* ASP.NET Core
* Entity framework Core
* Fluent Validations
* NewtonSoft JSON
* AutoMapper
* MoreLinq
* Zip.Core
* Serilog
* Swagger
* Coverlet
* xunit
* Moq
* AutoFixture
* Fluent Assertions

## Design Patterns Used

* CQRS
* DDD
* Immutability
* SOLID

## Testing

Run DotNet Test from the CLI to get code coverage stats.

```
dotnet test /p:CollectCoverage=true
```

## Environments links

* https://zip-api-merchantdataenrichment.internal.dev.au.edge.zip.co
* https://zip-api-merchantdataenrichment.internal.sand.au.edge.zip.co
* https://zip-api-merchantdataenrichment.internal.prod.au.edge.zip.co

## Swagger link

* https://zip-api-merchantdataenrichment.internal.dev.au.edge.zip.co/swagger/index.html
* https://zip-api-merchantdataenrichment.internal.sand.au.edge.zip.co/swagger/index.html

## Sonarqube link

https://sonar.internal.mgmt.au.edge.zip.co/dashboard?id=zip.api.merchantdataenrichment
