const { describe, it, before } = require('mocha');
const { createMockServer } = require('../utils/create-mock-server');
const client = require('../transactions/identityServiceProxy-transactions');
const interactions = require('../interactions/identityServiceProxy-interactions');

let mockServer;

describe('Identity service API Contract Tests; Countries Endpoint ', () => {
    before(async () => {
        mockServer = await createMockServer(process.env.CONSUMER_NAME, process.env.PROVIDER_NAME_IDENTITY_SERVICE_PROXY);
    });
    describe('Get user by email query string', () => {
        it('returns user data json by email', async () => {
            await mockServer.addInteraction(interactions.getUser);
            await client.getUser();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get user by unexisting email query string', () => {
        it('returns json message', async () => {
            await mockServer.addInteraction(interactions.getUnexistingUser);
            await client.getUnexistingUser();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get user by empty email query string', () => {
        it('returns json message', async () => {
            await mockServer.addInteraction(interactions.getEmptyUser);
            await client.getEmptyUser();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
});
