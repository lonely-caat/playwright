# POC for Contract Tests - Provider Side 
This contract tests POC is created using a nodejs library called Pact.  
https://github.com/pact-foundation/pact-js

This POC demonstrate provider side implementaion of contract tests  
Provider is the author of api.  
Provider team does not have to write any tests rather they need to:    
 - Get pact files from pact-broker
 - Execute tests from the pacts file
 - Publish result to pact-broker  


### To Run Tests
`npm install` to install dependencies  

`npm run test:contract` to run consumer tests against mock server and generate pact file


### Test Report
mochawesome reporter is used to capture and present the test results.

Test Report is available in `./contract-tests/mochawesome-report` in both json and html format

> Pact is a contract testing tool. Contract testing is a way to ensure that services (such as an API provider and a client) can communicate with each other.
> https://docs.pact.io/readme 