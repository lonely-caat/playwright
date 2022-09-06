const { describe, it, before } = require('mocha');
const { createMockServer } = require('../utils/create-mock-server');
const client = require('../transactions/paymentWebhookApi-transactions');
const interactions = require('../interactions/paymentWebhookApi-interactions');

let mockServer;

describe('Payment WebhookApi API Contract Tests; transactions endpoint', () => {
    before(async () => {
        mockServer = await createMockServer(process.env.CONSUMER_NAME, process.env.PROVIDER_NAME_PAYMENT_WEBHOOK_API);
    });
    describe('Get valid transaction by externalid ', () => {
        it('transaction record is successfully returned', async () => {
            await mockServer.addInteraction(interactions.getTransactionsByExternalId);
            await client.getTransactionsByExternalId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get invalid transaction by unexisting externalid ', () => {
        it('no error returned for invalid externalid', async () => {
            await mockServer.addInteraction(interactions.getTransactionsByUnexistingExternalId);
            await client.getTransactionsByUnexistingExternalId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('Get valid transaction by nrid ', () => {
        it('transaction record by nrid is successfully returned', async () => {
            await mockServer.addInteraction(interactions.getTransactionsByNRID);
            await client.getTransactionsByNRID();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get invalid transaction by unexisting nrid ', () => {
        it('no error returned for invalid nrid', async () => {
            await mockServer.addInteraction(interactions.getTransactionsByUnexistingNRID);
            await client.getTransactionsByUnexistingNRID();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

});
