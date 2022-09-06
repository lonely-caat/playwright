# 2. Ignore DNS Propogation Issue in Smoke Test in CI

Date: 2019-07-10

## Status

Accepted

## Context

Its Ok for now that the CI pipeline breaks due to DNS propogation time in smoke test, this happens when we change the host name in Ingress Rules. e.g Chaning the value of host in values-dev.yaml

````
Changing from :
host: api-specs.template.dev.au.edge.zip.co
To :
host: api.template.dev.au.edge.zip.co
````

## Decision

Continue to ignore this build failure for now as it looks like an edge case. To be fixed later.

## Consequences

The developers might need to wait for 10 mins for DNS propogation to happen and then retry the smoke test.