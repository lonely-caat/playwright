const { somethingLike: like } = require('@pact-foundation/pact').Matchers;
const paymentData = require('../req-resp/paymentWebhookApi');

module.exports = {
    getTransactionsByExternalId: {
        state: 'it has the ability to return correct data',
        uponReceiving: 'GET request to get transaction data by external id',
        withRequest: {
            method: 'GET',
            path: '/internal/transactions',
            query: {externalId: '52eed3e8-65c2-431a-b471-002c6a9962bd'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(paymentData.responses.transactionsByExternalId)
        },
    },
    getTransactionsByUnexistingExternalId: {
        state: 'it has the ability to return correct data when unexisting externalid is provided',
        uponReceiving: 'GET request to get transaction data by unexisting external id',
        withRequest: {
            method: 'GET',
            path: '/internal/transactions',
            query: {externalId: 'noexist'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(paymentData.responses.transactionsByUnexistingExternalId)
        },
    },

    getTransactionsByNRID: {
        state: 'it has the ability to return correct data by nrid',
        uponReceiving: 'GET request to get transaction data by nrid',
        withRequest: {
            method: 'GET',
            path: '/internal/transactions',
            query: {nrid: '12345678'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(paymentData.responses.transactionsByNRID)
        },
    },
    getTransactionsByUnexistingNRID: {
        state: 'it has the ability to return correct data when unexisting nrid is provided',
        uponReceiving: 'GET request to get transaction data by unexisting nrid',
        withRequest: {
            method: 'GET',
            path: '/internal/transactions',
            query: {nrid: 'noexist'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(paymentData.responses.transactionsByUnexistingNRID)
        },
    },

};
