# Zip.Api.CustomerSummary

[![Quality Gate Status](https://sonar.internal.mgmt.au.edge.zip.co/api/project_badges/measure?project=zip-api-customersummary&metric=alert_status)](https://sonar.internal.mgmt.au.edge.zip.co/dashboard?id=zip-api-customersummary)

Api project for admin customer summary. This api is a Backend For Frontend (BFF) for https://gitlab.com/zip-au/bas/front-end/zip.admin.web.customersummary which acts as an api gateway to fetch all the required data.

## Develop locally with hot reload

`./scripts/local-dev.sh`

## Requirements

* Helm
* Skaffold
* Docker for Desktop
* kubectl
* .NET Core SDK (2.2)

## Run

`dotnet run`

## Test

`dotnet test`

### With Coverage

`dotnet test /p:CollectCoverage=true /p:Exclude=[xunit.*]*`

### Recommended VsCode Setup

For test explorer integration: [.NET Core Test Explorer](https://marketplace.visualstudio.com/items?itemName=formulahendry.dotnet-test-explorer&WT.mc_id=-blog-scottha)

For code coverage: [Coverage Gutters](https://marketplace.visualstudio.com/items?itemName=ryanluker.vscode-coverage-gutters&WT.mc_id=-blog-scottha)

### Namespaces

````
NAMESPACE=business-apps-and-support
````

### Application settings environment variables

There are 2 types of application setting environment variables :

1) Non-Secrets

To put the variables like external service URLs or any not secret stuff, they can be provided into helm deployment templates

 - Create a placeholder in deployment.yaml file and

 - Provide its corresponding value in 'values-local.yaml' and 'values-prodlike.yaml'
Refer this commit for an example:
https://gitlab.com/zip-au/zip-thoughtworks/zip.api.accounts/commit/76ab8259298f6994a247937c2905278f58204cb8

2) Secrets
Secrets should be managed in Vaults, this is still work in progress.

Meanwhile as a workaround, they can be put in Git-lab Environment variable and then it needs to be inserted 
into the cluster using the ci pipeline.(via makefile)

Refer this files for an example :
Setting Environment variable (masked) in git lab UI : https://docs.gitlab.com/ee/ci/variables/
Makefile : https://gitlab.com/zip-au/zip-thoughtworks/zip.api.linking/blob/master/Makefile
gitlab-ci.yml : https://gitlab.com/zip-au/zip-thoughtworks/zip.api.linking/blob/master/.gitlab-ci.yml

### Running Smoke Tests in the build
The gitlab-ci pipeline uses a python library called tox to smoke test the end points after a successful deployment. The default endpoint it checks is /health and expects a 200 response status with text "Healthy". In order to customize/change, edit the file test/test_website.py.

### Details of Service and endpoints
This is the confluence link to environment endpoints, specs, architecture.
https://zipmoney.atlassian.net/wiki/spaces/MOAP/pages/770181538/Linking+API

## Decisions

* Logging in JSON format because it lets us describe events in systems in a more machine-readable format (In the spirit of observability)

* xUnit for Testing

* Using Skaffold for hot reloading for local dev - this will help us go fast if we need to pull in new dependencies while developing locally

* Turning off HTTPS redirect so that the cluster can handle certificates, etc

-----------------------------------

## Service Acceptance
### 1. Code
#### 1.1 Team responsible for the implementation ?
 - Business apps and support
#### 1.2 Where is the code stored ?
 - https://gitlab.com/zip-au/bas/api-and-services/zip.api.customersummary

### 2. Deployment/Automation
#### 2.1 Where it deploys ? How ?
 - AWS v2 K8s (GitLab pipeline)]

#### 2.2 How often it is deployed ?
 - Frequently

#### 2.3 Does the deployment causes downtime ? If yes, are there any business impact  ? Is there any process to reduce the impact ?
 - No down time.

#### 2.4 If deployment fails, how to rollback ?
 - Run previous CI/CD pipeline

#### 2.5 Has all the mandatory Tags being added ? More info
 - Tags used: "Team", "Environment", "Repo", "Zone", "Version"

### 3. Redundancy
#### 3.1 Where it gets deployed ?
 - AWS V2 EKS

#### 3.2 How many zones/regions it gets deployed ?
 - Multi AZ, region: ap-southeast-2

#### 3.3 Does the service auto scale  ? How it scales ?
 - Kubernetes autoscale: TBD by DevOps

#### 3.3 What happens if one zone goes down ? Anything needed to ensure service keeps running ?
 - We have multiple pods running in multiple availability zones, so app still works if one zone goes down

#### 3.4 What happens if one region goes down ? Anything needed to ensure service keeps running ?
 - TBD

### 4. Security
#### 4.1 How to access this service ?
 - Login to AWS EKS. Follow this link https://zipmoney.atlassian.net/wiki/spaces/DevOps/pages/771817617/Kubectl+on+AWS+EKS+and+OneLogin+roles

#### 4.2 Who has access to this service (Customer Summary page) ?
 - Customer Service Agents

### 5. Monitoring
#### 5.1 Is there any logging ? How to access logs ?
 - To be build for logging : Prometheus
 - To be build for access log : Grafana

#### 5.2 Is there any dashboard/metrics ? How to access ?
 - New Relic https://rpm.newrelic.com/accounts/1367286/applications/375295028
 - Grafana

#### 5.3 Is there any alerts ? Where are they setup and who gets alerted ?
 - New Relic : BAS team to set up alert
 - Grafana : BAS team to set up alert

### 6. Errors
#### 6.1 What are the possible problems/failures ?
 - It depends on services from AWS v1, if we have any connectivity issue or if we have one of the services not functioning then we will have limited functionality for the customer summary page.
#### 6.2 How it gets alerted ?
 - Opsgenie
#### 6.3 How to troubleshoot  ?
 - First look at https://api-customer-summary.internal.{env}.au.edge.zip.co/health
 - If all services are healthy then we need to look at Kibana logs


## Release notes

[Latest releases](release-notes/README.md)

## Architecture decisions

* [Record architecture decisions](doc/adr/0001-record-architecture-decisions.md)
  
* [Use xunit for tests](doc/adr/0002-use-xunit-for-tests.md)

* [Set namespace per app](doc/adr/0003-set-namespace-per-app.md)

* [Ignore dns propagation issues in smoke test](doc/adr/0004-ignore-dns-propogation-issue-in&#32;smoke-test.md)