const request = require('superagent');
require('dotenv-safe').config();
const tangoData = require('../req-resp/tango');


const MOCK_SERVICE_URL = `http://localhost:${process.env.MOCK_SERVER_PORT}`;

module.exports = {
    async getAccounts() {
        await request
            .get(`${MOCK_SERVICE_URL}/api/accounts/123`)
            .set('Content-Type', 'application/json')
            .send();
    },
    async getAccountsAsAtDate() {
        await request
            .get(`${MOCK_SERVICE_URL}/api/accounts/123`)
            .query({asAtDate:"01-AUG-2020"})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getUnexistingAccounts() {
        await request
            .get(`${MOCK_SERVICE_URL}/api/accounts/1`)
            .set('Content-Type', 'application/json')
            .send().catch(error => {error.statusCode = 404});
    },

    async getVariations() {
        await request
            .get(`${MOCK_SERVICE_URL}/api/variations`)
            .query({AccountHash:"403655"})
            .query({OverrideRepaymentAmount:"40.0000"})
            .query({RepaymentVariationStart:"2021-03-31"})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getVariationsUnexistingAccountHash() {
        await request
            .get(`${MOCK_SERVICE_URL}/api/variations`)
            .query({AccountHash:"1"})
            .set('Content-Type', 'application/json')
            .send();
    },
    async postVariations() {
        await request
            .post(`${MOCK_SERVICE_URL}/api/variations`)
            .set('Content-Type', 'application/json')
            .send(tangoData.requests.postVariations);
    },
    async postVariationsInvalidBody() {
        await request
            .post(`${MOCK_SERVICE_URL}/api/variations`)
            .set('Content-Type', 'application/json')
            .send({}).catch(error => {error.statusCode = 400});
    },

    async getVariationsDirectDebitId() {
        await request
            .get(`${MOCK_SERVICE_URL}/api/variations/12345`)
            .set('Content-Type', 'application/json')
            .send();
    },
    async getVariationsUnexistingDirectDebitId() {
        await request
            .get(`${MOCK_SERVICE_URL}/api/variations/0`)
            .set('Content-Type', 'application/json')
            .send().catch(error => {error.statusCode = 404});
    },

    async putVariationsDirectDebitId() {
        await request
            .put(`${MOCK_SERVICE_URL}/api/variations/666`)
            .set('Content-Type', 'application/json')
            .send(tangoData.requests.putVariationsDirectDebitId);
    },
    async putVariationsDirectDebitIdNoMatch() {
        await request
            .put(`${MOCK_SERVICE_URL}/api/variations/666`)
            .set('Content-Type', 'application/json')
            .send(tangoData.requests.putVariationsDirectDebitIdNoMatch).catch(error => {error.statusCode = 400});
    },

};