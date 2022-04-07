const { Verifier } = require('@pact-foundation/pact');
const request = require('superagent');

async function getResults(url, result) {
    const publishIndex = result.indexOf(`${url}/pact-version`);
    const resultUrl = result.substring(publishIndex);
    console.log('Results URL on Pact Broker > ', resultUrl);

    const resultBody = await request.get(resultUrl);
    return resultBody.body.testResults.tests;
}

async function getPactResults(opts) {
    const consumerUrl = opts.pactUrls[0].substring(0, opts.pactUrls[0].indexOf('/latest'));
    let failure;
    const success = await new Verifier(opts).verifyProvider().catch((error) => {
        failure = error.toString();
    });
    if (success) {
        return getResults(consumerUrl, success);
    }
    return getResults(consumerUrl, failure);
}

module.exports = getPactResults;