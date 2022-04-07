const { somethingLike: like } = require('@pact-foundation/pact').Matchers;
const summaryData = require('../req-resp/customerSummary');

module.exports = {
    getProducts: {
        state: 'it has the ability to return products json',
        uponReceiving: 'Get request to return products',
        withRequest: {
            method: 'GET',
            path: '/api/products',
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(summaryData.responses.products),
        },
    },
    getCountries: {
        state: 'it has the ability to return countries json',
        uponReceiving: 'Get request to return countries',
        withRequest: {
            method: 'GET',
            path: '/api/countries',
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(summaryData.responses.countries),
        },
    },
};
