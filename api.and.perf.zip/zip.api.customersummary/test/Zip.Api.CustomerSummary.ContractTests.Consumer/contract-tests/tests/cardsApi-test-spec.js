const { describe, it, before } = require('mocha');
const { createMockServer } = require('../utils/create-mock-server');
const client = require('../transactions/cardsApi-transactions');
const interactions = require('../interactions/cardsApi-interactions');

let mockServer;

describe.only('Cards API Contract Tests; cards endpoint', () => {
    before(async () => {
        mockServer = await createMockServer(process.env.CONSUMER_NAME, process.env.PROVIDER_NAME_CARDS_API);
    });
    describe('Get valid card data by card id', () => {
        it('Card json is successfully returned', async () => {
            await mockServer.addInteraction(interactions.getCardData);
            await client.getCardData();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get card data by unexisting card id', () => {
        it('Card json is successfully returned', async () => {
            await mockServer.addInteraction(interactions.getUnexistingCardData);
            await client.getUnexistingCardData();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('Get valid card data by external id', () => {
        it('Card json is successfully returned', async () => {
            await mockServer.addInteraction(interactions.getCardDataByExternalId);
            await client.getCardDataByExternalId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get card data by unexisting external id', () => {
        it('Card json is successfully returned', async () => {
            await mockServer.addInteraction(interactions.getCardDataByUnexistingExternalId);
            await client.getCardDataByUnexistingExternalId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('Get valid card data by customer id', () => {
        it('Card json is successfully returned', async () => {
            await mockServer.addInteraction(interactions.getCardDataByCustomerId);
            await client.getCardDataByCustomerId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get card data by unexisting customer id', () => {
        it('Card json is successfully returned', async () => {
            await mockServer.addInteraction(interactions.getCardDataByUnexistingCustomerId);
            await client.getCardDataByUnexistingCustomerId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('Put block valid card', () => {
        it('card is blocked', async () => {
            await mockServer.addInteraction(interactions.putBlockValidCard);
            await client.putBlockValidCard();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Put block unexisting card', () => {
        it('card is blocked', async () => {
            await mockServer.addInteraction(interactions.putBlockUnexistingCard);
            await client.putBlockUnexistingCard();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Put block unexisting customerId', () => {
        it('card is blocked', async () => {
            await mockServer.addInteraction(interactions.putBlockUnexistingCustomerId);
            await client.putBlockUnexistingCustomerId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('Put unblock valid card', () => {
        it('card is blocked', async () => {
            await mockServer.addInteraction(interactions.putUnblockValidCard);
            await client.putUnBlockValidCard();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Put unblock unexisting card', () => {
        it('card is blocked', async () => {
            await mockServer.addInteraction(interactions.putUnblockUnexistingCard);
            await client.putUnBlockUnexistingCard();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Put unblock unexisting customerId', () => {
        it('card is blocked', async () => {
            await mockServer.addInteraction(interactions.putUnblockUnexistingCustomerId);
            await client.putUnBlockUnexistingCustomerId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    // based on https://gitlab.com/zip-au/payments/zip.api.cards/-/merge_requests/448#note_562093444 token transition
    // does not work on dev/sandbox only prod
    // describe('Post token transition valid card', () => {
    //     it('card is transitioned', async () => {
    //         await mockServer.addInteraction(interactions.postTransitionValidCard);
    //         await client.postTransitionValidCard();
    //     });
    //     it('should validate the interactions and create a contract', () => mockServer.verify());
    // });
    describe('Post token transition invalid card', () => {
        it('card is transitioned', async () => {
            await mockServer.addInteraction(interactions.postTransitionInValidCard);
            await client.postTransitionInValidCard();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

});
