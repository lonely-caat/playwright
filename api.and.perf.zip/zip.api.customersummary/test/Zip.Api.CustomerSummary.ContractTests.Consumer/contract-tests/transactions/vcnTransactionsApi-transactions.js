const request = require('superagent');
require('dotenv-safe').config();


const MOCK_SERVICE_URL = `http://localhost:${process.env.MOCK_SERVER_PORT}`;

module.exports = {
    async getTransactionsByNRID() {
        await request
            .get(`${MOCK_SERVICE_URL}/internal/transaction`)
            .query({nrid: '12345678'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getTransactionsByUnexistingNRID() {
        await request
            .get(`${MOCK_SERVICE_URL}/internal/transaction`)
            .query({nrid: 'noexist'})
            .set('Content-Type', 'application/json')
            .send();
    },
    

};