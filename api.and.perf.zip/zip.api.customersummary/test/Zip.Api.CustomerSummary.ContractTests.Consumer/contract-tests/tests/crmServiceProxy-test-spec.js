const { describe, it, before } = require('mocha');
const { createMockServer } = require('../utils/create-mock-server');
const client = require('../transactions/crmServiceProxy-transactions');
const interactions = require('../interactions/crmServiceProxy-interactions');

let mockServer;

describe('Crm Service Proxy contract tests, comment endpoint ', () => {
    before(async () => {
        mockServer = await createMockServer(process.env.CONSUMER_NAME, process.env.PROVIDER_NAME_CRM_SERVICE_PROXY);
    });
    describe('Post comment request with valid data', () => {
        it('Created Comment json is successfully returned', async () => {
            await mockServer.addInteraction(interactions.postComment);
            await client.postComment();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Post comment request without required params', () => {
        it('returns Validation error', async () => {
            await mockServer.addInteraction(interactions.postCommentMissingParams);
            await client.postCommentMissingParams();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Post comment request invalid data', () => {
        it('returns Validation error', async () => {
            await mockServer.addInteraction(interactions.postCommentInvalidData);
            await client.postCommentInvalidData();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('Get comment request with valid data', () => {
        it('Comment json is successfully returned', async () => {
            await mockServer.addInteraction(interactions.getComment);
            await client.getComment();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get comment request with unexisting customerid', () => {
        it('Comment json with empty nested array returned', async () => {
            await mockServer.addInteraction(interactions.getCommentUnexistingCustomerId);
            await client.getCommentUnexistingCustomerId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get comment request with invalid customerid', () => {
        it('Comment json with validation error returned', async () => {
            await mockServer.addInteraction(interactions.getCommentInvalidCustomerId);
            await client.getCommentInvalidCustomerId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get comment request with invalid parameters', () => {
        it('Comment json with validation error returned', async () => {
            await mockServer.addInteraction(interactions.getCommentInvalidParams);
            await client.getCommentInvalidParams();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
});
