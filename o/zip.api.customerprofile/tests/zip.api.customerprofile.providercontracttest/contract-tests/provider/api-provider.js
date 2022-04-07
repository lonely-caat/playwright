const getPactResults = require('../utils/get-pact-results');

const opts = {
  providerBaseUrl: process.env.PROVIDER_BASE_URL,
  pactUrls: [
    `${process.env.PACT_BROKER_URL}/pacts/provider/${process.env.PROVIDER_NAME}/consumer/${process.env.CONSUMER_NAME}/latest`,
  ],
  publishVerificationResult: true,
  providerVersion: '1.0.0',
  customProviderHeaders: [],
  timeout: 600000,
  requestFilter: (req, res, next) => {
    next();
  },

  stateHandlers: {
    [null]: () => {
      // This is the "default" state handler, when no state is given
    },
    'it should return valid graphql query data for a customer': async () => {
      return Promise.resolve('This promise is resolved');
    },
    'it should return valid graphql query data for an unexisting customer': async() => {
      return Promise.resolve('This promise is resolved');
    },
  },
};

async function getResults() {
  const testResults = await getPactResults(opts);
  return testResults;
};


module.exports = getResults;
