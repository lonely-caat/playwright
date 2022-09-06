const request = require('superagent');
require('dotenv-safe').config();
const paymentsData = require('../req-resp/paymentsServiceProxy');


const MOCK_SERVICE_URL = `http://localhost:${process.env.MOCK_SERVER_PORT}`;

module.exports = {
    async getPaymentsById() {
        await request
            .get(`${MOCK_SERVICE_URL}/payments/2828`)
            .set('Content-Type', 'application/json')
            .send();
    },
    async getPaymentsByUnexistingId() {
        await request
            .get(`${MOCK_SERVICE_URL}/payments/99999999999883344`)
            .set('Content-Type', 'application/json')
            .send().catch(error => {error.statusCode = 404});
    },
    async getPaymentsByInvalidId() {
        await request
            .get(`${MOCK_SERVICE_URL}/payments/noexist`)
            .set('Content-Type', 'application/json')
            .send().catch(error => {error.statusCode = 400});
    },
    async getPaymentsByAccount() {
        await request
            .get(`${MOCK_SERVICE_URL}/payments`)
            .query({accountid: '10'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getPaymentsByUnexistingAccount() {
        await request
            .get(`${MOCK_SERVICE_URL}/payments`)
            .query({accountid: 'wowwowowowwowowoowowowooowowowoowowowowowoowow'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getPaymentsByAccountDateRange() {
        await request
            .get(`${MOCK_SERVICE_URL}/payments`)
            .query({accountid: '123'})
            .query({fromDate: '2020-11-17'})
            .query({toDate:'2020-11-19'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getPaymentsByDateRange() {
        await request
            .get(`${MOCK_SERVICE_URL}/payments`)
            .query({fromDate:'2021-04-03'})
            .query({toDate:'2021-04-04'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getPaymentsByInvalidDateRange() {
        await request
            .get(`${MOCK_SERVICE_URL}/payments`)
            .query({fromDate:'9999-04-03'})
            .query({toDate:'2021-04-04'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getPaymentsPaymentBatchId() {
        await request
            .get(`${MOCK_SERVICE_URL}/payments`)
            .query({PaymentBatchId: '0891da65-b462-41ae-94f7-76985dc76c4e'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getPaymentsInvalidPaymentBatchId() {
        await request
            .get(`${MOCK_SERVICE_URL}/payments`)
            .query({PaymentBatchId: 'test'})
            .set('Content-Type', 'application/json')
            .send().catch(error => {error.statusCode = 400});
    },
    async getPaymentsUnexistingPaymentBatchId() {
        await request
            .get(`${MOCK_SERVICE_URL}/payments`)
            .query({PaymentBatchId: '0339b499-aa98-46f2-b9d8-1eedb22c32b1'})
            .set('Content-Type', 'application/json')
            .send().catch(error => {error.statusCode = 400});
    },
    async getPaymentsAllQueries() {
        await request
            .get(`${MOCK_SERVICE_URL}/payments`)
            .query({accountid: '140654'})
            .query({paymentBatchId: '48661050-1a74-4601-944e-813046c7eb6f'})
            .query({fromDate:'2020-04-01'})
            .query({toDate:'2021-04-04'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async postPayments() {
        await request
            .post(`${MOCK_SERVICE_URL}/payments`)
            .set('Content-Type', 'application/json')
            .send(paymentsData.requests.postPayments);
    },

    async getPaymentMethods() {
        await request
            .get(`${MOCK_SERVICE_URL}/paymentmethods`)
            .query({customerId: '79779'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getPaymentMethodsUnexistingCustomerId() {
        await request
            .get(`${MOCK_SERVICE_URL}/paymentmethods`)
            .query({customerId: '7977990909066'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getPaymentMethodsFailedAttemptedTrue() {
        await request
            .get(`${MOCK_SERVICE_URL}/paymentmethods`)
            .query({customerId: '516021'})
            .query({includeFailedAttempted:'true'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getPaymentMethodsFailedAttemptedFalse() {
        await request
            .get(`${MOCK_SERVICE_URL}/paymentmethods`)
            .query({customerId: '302353'})
            .query({includeFailedAttempted:'false'})
            .set('Content-Type', 'application/json')
            .send();
    },

    async getPaymentMethodsGuid() {
        await request
            .get(`${MOCK_SERVICE_URL}/paymentmethods/f82d0cf0-3382-4e27-a4dc-001da80014a4`)
            .set('Content-Type', 'application/json')
            .send();
    },
    async getPaymentMethodsUnexistingGuid() {
        await request
            .get(`${MOCK_SERVICE_URL}/paymentmethods/f82d0cf0-3382-4e27-a4dc-001da80014a6`)
            .set('Content-Type', 'application/json')
            .send().catch(error => {error.statusCode = 404});
    },
    async getPaymentMethodsInvalidGuid() {
        await request
            .get(`${MOCK_SERVICE_URL}/paymentmethods/f82d0cf0-3382-4e27-a4dc-001d`)
            .set('Content-Type', 'application/json')
            .send().catch(error => {error.statusCode = 400});
    },

    async postPaymentMethods() {
        await request
            .post(`${MOCK_SERVICE_URL}/paymentmethods`)
            .set('Content-Type', 'application/json')
            .send(paymentsData.requests.postPaymentMethods);
    },
    async postInvalidPaymentMethods() {
        await request
            .post(`${MOCK_SERVICE_URL}/paymentmethods`)
            .set('Content-Type', 'application/json')
            .send(paymentsData.requests.postInvalidPaymentMethods).catch(error => {error.statusCode = 400});
    },

    async deletePaymentMethodUnexistingId() {
        await request
            .delete(`${MOCK_SERVICE_URL}/paymentmethods/3dca16e8-20f9-4301-a79a-002384695443`)
            .set('Content-Type', 'application/json')
            .send().catch(error => {error.statusCode = 204});
    },
    async deletePaymentMethodInvalidId() {
        await request
            .delete(`${MOCK_SERVICE_URL}/paymentmethods/test`)
            .set('Content-Type', 'application/json')
            .send().catch(error => {error.statusCode = 400});
    },

    async postRefundFatZebraError() {
        await request
            .post(`${MOCK_SERVICE_URL}/662f6753-f115-4708-bb99-fd89935f3472/refund`)
            .set('Content-Type', 'application/json')
            .send({}).catch(error => {error.statusCode = 422});
    },
    async postRefundUnexistingGuid() {
        await request
            .post(`${MOCK_SERVICE_URL}/662f6753-f115-4708-bb99-fd89935f3478/refund`)
            .set('Content-Type', 'application/json')
            .send({}).catch(error => {error.statusCode = 404});
    },

    async getPaymentBatchesDate() {
        await request
            .get(`${MOCK_SERVICE_URL}/paymentBatches`)
            .query({startDate: '2020-11-11'})
            .query({endDate: '2020-11-15'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getPaymentBatchesDateSkipTrue() {
        await request
            .get(`${MOCK_SERVICE_URL}/paymentBatches`)
            .query({startDate: '2020-11-11'})
            .query({endDate: '2020-11-15'})
            .query({skip: '1'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getPaymentBatchesDateSkipFalse() {
        await request
            .get(`${MOCK_SERVICE_URL}/paymentBatches`)
            .query({startDate: '2020-11-11'})
            .query({endDate: '2020-11-15'})
            .query({skip: '0'})
            .set('Content-Type', 'application/json')
            .send();
    },

    async getPaymentBatchesDateTakeOne() {
        await request
            .get(`${MOCK_SERVICE_URL}/paymentBatches`)
            .query({startDate: '2020-11-11'})
            .query({endDate: '2020-11-15'})
            .query({skip: '0'})
            .query({take: '1'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getPaymentBatchesDateTakeThree() {
        await request
            .get(`${MOCK_SERVICE_URL}/paymentBatches`)
            .query({startDate: '2020-11-11'})
            .query({endDate: '2020-11-15'})
            .query({skip: '0'})
            .query({take: '3'})
            .set('Content-Type', 'application/json')
            .send();
    },

    async getPaynowHash() {
        await request
            .get(`${MOCK_SERVICE_URL}/paynow/hash`)
            .query({"product":"ZipMoney"})
            .query({"country": "AU"})
            .query({"reference": "Batch:DefaultCard:Arrears:DD-A200402-2to250F10T2000N0:158656"})
            .query({"amount": "0.01"})
            .query({"paymentType":"gradual"})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getPaynowHashInvalidData() {
        await request
            .get(`${MOCK_SERVICE_URL}/paynow/hash`)
            .query({"product":"test"})
            .query({"country": "test"})
            .query({"reference": "test"})
            .query({"amount": "test"})
            .query({"paymentType":"test"})
            .set('Content-Type', 'application/json')
            .send().catch(error => {error.statusCode = 400});
    },
    async getProviderHash() {
        await request
            .get(`${MOCK_SERVICE_URL}/provider/hash`)
            .query({"product":"ZipMoney"})
            .query({"country": "AU"})
            .query({"reference": "Batch:DefaultCard:Arrears:DD-A200402-2to250F10T2000N0:158656"})
            .query({"amount": "0.01"})
            .query({"paymentType":"gradual"})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getProviderHashInvalidData() {
        await request
            .get(`${MOCK_SERVICE_URL}/provider/hash`)
            .query({"product":"test"})
            .query({"country": "test"})
            .query({"reference": "test"})
            .query({"amount": "test"})
            .query({"paymentType":"test"})
            .set('Content-Type', 'application/json')
            .send().catch(error => {error.statusCode = 400});
    },

};