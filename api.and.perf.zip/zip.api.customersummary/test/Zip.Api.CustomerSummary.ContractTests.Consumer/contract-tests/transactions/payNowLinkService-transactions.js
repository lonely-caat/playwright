const request = require('superagent');
require('dotenv-safe').config();
const payNow = require('../req-resp/payNowLinkService');


const MOCK_SERVICE_URL = `http://localhost:${process.env.MOCK_SERVER_PORT}`;

module.exports = {
    async postPaynow() {
        await request
            .post(`${MOCK_SERVICE_URL}/api/paynowlink/consumer`)
            .set('Content-Type', 'application/json')
            .send(payNow.requests.postPaynow);
    },
    async postPaynowStringValues() {
        await request
            .post(`${MOCK_SERVICE_URL}/api/paynowlink/consumer`)
            .set('Content-Type', 'application/json')
            .send(payNow.requests.postPaynowStringValues).catch(error => {error.statusCode = 400});
    },
    async postPaynowUnexistingConsumerId() {
        await request
            .post(`${MOCK_SERVICE_URL}/api/paynowlink/consumer`)
            .set('Content-Type', 'application/json')
            .send(payNow.requests.postPaynowUnexistingConsumerId).catch(error => {error.statusCode = 500});
    },
};