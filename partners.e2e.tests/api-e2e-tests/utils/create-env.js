/* eslint-disable no-undef */
import * as fs from 'fs';

// function setEnvParams(currentValue, newValue) {
//   const envFile = fs.readFileSync('./.env', 'utf8');
//   const result = envFile.replace(currentValue, newValue);
//   fs.writeFileSync('./.env', result, 'utf8');
// }
const environmentName = process.env.TEST_ENV;

switch (environmentName) {
  case 'dev':
    fs.copyFileSync('./.env.dev', './.env');
    break;
  case 'staging':
    fs.copyFileSync('./.env.staging', './.env');
    break;
  case 'sandbox':
    fs.copyFileSync('./.env.sandbox', './.env');
    break;
  default:
    fs.copyFileSync('./.env.sandbox', './.env');
    break;
}

// setEnvParams('CLIENT_SECRET', $CLIENT_SECRET);
