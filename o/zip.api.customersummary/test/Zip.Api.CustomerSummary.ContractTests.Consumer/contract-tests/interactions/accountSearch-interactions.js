const { somethingLike: like } = require('@pact-foundation/pact').Matchers;
const accountSearchData = require('../req-resp/accountSearch');

module.exports = {
    getAccountSearch: {
        state: 'it has the ability to return account json',
        uponReceiving: 'Get request to return customer account data based on query',
        withRequest: {
            method: 'GET',
            path: '/accountsearch/api/v1/accounts',
            query: {query:'zipMoney', type:'noFilter', skip:"1", take:"100"},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(accountSearchData.responses.validAccount),
        },
    },
    getAccountSearchNoData: {
        state: 'it has the ability to return empty array for data based on query for an account that does not exist',
        uponReceiving: 'Get request to return account data based on query for an account that does not exist',
        withRequest: {
            method: 'GET',
            path: '/accountsearch/api/v1/accounts',
            query: {query:'basTest', type:'noFilter', skip:"1", take:"100"},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: [],
        },
    },
};
