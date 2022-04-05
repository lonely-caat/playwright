# E2E Tests for Partner Approved & Declined application onboarding with TestCafe

This repo demonstrates Partner Onboarding UI E2E application testing with the use of TestCafe framework which support cross browser & parallel execution.  
https://devexpress.github.io/testcafe/


### To Run Tests
`npm install` to install dependencies

`npm test` to run E2E tests on Chrome only (Default env = sandbox,  can be chanegd `./utils/create-env.sh`)  

`npm run test:local:safari` to run E2E tests on Safari only  

`test:remote:chrome` to run tests on Chrome in headless mode

#### Please make sure you create respective .env files with respective vars

#### Test execution in Pipeline  
YOu can trigger execution against any environment on demand.








