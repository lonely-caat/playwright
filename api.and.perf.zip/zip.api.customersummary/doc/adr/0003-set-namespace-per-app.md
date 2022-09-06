# 3. Do not provide a default namespace in the API template

Date: 2019-02-07

## Status

Accepted

## Context

We have a list of known namespaces which we can use to push to EKS. They are currently:

```
business-apps-and-support
customer-acquisition
customer-engagement
data-risk
partners
payments
platform
zipbiz
```

We need to pick the correct namespace based on the app being created from this template.

## Decision

We are not including the namespace in our environment variables. The template uses a masked namespace which is a custom Gitlab-CI variable. Developers creating a service for the first time will notice their deploy failing in the pipeline, and should add a new NAMESPACE variable to their env file that suits the business function of the API they are building.

## Consequences

Allow development team to pick a namespace that suits them instead of defaulting to a single namespace, which seems presumptuous.

