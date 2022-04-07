const { describe, it, before } = require('mocha');
const { createMockServer } = require('../utils/create-mock-server');
const client = require('../transactions/tango-transactions');
const interactions = require('../interactions/tango-interactions');

let mockServer;

describe('Tango API Contract Tests; Accounts and Variations Endpoint ', () => {
    before(async () => {
        mockServer = await createMockServer(process.env.CONSUMER_NAME, process.env.PROVIDER_NAME_TANGO);
    });
    describe('Get accounts data by accountid', () => {
        it('returns json with account data for the given account', async () => {
            await mockServer.addInteraction(interactions.getAccounts);
            await client.getAccounts();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get accounts data by accountid with asAtDate query string', () => {
        it('returns json with account data for the given account and asAtDate query string', async () => {
            await mockServer.addInteraction(interactions.getAccountsAsAtDate);
            await client.getAccountsAsAtDate();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get accounts data by accountid; unexisting account', () => {
        it('returns string with an unexisting account error', async () => {
            await mockServer.addInteraction(interactions.getUnexistingAccounts);
            await client.getUnexistingAccounts();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('Get variations data with valid query params to return data', () => {
        it('returns json with variations data for the given parameters', async () => {
            await mockServer.addInteraction(interactions.getVariations);
            await client.getVariations();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get variations data with unexisting account hash', () => {
        it('returns empty array', async () => {
            await mockServer.addInteraction(interactions.getVariationsUnexistingAccountHash);
            await client.getVariationsUnexistingAccountHash();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Post variations data to create a new record', () => {
        it('returns json with variations data for the given parameters', async () => {
            await mockServer.addInteraction(interactions.postVariations);
            await client.postVariations();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Post variations invalid body', () => {
        it('returns json with validation requirements', async () => {
            await mockServer.addInteraction(interactions.postVariationsInvalidBody);
            await client.postVariationsInvalidBody();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('Get variations data by directDebitId', () => {
        it('returns json data by directDebitId', async () => {
            await mockServer.addInteraction(interactions.getVariationsDirectDebitId);
            await client.getVariationsDirectDebitId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get variations data by unexisting directDebitId', () => {
        it('returns json data by unexisting directDebitId', async () => {
            await mockServer.addInteraction(interactions.getVariationsUnexistingDirectDebitId);
            await client.getVariationsUnexistingDirectDebitId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('Put variations data by directDebitId', () => {
        it('returns updated json data for directDebitId', async () => {
            await mockServer.addInteraction(interactions.putVariationsDirectDebitId);
            await client.putVariationsDirectDebitId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Put variations data by directDebitId where DirectDebitID does not match account hash', () => {
        it('returns json error for incorrect match', async () => {
            await mockServer.addInteraction(interactions.putVariationsDirectDebitIdNoMatch);
            await client.putVariationsDirectDebitIdNoMatch();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
});
