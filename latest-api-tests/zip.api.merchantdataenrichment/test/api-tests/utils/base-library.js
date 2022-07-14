const supertest = require('supertest');
const chalk = require('chalk');
require('dotenv-safe').config();

const request = supertest(process.env.BASE_URL);
const healthRequest = supertest(process.env.BASE_URL);
const maxRetry = process.env.MAX_RETRY;


module.exports = {
  async isApiHealthy() {
    console.log(chalk.yellow('Checking API health before test execution...'));
    let healthCheckResponse;
    let healthStatusCode;
    let healthResponseText;

    for (let i = 0; i < maxRetry; i++) {
      healthCheckResponse = await healthRequest.get('/health');
      if (healthCheckResponse) {
        healthStatusCode = healthCheckResponse.statusCode;
        healthResponseText = healthCheckResponse.res.text;
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
                `Warm up - did not receive a correct status code for health endpoint! 
                  Instead got status code:${healthStatusCode} and body:${healthResponseText}; 
                    Suspending test execution.`,
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

  delay(milliseconds) {
    return new Promise((resolve) => setTimeout(resolve, milliseconds));
  },
};
