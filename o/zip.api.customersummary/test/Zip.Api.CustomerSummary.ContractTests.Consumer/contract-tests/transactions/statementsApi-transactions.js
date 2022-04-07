const request = require('superagent');
require('dotenv-safe').config();
const statements = require('../req-resp/statementsApi');


const MOCK_SERVICE_URL = `http://localhost:${process.env.MOCK_SERVER_PORT}`;

module.exports = {
    async postStatementsTrigger() {
        await request
            .post(`${MOCK_SERVICE_URL}/statements/trigger`)
            .set('Content-Type', 'application/json')
            .send(statements.requests.statementsTrigger);
    },
    async postUnexistingStatementsTrigger() {
        await request
            .post(`${MOCK_SERVICE_URL}/statements/trigger`)
            .set('Content-Type', 'application/json')
            .send(statements.requests.statementsUnexistingTrigger);
    },

};