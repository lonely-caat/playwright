const { somethingLike: like } = require('@pact-foundation/pact').Matchers;
const customerProfileData = require('../req-resp/customerProfile');

module.exports = {
    getGraphql: {
        state: 'it should return valid graphql query data for a customer',
        uponReceiving: 'Get request to return customer profile data based on query',
        withRequest: {
            method: 'GET',
            path: '/graphql/',
            query: {query: customerProfileData.queries.getGraphql},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(customerProfileData.responses.validCustomer),
        },
    },
    getGraphqlNoData: {
        state: 'it should return valid graphql query data for an unexisting customer',
        uponReceiving: 'Get request to return customer profile data based on query for a customer that does not exist',
        withRequest: {
            method: 'GET',
            path: '/graphql/',
            query: {query: customerProfileData.queries.getGraphqlNoData},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(customerProfileData.responses.unexistingCustomer),
        },
    },
};
