const request = require('superagent');
require('dotenv-safe').config();


const MOCK_SERVICE_URL = `http://localhost:${process.env.MOCK_SERVER_PORT}`;

module.exports = {
    async getTransactionsByNRID() {
        await request
            .get(`${MOCK_SERVICE_URL}/internal/transactions`)
            .query({nrid: '12345678'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getTransactionsByUnexistingNRID() {
        await request
            .get(`${MOCK_SERVICE_URL}/internal/transactions`)
            .query({nrid: 'noexist'})
            .set('Content-Type', 'application/json')
            .send();
    },

    async getTransactionsByExternalId() {
        await request
            .get(`${MOCK_SERVICE_URL}/internal/transactions`)
            .query({externalId: '52eed3e8-65c2-431a-b471-002c6a9962bd'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getTransactionsByUnexistingExternalId() {
        await request
            .get(`${MOCK_SERVICE_URL}/internal/transactions`)
            .query({externalId: 'noexist'})
            .set('Content-Type', 'application/json')
            .send();
    },

};