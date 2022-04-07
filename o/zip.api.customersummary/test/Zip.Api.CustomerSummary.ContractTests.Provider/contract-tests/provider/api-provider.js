const getPactResults = require('../utils/get-pact-results');
process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";

const opts = {
  providerBaseUrl: process.env.PROVIDER_BASE_URL,
  pactUrls: [
    `${process.env.PACT_BROKER_URL}/pacts/provider/${process.env.PROVIDER_NAME}/consumer/${process.env.CONSUMER_NAME}/latest`,
  ],
  publishVerificationResult: true,
  providerVersion: '1.0.0',
 customProviderHeaders: [],
  timeout: 60000,

  stateHandlers: {
    [null]: () => {
      // This is the "default" state handler, when no state is given
    },
    'it has the ability to return countries json': async () => {
      return Promise.resolve('This promise is resolved');
    },
    'it has the ability to return products json': async() => {
      return Promise.resolve('This promise is resolved');
    },
  },
};

async function getResults() {
  const testResults = await getPactResults(opts);
  return testResults;
};


module.exports = getResults;
