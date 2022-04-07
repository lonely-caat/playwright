# Zip.Api.CustomerProfile

CustomerProfile with structured JSON logging, styling/linting checks and skaffold support to allow local development in a minikube cluster.

## Develop locally with hot reload

`./scripts/local-dev.sh`

## Requirements

* Helm
* Skaffold
* Docker for Desktop
* kubectl
* .NET Core SDK (3.0)

## Run

`dotnet run`

## Test

`dotnet test`

### With Coverage

`dotnet test /p:CollectCoverage=true /p:Exclude=[xunit.*]*`

## GraphQL
### Request

```
{
  customerProfile(keyword: "Inam") {
    id
    givenName
    email
    dateOfBirth
    account {
      accountType
    }
  }
}
```

### Response

```
{
  "data": {
    "customerProfile": [
      {
        "id": "8e3c72ec-1e8e-4d46-80f6-c5cadd4dccbd",
        "givenName": "Inam",
        "email": null,
        "dateOfBirth": "1984-11-11",
        "account": {
          "accountType": "Company"
        }
      }
    ]
  }
}

```

### Recommended VsCode Setup

For test explorer integration: [.NET Core Test Explorer](https://marketplace.visualstudio.com/items?itemName=formulahendry.dotnet-test-explorer&WT.mc_id=-blog-scottha)

For code coverage: [Coverage Gutters](https://marketplace.visualstudio.com/items?itemName=ryanluker.vscode-coverage-gutters&WT.mc_id=-blog-scottha)

### Namespaces

We would need to set a namespace for our deployments to work as an the environment variable.
for example :

````
NAMESPACE=customer-acquisition
````

The available namespaces are configured here 
https://zipmoney.atlassian.net/wiki/spaces/DevOps/pages/770179475/Zip+-+Dev+Helper

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

3) Connecting to local postgres Database in kube cluster
In order to have a local Postgres database use this repo to start one : https://gitlab.com/zip-au/zip-thoughtworks/zip.local.postgres
The database password set should match the database password used.
To set the env variable used for database password:
```
export CUSTOMERPROFILEDATABASE__PASSWORD=<same password used for starting local postgres>
```