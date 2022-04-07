const request = require('superagent');
require('dotenv-safe').config();

const MOCK_SERVICE_URL = `http://localhost:${process.env.MOCK_SERVER_PORT}`;

module.exports = {
    async getUser() {
        await request
            .get(`${MOCK_SERVICE_URL}/api/user/email`)
            .query({email:'max.bilichenko@zip.co'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getUnexistingUser() {
        await request
            .get(`${MOCK_SERVICE_URL}/api/user/email`)
            .query({email:'bastest@zip.co'})
            .set('Content-Type', 'application/json')
            .send().catch(error => {error.statusCode = 404});
    },
    async getEmptyUser() {
        await request
            .get(`${MOCK_SERVICE_URL}/api/user/email`)
            .query({email:''})
            .set('Content-Type', 'application/json')
            .send().catch(error => {error.statusCode = 400});
    },
};