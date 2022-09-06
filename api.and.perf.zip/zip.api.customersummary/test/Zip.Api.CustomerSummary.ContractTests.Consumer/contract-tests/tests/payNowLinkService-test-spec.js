const { describe, it, before } = require('mocha');
const { createMockServer } = require('../utils/create-mock-server');
const client = require('../transactions/payNowLinkService-transactions');
const interactions = require('../interactions/payNowLinkService-interactions');

let mockServer;

describe('Pay Now Link Service Contract Tests; Consumer Endpoint ', () => {
    before(async () => {
        mockServer = await createMockServer(process.env.CONSUMER_NAME, process.env.PROVIDER_NAME_PAY_NOW_LINK_SERVICE);
    });
    describe('Post paynow link for consumer', () => {
        it('returns Json with Link to paynow', async () => {
            await mockServer.addInteraction(interactions.postPaynow);
            await client.postPaynow();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Post paynow link for consumer, invalid data: values as strings', () => {
        it('returns an error', async () => {
            await mockServer.addInteraction(interactions.postPaynowStringValues);
            await client.postPaynowStringValues();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Post paynow link for unexisting consumer', () => {
        it('returns an error', async () => {
            await mockServer.addInteraction(interactions.postPaynowUnexistingConsumerId);
            await client.postPaynowUnexistingConsumerId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
});
