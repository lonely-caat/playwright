const request = require('superagent');
require('dotenv-safe').config();

const MOCK_SERVICE_URL = `http://localhost:${process.env.MOCK_SERVER_PORT}`;

module.exports = {
    async putEmailValidate() {
        await request
            .put(`${MOCK_SERVICE_URL}/customer-api/v1/customers/1/email/validate`)
            .send({'EmailAddress':'basTest@mailiantor.com'})
            .set('Content-Type', 'application/json')
    },
    async putEmailValidateExistingEmail() {
        await request
            .put(`${MOCK_SERVICE_URL}/customer-api/v1/customers/1/email/validate`)
            .send({'EmailAddress':'max.bilichenko@zip.co'})
            .set('Content-Type', 'application/json')
    },
    async putEmailValidateInvalidEmail() {
        await request
            .put(`${MOCK_SERVICE_URL}/customer-api/v1/customers/1/email/validate`)
            .send({'EmailAddress':''})
            .set('Content-Type', 'application/json')
    },
    async putEmailValidateInvalidCustomerId() {
        await request
            .put(`${MOCK_SERVICE_URL}/customer-api/v1/customers/10000000000000000000000000/email/validate`)
            .send({'EmailAddress':'basTest@mailiantor.com'})
            .set('Content-Type', 'application/json')
    },

    async putEmailUpdate() {
        await request
            .put(`${MOCK_SERVICE_URL}/customer-api/v1/customers/1/email`)
            .send({'EmailAddress':'basTest2@mailiantor.com'})
            .set('Content-Type', 'application/json')
    },
    async putEmailUpdateExistingEmail() {
        await request
            .put(`${MOCK_SERVICE_URL}/customer-api/v1/customers/1/email`)
            .send({'EmailAddress':'max.bilichenko@zip.co'})
            .set('Content-Type', 'application/json')
    },
    async putEmailUpdateInvalidEmail() {
        await request
            .put(`${MOCK_SERVICE_URL}/customer-api/v1/customers/1/email`)
            .send({'EmailAddress':''})
            .set('Content-Type', 'application/json')
    },
    async putEmailUpdateInvalidCustomerid() {
        await request
            .put(`${MOCK_SERVICE_URL}/customer-api/v1/customers/10000000000000000000000000/email`)
            .send({'EmailAddress':'basTest2@mailinator.com'})
            .set('Content-Type', 'application/json')
    },


    async putMobileValidate() {
        await request
            .put(`${MOCK_SERVICE_URL}/customer-api/v1/customers/1/mobile/validate`)
            .send({'PhoneNumber':'0400000000'})
            .set('Content-Type', 'application/json')
    },
    async putMobileValidateExistingMobile() {
        await request
            .put(`${MOCK_SERVICE_URL}/customer-api/v1/customers/1/mobile/validate`)
            .send({'PhoneNumber':'0423235237'})
            .set('Content-Type', 'application/json')
    },
    async putMobileValidateInvalidMobile() {
        await request
            .put(`${MOCK_SERVICE_URL}/customer-api/v1/customers/1/mobile/validate`)
            .send({'PhoneNumber':''})
            .set('Content-Type', 'application/json')
    },
    async putMobileValidateInvalidCustomerid() {
        await request
            .put(`${MOCK_SERVICE_URL}/customer-api/v1/customers/10000000000000000000000000/mobile/validate`)
            .send({'PhoneNumber':'0400000000'})
            .set('Content-Type', 'application/json')
    },

    async putMobileUpdate() {
        await request
            .put(`${MOCK_SERVICE_URL}/customer-api/v1/customers/1/mobile`)
            .send({'PhoneNumber':'0400000000'})
            .set('Content-Type', 'application/json')
    },
    async putMobileUpdateExistingMobile() {
        await request
            .put(`${MOCK_SERVICE_URL}/customer-api/v1/customers/1/mobile`)
            .send({'PhoneNumber':'0423235237'})
            .set('Content-Type', 'application/json')
    },
    async putMobileUpdateInvalidMobile() {
        await request
            .put(`${MOCK_SERVICE_URL}/customer-api/v1/customers/1/mobile`)
            .send({'PhoneNumber':''})
            .set('Content-Type', 'application/json')
    },
    async putMobileUpdateInvalidCustomerid() {
        await request
            .put(`${MOCK_SERVICE_URL}/customer-api/v1/customers/10000000000000000000000000/mobile`)
            .send({'PhoneNumber':'0400000000'})
            .set('Content-Type', 'application/json')
    },

};