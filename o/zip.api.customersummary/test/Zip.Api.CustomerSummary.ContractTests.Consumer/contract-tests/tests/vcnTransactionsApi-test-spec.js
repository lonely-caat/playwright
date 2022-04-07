const { describe, it, before } = require('mocha');
const { createMockServer } = require('../utils/create-mock-server');
const client = require('../transactions/vcnTransactionsApi-transactions');
const interactions = require('../interactions/vcnTransactionsApi-interactions');

let mockServer;

describe('Vcn Transactions API Contract Tests; transactions endpoint', () => {
    before(async () => {
        mockServer = await createMockServer(process.env.CONSUMER_NAME, process.env.PROVIDER_NAME_VCN_TRANSACTIONS_API);
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
