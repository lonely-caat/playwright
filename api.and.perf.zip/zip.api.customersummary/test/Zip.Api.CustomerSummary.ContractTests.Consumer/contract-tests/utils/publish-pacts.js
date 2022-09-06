/* eslint-disable no-console */

const fs = require('fs');
//const pact = require('@pact-foundation/pact-node');
const { Publisher } = require('@pact-foundation/pact');
const path = require('path');

const pactDir = path.resolve(process.cwd(), './contract-tests/pacts');

const versionFile = fs.readFileSync('./package.json');
const { version } = JSON.parse(versionFile);
const opts = {
  pactBroker: process.env.PACT_BROKER_URL,
  pactFilesOrDirs: [pactDir],
  consumerVersion: version,
};

new Publisher(opts)
    .publishPacts()
    .then(() => console.log('Pact contract publishing complete!'))
  .catch((error) => console.log('Pact contract publishing failed: ', error));
