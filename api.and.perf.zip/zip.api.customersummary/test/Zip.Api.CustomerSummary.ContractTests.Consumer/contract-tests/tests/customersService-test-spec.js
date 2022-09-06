const { describe, it, before } = require('mocha');
const { createMockServer } = require('../utils/create-mock-server');
const client = require('../transactions/customersService-transactions');
const interactions = require('../interactions/customersService-interactions');

let mockServer;

describe('Customers Service API Contract Tests; Validate customer email; validate endpoint', () => {
    before(async () => {
        mockServer = await createMockServer(process.env.CONSUMER_NAME, process.env.PROVIDER_NAME_CUSTOMERS_SERVICE);
    });
    describe('PUT validate email response for a clientId', () => {
        it('clientId json is successfully returned', async () => {
            await mockServer.addInteraction(interactions.putEmailValidate);
            await client.putEmailValidate();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('PUT validate for already existing email for a clientId', () => {
        it('clientId json is successfully returned for existing client', async () => {
            await mockServer.addInteraction(interactions.putEmailValidateExistingEmail);
            await client.putEmailValidateExistingEmail();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('PUT validate for invalid email', () => {
        it('clientId json is successfully returned for client with invalid email', async () => {
            await mockServer.addInteraction(interactions.putEmailValidateInvalidEmail);
            await client.putEmailValidateInvalidEmail();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('PUT validate for invalid customerid', () => {
        it('clientId json is successfully returned for invalid customerid', async () => {
            await mockServer.addInteraction(interactions.putEmailValidateInvalidCustomerid);
            await client.putEmailValidateInvalidCustomerId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });



    describe('PUT update customer email', () => {
        it('customer email is successfully updated', async () => {
            await mockServer.addInteraction(interactions.putEmailUpdate);
            await client.putEmailUpdate();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('PUT update customer email for already existing email', () => {
        it('email update; duplicate email error returned', async () => {
            await mockServer.addInteraction(interactions.putEmailUpdateExistingEmail);
            await client.putEmailUpdateExistingEmail();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('PUT update customer email for invalid email', () => {
        it('email update; invalid email error returned', async () => {
            await mockServer.addInteraction(interactions.putEmailUpdateInvalidEmail);
            await client.putEmailUpdateInvalidEmail();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('PUT update customer email for invalid customerid', () => {
        it('email update; invalid customerid error returned', async () => {
            await mockServer.addInteraction(interactions.putEmailUpdateInvalidCustomerid);
            await client.putEmailUpdateInvalidCustomerid();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });


    describe('PUT validate Mobile response for a clientId', () => {
        it('clientId json is successfully returned', async () => {
            await mockServer.addInteraction(interactions.putMobileValidate);
            await client.putMobileValidate();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('PUT validate for already existing Mobile for a clientId', () => {
        it('clientId json is successfully returned for existing client', async () => {
            await mockServer.addInteraction(interactions.putMobileValidateExistingMobile);
            await client.putMobileValidateExistingMobile();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('PUT validate for invalid Mobile', () => {
        it('clientId json is successfully returned for client with invalid Mobile', async () => {
            await mockServer.addInteraction(interactions.putMobileValidateInvalidMobile);
            await client.putMobileValidateInvalidMobile();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('PUT validate for invalid customerid', () => {
        it('clientId json is successfully returned for invalid customerid', async () => {
            await mockServer.addInteraction(interactions.putMobileValidateInvalidCustomerid);
            await client.putMobileValidateInvalidCustomerid();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('PUT update customer Mobile', () => {
        it('customer Mobile is successfully updated', async () => {
            await mockServer.addInteraction(interactions.putMobileUpdate);
            await client.putMobileUpdate();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('PUT update customer Mobile for already existing Mobile', () => {
        it('Mobile update; duplicate Mobile error returned', async () => {
            await mockServer.addInteraction(interactions.putMobileUpdateExistingMobile);
            await client.putMobileUpdateExistingMobile();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('PUT update customer Mobile for invalid Mobile', () => {
        it('Mobile update; invalid Mobile error returned', async () => {
            await mockServer.addInteraction(interactions.putMobileUpdateInvalidMobile);
            await client.putMobileUpdateInvalidMobile();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('PUT update customer Mobile for invalid customerid', () => {
        it('Mobile update; invalid customerid error returned', async () => {
            await mockServer.addInteraction(interactions.putMobileUpdateInvalidCustomerid);
            await client.putMobileUpdateInvalidCustomerid();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
});

