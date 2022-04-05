const supertest = require('supertest');
const urls = require('../constants/urls');

const payloadZipPay = {
    classification: "ZipPay",
    customerEmail: "zippay.email.login@mailinator.com",
};

const payloadZipMoney = {
    classification: 'ZipMoney',
    customerEmail: 'zipmoney.email.login@mailinator.com',
};

async function CreatePin(payload){
    return supertest(urls.generateToken)
        .post('/createPin')
        .set({ 'Content-Type': 'application/json' })
        .send(payload)
        .then(({ status, text }) => {
            if (status !== 200) {
                throw new Error(`Status code ${status}`);
            }
            return text;
        })
        .catch((error) => {
            throw new Error(`Test failed due to /createPin script returning error: ${error}`);
        });
}

module.exports = {
    async CreatePinZipPay() {
        return createUser(CreatePin(payloadZipPay));
    },
    async CreatePinZipMoney() {
        return createUser(CreatePin(payloadZipMoney));
    },
};
