
   
 - Create interaction specifying pacts
 - Run those interactions against pact provided mock server
 - Generate pact files
 - Publish pacts to pact-broker  


### To Run Tests
`npm install` to install dependencies

`npm run test:contract` to run consumer tests against mock server and generate pact file

`npm run publish:pacts` to publish pact to pact broker

`npm run test:e2e` to run consumer contract tests and publish pacts  


> Pact is a contract testing tool. Contract testing is a way to ensure that services (such as an API provider and a client) can communicate with each other.
> https://docs.pact.io/readme 