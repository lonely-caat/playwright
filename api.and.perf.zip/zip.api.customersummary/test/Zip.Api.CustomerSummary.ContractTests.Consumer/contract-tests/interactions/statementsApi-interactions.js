const { somethingLike: like } = require('@pact-foundation/pact').Matchers;
const statementsData = require('../req-resp/statementsApi');

module.exports = {
    postStatementsTrigger: {
        state: 'it has the ability to return correct status code',
        uponReceiving: 'POST request to create statements record',
        withRequest: {
            method: 'POST',
            path: '/statements/trigger',
            body: statementsData.requests.statementsTrigger,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
        },
    },
    postUnexistingStatementsTrigger: {
        state: 'it does not return error when trying to provide invalid data',
        uponReceiving: 'POST request to create statements record with invalid data',
        withRequest: {
            method: 'POST',
            path: '/statements/trigger',
            body: statementsData.requests.statementsUnexistingTrigger,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
        },
    },
};
