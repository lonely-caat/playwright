const { describe, it, before } = require('mocha');
const { createMockServer } = require('../utils/create-mock-server');
const client = require('../transactions/communicationsServiceProxy-transactions');
const interactions = require('../interactions/communicationsServiceProxy-interactions');

let mockServer;

describe('Communications API Contract Tests; emails/sms Endpoints ', () => {
    before(async () => {
        mockServer = await createMockServer(process.env.CONSUMER_NAME, process.env.PROVIDER_NAME_COMMUNICATIONS_SERVICE_PROXY);
    });
    // TODO: add negative test cases
    describe('Post close account', () => {
        it('Close account json is successfully returned', async () => {
            await mockServer.addInteraction(interactions.postCloseAccount);
            await client.postCloseAccount();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Post Reset password', () => {
        it('Post reset password json returned', async () => {
            await mockServer.addInteraction(interactions.postResetPassword);
            await client.postResetPassword();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('Get sms content', () => {
        it('get sms content returned', async () => {
            await mockServer.addInteraction(interactions.getSmsContent);
            await client.getSmsContent();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Post send sms', () => {
        it('post sms json returned', async () => {
            await mockServer.addInteraction(interactions.postSendSms);
            await client.postSendSms();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Post send sms paynow link', () => {
        it('post sms json returned for paynow link', async () => {
            await mockServer.addInteraction(interactions.postSendSmsPaynowLink);
            await client.postSendSmsPaynowLink();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Post send email paidout', () => {
        it('post email json returned for account paidout', async () => {
            await mockServer.addInteraction(interactions.postSendEmailAccountPaidout);
            await client.postSendEmailAccountPaidout();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
});
