const { describe, it, before } = require('mocha');
const { createMockServer } = require('../utils/create-mock-server');
const client = require('../transactions/customerSummary-transactions');
const interactions = require('../interactions/customerSummary-interactions');

let mockServer;

describe('Customer Summary API Contract Tests; products Endpoint ', () => {
    before(async () => {
        mockServer = await createMockServer(process.env.CONSUMER_NAME, process.env.PROVIDER_NAME_CUSTOMER_SUMMARY);
    });
    describe('Get products data', () => {
        it('products json is successfully returned', async () => {
            await mockServer.addInteraction(interactions.getProducts);
            await client.getProducts();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get countries data', () => {
        it('Countries json is successfully returned', async () => {
            await mockServer.addInteraction(interactions.getCountries);
            await client.getCountries();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
});
