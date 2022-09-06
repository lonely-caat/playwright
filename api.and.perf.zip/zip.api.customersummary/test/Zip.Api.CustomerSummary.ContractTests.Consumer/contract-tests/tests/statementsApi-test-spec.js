const { describe, it, before } = require('mocha');
const { createMockServer } = require('../utils/create-mock-server');
const client = require('../transactions/statementsApi-transactions');
const interactions = require('../interactions/statementsApi-interactions');

let mockServer;

describe('Statements API Contract Tests; trigger endpoint', () => {
    before(async () => {
        mockServer = await createMockServer(process.env.CONSUMER_NAME, process.env.PROVIDER_NAME_STATEMENTS_API);
    });
    describe('Post valid trigger data ', () => {
        it('Statements record is successfully returned', async () => {
            await mockServer.addInteraction(interactions.postStatementsTrigger);
            await client.postStatementsTrigger();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Post valid trigger data ', () => {
        it('Statements record is successfully returned', async () => {
            await mockServer.addInteraction(interactions.postUnexistingStatementsTrigger);
            await client.postUnexistingStatementsTrigger();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });


});
