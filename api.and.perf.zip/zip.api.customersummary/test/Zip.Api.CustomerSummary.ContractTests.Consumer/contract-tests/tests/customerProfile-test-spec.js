const { describe, it, before } = require('mocha');
const { createMockServer } = require('../utils/create-mock-server');
const client = require('../transactions/customerProfile-transactions');
const interactions = require('../interactions/customerProfile-interactions');

let mockServer;

describe('Customer Profile API Contract Tests; graphql endpoint', () => {
    before(async () => {
        mockServer = await createMockServer(process.env.CONSUMER_NAME, process.env.PROVIDER_NAME_CUSTOMER_PROFILE);
    });
    describe('Get valid graphql query data for a customer', () => {
        it('Customer Profile json is successfully returned', async () => {
            await mockServer.addInteraction(interactions.getGraphql);
            await client.getGraphql();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get valid graphql query data for an unexisting customer', () => {
        it('Empty array is successfully returned', async () => {
            await mockServer.addInteraction(interactions.getGraphqlNoData);
            await client.getGraphqlNoData();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
});
