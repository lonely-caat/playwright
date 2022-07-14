const supertest = require('supertest');
const chalk = require('chalk');
require('dotenv-safe').config();

const request = supertest(process.env.BASE_URL);
const healthRequest = supertest(process.env.BASE_URL_STAG);
const maxRetry = process.env.MAX_RETRY;

let access_token,merchant_email;

async function getAuthToken(merchant_email) {
  return request
    .post('/oauth/token')
    .set('Content-type', 'application/json')
    .send({
      grant_type: 'password',
      username: merchant_email,
      password: 'Test1234',
      audience: 'https://merchant.dev1.zipmoney.com.au/',
      scope: 'openid profile email',
      client_id: 'FZHckE3E2w6g5rfNXkFKzhV25x6bxRpA',
      client_secret: 'KG9DdC3B6PpCAiVVNPP0JcR_O3UICdz_eCakH73rdRULHV3xJNwUJvq5ufTq0Ryh',
    })
    .catch((error) => error.response);
}

module.exports = {
  async isApiHealthy() {
    console.log(chalk.yellow('Checking API health before test execution...'));
    let healthCheckResponse;
    let healthStatusCode;

    for (i = 0; i < maxRetry; i++) {
      healthCheckResponse = await healthRequest.get('/health');
      if (healthCheckResponse) {
        healthStatusCode = healthCheckResponse.statusCode;
        if (healthStatusCode === 200) {
          console.log(
            chalk.green('Warm up - API /health is healthy, moving forward with execution >>'),
          );
          break;
        } else {
          console.log(chalk.yellow('Warm up - Checking API /health before test execution...'));
          if (i === maxRetry - 1) {
            console.log(
              chalk.red(
                'Warm up - did not receive a correct status code for health endpoint!!!! Suspending test execution.',
              ),
            );
            process.exit(1);
          }
        }
      } else {
        console.log(chalk.yellow('Warm up - Checking API /health before test execution...'));
        if (i === maxRetry - 1) {
          console.log(chalk.red('Warm up - API /health is down!!! Suspending test execution.'));
          process.exit(1);
        }
      }
    }
  },

  async authHeaderGeneric(merchant_email) {
    const response = await getAuthToken(merchant_email);
    console.log(response.body.access_token)
    access_token = response.body.access_token;
    // access_token = 'eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IlJqa3pNVUl3TTBVMU16RTFNa1E1TnpZeVJFWkZNRGhDUWpJMk1UazFNemxCTjBVeVF6a3hSUSJ9.eyJodHRwczovL21lcmNoYW50LmRldjEuemlwbW9uZXkuY29tLmF1L3JvbGVzIjoiNzk1MDc0QkUtNTg2OC00ODU5LTg3NzktRTcxNTQ0QUMyN0E3IiwiaHR0cHM6Ly9tZXJjaGFudC5kZXYxLnppcG1vbmV5LmNvbS5hdS91c2VybmFtZSI6ImJhcy10ZXN0aW5nMUBtYWlsaW5hdG9yLmNvbSIsImh0dHBzOi8vbWVyY2hhbnQuZGV2MS56aXBtb25leS5jb20uYXUvZW1haWwiOiJiYXMtdGVzdGluZzFAbWFpbGluYXRvci5jb20iLCJodHRwczovL21lcmNoYW50LmRldjEuemlwbW9uZXkuY29tLmF1L2Z1bGxOYW1lIjoiYmFzLXRlc3RpbmcxQG1haWxpbmF0b3IuY29tIiwiaHR0cHM6Ly9tZXJjaGFudC5kZXYxLnppcG1vbmV5LmNvbS5hdS9maXJzdE5hbWUiOiJCYXMiLCJodHRwczovL21lcmNoYW50LmRldjEuemlwbW9uZXkuY29tLmF1L2xhc3ROYW1lIjoiVGVzdGluZyIsImh0dHBzOi8vbWVyY2hhbnQuZGV2MS56aXBtb25leS5jb20uYXUvY3JlYXRlZFRpbWUiOiIyMDIxLTAxLTE0VDIzOjQyOjE1LjY0NloiLCJpc3MiOiJodHRwczovL21lcmNoYW50LWxvZ2luLmRldi56aXAuY28vIiwic3ViIjoiYXV0aDB8OTE0MTVmNDYtMjVkMS00MWY1LWIxZjAtYTIwZmQ4NzQwYmRkIiwiYXVkIjpbImh0dHBzOi8vbWVyY2hhbnQuZGV2MS56aXBtb25leS5jb20uYXUvIiwiaHR0cHM6Ly96aXAtbWVyY2hhbnQtZGV2LmF1LmF1dGgwLmNvbS91c2VyaW5mbyJdLCJpYXQiOjE2MTI5NTIyOTIsImV4cCI6MTYxMzAzODY5MiwiYXpwIjoiRlpIY2tFM0UydzZnNXJmTlhrRkt6aFYyNXg2YnhScEEiLCJzY29wZSI6Im9wZW5pZCBwcm9maWxlIGVtYWlsIiwicGVybWlzc2lvbnMiOlsiQXBpSW50ZWdyYXRpb25SZWFkIiwiQ29tcGxldGVPcmRlckV4ZWN1dGUiLCJDcmVhdGVJbnZpdGVFeGVjdXRlIiwiQ3JlYXRlT3JkZXJFeGVjdXRlIiwiQ3JlYXRlUGF5bWVudENvZGVFeGVjdXRlIiwiQ3VzdG9tZXJzUmVhZCIsIkN1c3RvbVMzUmVwb3J0UmVhZCIsIkRpc2J1cnNlbWVudHNDcmVhdGUiLCJEaXNidXJzZW1lbnRzUmVhZCIsIkVkaXRPcmRlclJlZmVyZW5jZSIsIk5vdGlmaWNhdGlvbnNVcGRhdGUiLCJPcmRlclJlYWRGb3JCcmFuY2giLCJPcmRlcnNSZWFkIiwiUmVhc3NpZ25DdXN0b21lclN0YWZmTWVtYmVyRXhlY3V0ZSIsIlJlZnVuZE9yZGVyRXhlY3V0ZSIsIlJlcG9ydHNEYXNoYm9hcmRSZWFkIiwiUmVwb3J0c1JlYWQiLCJTZXR0aW5nc1JlYWQiLCJTaWduVXBFeGVjdXRlIiwiU2lnblVwUmVhZCIsIlN0b3JlQWR2ZXJ0aXNpbmdFeGVjdXRlIiwiU3RvcmVDYXRlZ29yeVJlYWQiLCJTdG9yZUNhdGVnb3J5VXBkYXRlIiwiU3RvcmVQcm9maWxlUmVhZCIsIlN0b3JlUHJvZmlsZVVwZGF0ZSIsIlVzZXJNYW5hZ2VtZW50RXhlY3V0ZSIsIlVzZXJNYW5hZ2VtZW50UmVhZCIsIlZpZXdBbGxCcmFuY2hlc1JlYWQiXX0.gGoHZ3GJorojmMYBppovdbQGahjD1IRRYu3QB3No36znASlEYTjsMRJ_nEwlUwxG_1NCY5fR1soHn8gnuTW3nwU_9Hzn6-7qEeYhzGQQ-iclD4IzvJeHhKi4myehVLyj9nLYTb3qh-L8AZa7vNbJQOdDGh5UsHqYccA2A5YOJ_PJmxI3_mWei0OV26VdXfcjXLybIvccvaHmYKQaYYn-sNiAmO8w9rKrRx4g13xlKB2097DqOa3h2xMPCctvUqQ5E0ezVOHWTKgMyKtImRqCz7h67TwSOQEH_GQ2xLmVdZJlPzXcGH2gdXo0_Muz-wa6rSnX2Lcn4DNJseeP1VHPUA'
    // Authorization: 'Bearer ' + `${response.body.access_token}`, put me in return statement
    return {
      Accept: 'application/json',
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + `${access_token}`,
    };
  },

  delay(milliseconds) {
    return new Promise((resolve) => setTimeout(resolve, milliseconds));
  },
};
