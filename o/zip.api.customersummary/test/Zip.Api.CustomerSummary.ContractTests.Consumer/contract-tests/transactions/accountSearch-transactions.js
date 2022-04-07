const request = require('superagent');
require('dotenv-safe').config();

const MOCK_SERVICE_URL = `http://localhost:${process.env.MOCK_SERVER_PORT}`;

module.exports = {
    async getAccountSearch() {
        await request
            .get(`${MOCK_SERVICE_URL}/accountsearch/api/v1/accounts`)
            .query({query:'zipMoney', type:'noFilter', skip:1, take:100})
            .set('Content-Type', 'application/json')
            .send();
    },

    async getAccountSearchNoData() {
        await request
            .get(`${MOCK_SERVICE_URL}/accountsearch/api/v1/accounts`)
            .query({query:'basTest', type:'noFilter', skip:1, take:100})
            .set('Content-Type', 'application/json')
            .send();
    },
};