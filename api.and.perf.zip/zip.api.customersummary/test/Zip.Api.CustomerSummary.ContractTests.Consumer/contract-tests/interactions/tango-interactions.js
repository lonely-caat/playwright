const { somethingLike: like } = require('@pact-foundation/pact').Matchers;
const tangoData = require('../req-resp/tango');

module.exports = {
    getAccounts: {
        state: 'it has the ability to return accounts json',
        uponReceiving: 'Get request to return account data',
        withRequest: {
            method: 'GET',
            path: '/api/accounts/123',
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(tangoData.responses.getAccounts),
        },
    },
    getAccountsAsAtDate: {
        state: 'it has the ability to return accounts json by asAtDate query string',
        uponReceiving: 'Get request to return account data for asAtDate query string',
        withRequest: {
            method: 'GET',
            path: '/api/accounts/123',
            query: {asAtDate:"01-AUG-2020"},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(tangoData.responses.getAccountsAsAtDate),
        },
    },
    getUnexistingAccounts: {
        state: 'it has the ability to return error string',
        uponReceiving: 'Get request will return an error message string',
        withRequest: {
            method: 'GET',
            path: '/api/accounts/1',
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 404,
            body: like(tangoData.responses.getUnexistingAccounts),
        },
    },
    getVariations: {
        state: 'it has the ability to return variations json by query string params',
        uponReceiving: 'Get request to return variations data for query string parameters',
        withRequest: {
            method: 'GET',
            path: '/api/variations',
            query: {AccountHash:"403655",OverrideRepaymentAmount:"40.0000",RepaymentVariationStart:"2021-03-31"},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(tangoData.responses.getVariations),
        },
    },
    getVariationsUnexistingAccountHash: {
        state: 'it has the ability to return empty array for unexisting account hash value',
        uponReceiving: 'Get request to return variations data for query string parameters where accountHash does not exist',
        withRequest: {
            method: 'GET',
            path: '/api/variations',
            query: {AccountHash:"1"},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(tangoData.responses.getVariationsUnexistingAccountHash),
        },
    },

    postVariations: {
        state: 'it has the ability to create a new record and return that record data',
        uponReceiving: 'Post request to create a new variations record',
        withRequest: {
            method: 'POST',
            path: '/api/variations',
            body: tangoData.requests.postVariations,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(tangoData.responses.postVariations),
        },
    },
    postVariationsInvalidBody: {
        state: 'it has the ability to return validation error with validation message',
        uponReceiving: 'Post request with invalid body to create a new variations record',
        withRequest: {
            method: 'POST',
            path: '/api/variations',
            body: {},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 400,
            body: tangoData.responses.postVariationsInvalidBody,
        },
    },

    getVariationsDirectDebitId: {
        state: 'it has the ability to return variations json by Direct Debit id',
        uponReceiving: 'Get request to return variations data by Direct Debit id',
        withRequest: {
            method: 'GET',
            path: '/api/variations/12345',
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(tangoData.responses.getVariationsDirectDebitId),
        },
    },
    getVariationsUnexistingDirectDebitId: {
        state: 'it has the ability to error string for an unexisting direct debit id',
        uponReceiving: 'Get request to return variations data by unexisting Direct Debit id',
        withRequest: {
            method: 'GET',
            path: '/api/variations/0',
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 404,
            body: like(tangoData.responses.getVariationsUnexistingDirectDebitId),
        },
    },

    putVariationsDirectDebitId: {
        state: 'it has the ability to edit existing variations',
        uponReceiving: 'Put request to return variations data after modification',
        withRequest: {
            method: 'PUT',
            path: '/api/variations/666',
            body: tangoData.requests.putVariationsDirectDebitId,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(tangoData.responses.putVariationsDirectDebitId),
        },
    },
    putVariationsDirectDebitIdNoMatch: {
        state: 'it has the ability to return error for unmatching directdebitid',
        uponReceiving: 'Put request to return variations json validation error',
        withRequest: {
            method: 'PUT',
            path: '/api/variations/666',
            body: tangoData.requests.putVariationsDirectDebitIdNoMatch,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 400,
            body: like(tangoData.responses.putVariationsDirectDebitIdNoMatch),
        },
    },
};
