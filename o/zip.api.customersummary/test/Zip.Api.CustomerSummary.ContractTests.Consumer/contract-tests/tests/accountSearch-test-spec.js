const { describe, it, before } = require('mocha');
const { createMockServer } = require('../utils/create-mock-server');
const client = require('../transactions/accountSearch-transactions');
const interactions = require('../interactions/accountSearch-interactions');

let mockServer;

describe('Account Search API Contract Tests; accountsearch endpoint', () => {
    before(async () => {
        mockServer = await createMockServer(process.env.CONSUMER_NAME, process.env.PROVIDER_NAME_ACCOUNT_SEARCH);
    });
    describe('Get valid accountsearch query data for a account', () => {
        it('Account json is successfully returned', async () => {
            await mockServer.addInteraction(interactions.getAccountSearch);
            await client.getAccountSearch();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get valid accountsearch query data for an unexisting account', () => {
        it('Empty array is successfully returned', async () => {
            await mockServer.addInteraction(interactions.getAccountSearchNoData);
            await client.getAccountSearchNoData();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
});
