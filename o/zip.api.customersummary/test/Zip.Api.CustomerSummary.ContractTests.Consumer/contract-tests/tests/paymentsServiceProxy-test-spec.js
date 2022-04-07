const { describe, it, before } = require('mocha');
const { createMockServer } = require('../utils/create-mock-server');
const client = require('../transactions/paymentsServiceProxy-transactions');
const interactions = require('../interactions/paymentsServiceProxy-interactions');

let mockServer;

describe('Payments Service Proxy Contract Tests; payments endpoints', () => {
    before(async () => {
        mockServer = await createMockServer(process.env.CONSUMER_NAME, process.env.PROVIDER_NAME_PAYMENTS_SERVICE_PROXY);
    });
    describe('Get valid payments by id ', () => {
        it('payments record is successfully returned', async () => {
            await mockServer.addInteraction(interactions.getPaymentsById);
            await client.getPaymentsById();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get valid payments by unexisting id ', () => {
        it('unexisting id error is returned', async () => {
            await mockServer.addInteraction(interactions.getPaymentsByUnexistingId);
            await client.getPaymentsByUnexistingId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get payments by invalid id ', () => {
        it('invalid id error is returned', async () => {
            await mockServer.addInteraction(interactions.getPaymentsByInvalidId);
            await client.getPaymentsByInvalidId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('Get valid payments by account', () => {
        it('payments records by account are successfully returned', async () => {
            await mockServer.addInteraction(interactions.getPaymentsByAccount);
            await client.getPaymentsByAccount();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get payments by unexisting account', () => {
        it('empty array is successfully returned', async () => {
            await mockServer.addInteraction(interactions.getPaymentsByUnexistingAccount);
            await client.getPaymentsByUnexistingAccount();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get valid payments by account with date range', () => {
        it('payments records by account within date range are successfully returned', async () => {
            await mockServer.addInteraction(interactions.getPaymentsByAccountDateRange);
            await client.getPaymentsByAccountDateRange();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get valid payments by date range', () => {
        it('payments records by date range are successfully returned', async () => {
            await mockServer.addInteraction(interactions.getPaymentsByDateRange);
            await client.getPaymentsByDateRange();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get payments by invalid date range', () => {
        it('empty array is successfully returned', async () => {
            await mockServer.addInteraction(interactions.getPaymentsByInvalidDateRange);
            await client.getPaymentsByInvalidDateRange();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('Get payments by Payment Batch Id date range', () => {
        it('payments records by Payment Batch Id are successfully returned', async () => {
            await mockServer.addInteraction(interactions.getPaymentsPaymentBatchId);
            await client.getPaymentsPaymentBatchId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get payments by invalid Payment Batch Id', () => {
        it('invalid payload error returned', async () => {
            await mockServer.addInteraction(interactions.getPaymentsInvalidPaymentBatchId);
            await client.getPaymentsInvalidPaymentBatchId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get payments by unexisting Payment Batch Id', () => {
        it('empty array is successfully returned', async () => {
            await mockServer.addInteraction(interactions.getPaymentsUnexistingPaymentBatchId);
            await client.getPaymentsUnexistingPaymentBatchId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('Get payments by Payment Batch Id & date range & account id', () => {
        it('payments records by Payment Batch Id & date range & account id are successfully returned', async () => {
            await mockServer.addInteraction(interactions.getPaymentsAllQueries);
            await client.getPaymentsAllQueries();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    // describe('Post payments to make a payment', () => {
    //     it('payments record created', async () => {
    //         await mockServer.addInteraction(interactions.postPayments);
    //         await client.postPayments();
    //     });
    //     it('should validate the interactions and create a contract', () => mockServer.verify());
    // });

    describe('Get payment methods by customer id', () => {
        it('payment methods by customer id are successfully returned', async () => {
            await mockServer.addInteraction(interactions.getPaymentMethods);
            await client.getPaymentMethods();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get payment methods by unexisting customer id', () => {
        it('empty array is returned', async () => {
            await mockServer.addInteraction(interactions.getPaymentMethodsUnexistingCustomerId);
            await client.getPaymentMethodsUnexistingCustomerId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get payment methods by customer id and includeFailedAttempted flag set to True', () => {
        it('payment methods by customer id and includeFailedAttempted flag set to True ' +
            'are successfully returned', async () => {
            await mockServer.addInteraction(interactions.getPaymentMethodsFailedAttemptedTrue);
            await client.getPaymentMethodsFailedAttemptedTrue();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get payment methods by customer id and includeFailedAttempted flag set to False', () => {
        it('payment methods by customer id and includeFailedAttempted flag set to False ' +
            'are successfully returned', async () => {
            await mockServer.addInteraction(interactions.getPaymentMethodsFailedAttemptedFalse);
            await client.getPaymentMethodsFailedAttemptedFalse();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('Get payment methods by guid', () => {
        it('payment methods by guid are successfully returned', async () => {
            await mockServer.addInteraction(interactions.getPaymentMethodsGuid);
            await client.getPaymentMethodsGuid();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get payment methods by unexisting guid', () => {
        it('error returned', async () => {
            await mockServer.addInteraction(interactions.getPaymentMethodsUnexistingGuid);
            await client.getPaymentMethodsUnexistingGuid();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get payment methods by invalid guid', () => {
        it('error returned', async () => {
            await mockServer.addInteraction(interactions.getPaymentMethodsInvalidGuid);
            await client.getPaymentMethodsInvalidGuid();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    // describe('Post payment methods to create a new payment method', () => {
    //     it('payments method record created', async () => {
    //         await mockServer.addInteraction(interactions.postPaymentMethods);
    //         await client.postPaymentMethods();
    //     });
    //     it('should validate the interactions and create a contract', () => mockServer.verify());
    // });
    describe('Post payment methods invalid data in body', () => {
        it('payments method record created', async () => {
            await mockServer.addInteraction(interactions.postInvalidPaymentMethods);
            await client.postInvalidPaymentMethods();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('Delete payment methods unexisting guid', () => {
        it('payments method record created', async () => {
            await mockServer.addInteraction(interactions.deletePaymentMethodUnexistingId);
            await client.deletePaymentMethodUnexistingId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Delete payment methods invalid  guid', () => {
        it('payments method record created', async () => {
            await mockServer.addInteraction(interactions.deletePaymentMethodInvalidId);
            await client.deletePaymentMethodInvalidId();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('Refund payment Fat Zebra error', () => {
        it('payments method record created', async () => {
            await mockServer.addInteraction(interactions.postRefundFatZebraError);
            await client.postRefundFatZebraError();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Refund payment unexisting Guid', () => {
        it('payments method record created', async () => {
            await mockServer.addInteraction(interactions.postRefundUnexistingGuid);
            await client.postRefundUnexistingGuid();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('Get payment batches by date', () => {
        it('payments method record created', async () => {
            await mockServer.addInteraction(interactions.getPaymentBatchesDate);
            await client.getPaymentBatchesDate();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get payment batches by date', () => {
        it('payments method record created', async () => {
            await mockServer.addInteraction(interactions.getPaymentBatchesDate);
            await client.getPaymentBatchesDate();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get payment batches by date, skip flag equals true', () => {
        it('payments method record created', async () => {
            await mockServer.addInteraction(interactions.getPaymentBatchesDateSkipTrue);
            await client.getPaymentBatchesDateSkipTrue();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get payment batches by date, skip flag equals false', () => {
        it('payments method record created', async () => {
            await mockServer.addInteraction(interactions.getPaymentBatchesDateSkipFalse);
            await client.getPaymentBatchesDateSkipFalse();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get payment batches by date, take one', () => {
        it('payments method record created', async () => {
            await mockServer.addInteraction(interactions.getPaymentBatchesDateTakeOne);
            await client.getPaymentBatchesDateTakeOne();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get payment batches by date, take three', () => {
        it('payments method record created', async () => {
            await mockServer.addInteraction(interactions.getPaymentBatchesDateTakeThree);
            await client.getPaymentBatchesDateTakeThree();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('Get paynow hash', () => {
        it('paynow hash record returned', async () => {
            await mockServer.addInteraction(interactions.getPaynowHash);
            await client.getPaynowHash();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get paynow hash invalid data', () => {
        it('paynow validation error returned', async () => {
            await mockServer.addInteraction(interactions.getPaynowHashInvalidData);
            await client.getPaynowHashInvalidData();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

    describe('Get provider hash', () => {
        it('provider hash record returned', async () => {
            await mockServer.addInteraction(interactions.getProviderHash);
            await client.getProviderHash();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });
    describe('Get provider hash invalid data', () => {
        it('provider validation error returned', async () => {
            await mockServer.addInteraction(interactions.getProviderHashInvalidData);
            await client.getProviderHashInvalidData();
        });
        it('should validate the interactions and create a contract', () => mockServer.verify());
    });

});
