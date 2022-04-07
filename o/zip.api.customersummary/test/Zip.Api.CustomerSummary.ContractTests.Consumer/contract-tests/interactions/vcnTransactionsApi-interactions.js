const { somethingLike: like } = require('@pact-foundation/pact').Matchers;
const vcnTransactionsData = require('../req-resp/vcnTransactionsApi');

module.exports = {
    getTransactionsByNRID: {
        state: 'it has the ability to return correct body and status code',
        uponReceiving: 'GET request to fetch transaction records by nrid',
        withRequest: {
            method: 'GET',
            path: '/internal/transaction',
            query: {nrid: '12345678'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(vcnTransactionsData.responses.TransactionsByNRID)
        },
    },
    getTransactionsByUnexistingNRID: {
        state: 'it has the ability to return empty array in response and correct status code',
        uponReceiving: 'GET request to fetch transaction records by unexisting nrid',
        withRequest: {
            method: 'GET',
            path: '/internal/transaction',
            query: {nrid: 'noexist'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(vcnTransactionsData.responses.TransactionsByUnexistingNRID)
        },
    },
};
