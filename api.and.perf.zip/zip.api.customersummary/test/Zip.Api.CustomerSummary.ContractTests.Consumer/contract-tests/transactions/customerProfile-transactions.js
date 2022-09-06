const request = require('superagent');
require('dotenv-safe').config();
const customerProfileData = require('../req-resp/customerProfile');

const MOCK_SERVICE_URL = `http://localhost:${process.env.MOCK_SERVER_PORT}`;

module.exports = {
    async getGraphql() {
        await request
            .get(`${MOCK_SERVICE_URL}/graphql/`)
            .query({query: customerProfileData.queries.getGraphql})
            .set('Content-Type', 'application/json')
            .send();
    },

    async getGraphqlNoData() {
        await request
            .get(`${MOCK_SERVICE_URL}/graphql/`)
            .query({query: customerProfileData.queries.getGraphqlNoData})
            .set('Content-Type', 'application/json')
            .send();
    },
};