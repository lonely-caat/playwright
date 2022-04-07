const request = require('superagent');
require('dotenv-safe').config();

const MOCK_SERVICE_URL = `http://localhost:${process.env.MOCK_SERVER_PORT}`;

module.exports = {
    async getProducts() {
        await request
            .get(`${MOCK_SERVICE_URL}/api/products`)
            .set('Content-Type', 'application/json')
            .send();
    },
    async getCountries() {
        await request
            .get(`${MOCK_SERVICE_URL}/api/countries`)
            .set('Content-Type', 'application/json')
            .send();
    },
};