/* eslint-disable no-undef */
const fs = require('fs');

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
  default:
    fs.copyFileSync('./.env.dev', './.env');
    break;
}

// setEnvParams('CLIENT_SECRET', $CLIENT_SECRET);
