/* eslint-disable no-plusplus */
/* eslint-disable no-await-in-loop */
// import supertest from 'supertest';
import assert from "assert";

const supertest = require('supertest');
import chalk from 'chalk';
import helper from './api-helper';
import dotenv from 'dotenv-safe';

dotenv.config()

const requestLogin = supertest(process.env.BASE_ZIP_URL);
const requestToken = supertest(process.env.TOKEN_URL);
const maxRetry = process.env.MAX_HEALTH_RETRY;
let i;

async function getAuthToken() {
  return requestLogin
    .post(`/login/connect/token`)
    .type('form')
    .send({ client_id: process.env.CLIENT_ID })
    .send({ client_secret: process.env.CLIENT_SECRET })
    .send({ grant_type: process.env.GRANT_TYPE })
    .send({ username: process.env.APP_USERNAME })
    .send({ password: process.env.PASSWORD })
    .catch((error) => error.response);
}

module.exports = {

   async isApiHealthy(environment, maxRetry=3) {
    console.log(chalk.yellow('Warm up - Checking API /health before test execution...'));
    const requestHealth = supertest(environment)
    const healthCheckResponse = await requestHealth.get('/health');
    const healthStatusCode = healthCheckResponse.statusCode;
    console.log(healthStatusCode, healthCheckResponse.request.url, '99999999999')

    for (i = 0; i < maxRetry; i++) {
        if (healthStatusCode === 200) {
          // eslint-disable-next-line prettier/prettier
          console.log(
            chalk.green('Warm up - API /health is healthy, moving forward with execution >>'),
          );
          return true;
        } else {
          console.log(chalk.yellow('Warm up - Checking API /health before test execution...'));
          if (i === maxRetry) {
            console.log(chalk.red('Warm up - API /health is down!!! Suspending test execution.'));
            process.exit(1);
            return false;
          }
        }
    }
  },

  async authHeaderCharge(customerproduct) {
    const response = await helper.getAuthProxyToken(customerproduct);
    return {
      authorization: `${response.body.token_type} ${response.body.access_token}`,
      'user-agent': 'API-Test',
      'X-Zip-API-Key': process.env.API_KEY_MERCHANT,
    };
  },

  async authHeaderGeneric(){
           return supertest(process.env.TOKEN_URL)
               .post('/oauth/token')
               .set({'Content-Type': 'application/json'})
               .send(process.env.TOKEN_PAYLOAD)
               // .then(response => {
               //     console.log(response, '8888888')
               //     return response.body
               // });

    },
};
