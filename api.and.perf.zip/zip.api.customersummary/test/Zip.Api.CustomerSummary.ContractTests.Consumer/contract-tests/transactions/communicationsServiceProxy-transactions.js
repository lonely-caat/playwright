const request = require('superagent');
require('dotenv-safe').config();
const communicationData = require('../req-resp/communicationsServiceProxy');


const MOCK_SERVICE_URL = `http://localhost:${process.env.MOCK_SERVER_PORT}`;

module.exports = {
    async postCloseAccount() {
        await request
            .post(`${MOCK_SERVICE_URL}/api/emails/send/close-account`)
            .set('Content-Type', 'application/json')
            .send(communicationData.requests.postCloseAccount);
    },
    async postResetPassword() {
        await request
            .post(`${MOCK_SERVICE_URL}/api/emails/send/reset-password`)
            .set('Content-Type', 'application/json')
            .send(communicationData.requests.postResetPassword);
    },
    async getSmsContent() {
        await request
            .get(`${MOCK_SERVICE_URL}/api/sms/content/expired%20card`)
            .set('Content-Type', 'application/json')
            .send();
    },
    async postSendSms() {
        await request
            .post(`${MOCK_SERVICE_URL}/api/sms/send`)
            .set('Content-Type', 'application/json')
            .send(communicationData.requests.postSendSms);
    },
    async postSendSmsPaynowLink() {
        await request
            .post(`${MOCK_SERVICE_URL}/api/sms/send/paynowlink`)
            .set('Content-Type', 'application/json')
            .send(communicationData.requests.postSendSmsPaynowLink);
    },
    async postSendEmailAccountPaidout() {
        await request
            .post(`${MOCK_SERVICE_URL}/api/emails/send/account-paidout`)
            .set('Content-Type', 'application/json')
            .send(communicationData.requests.postSendEmailAccountPaidout);
    },
};