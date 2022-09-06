const { somethingLike: like } = require('@pact-foundation/pact').Matchers;
const paymentsData = require('../req-resp/paymentsServiceProxy');

module.exports = {
    getPaymentsById: {
        state: 'it has the ability to return correct data by id',
        uponReceiving: 'GET request to get payments data by  id',
        withRequest: {
            method: 'GET',
            path: '/payments/2828',
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(paymentsData.responses.paymentsById)
        },
    },
    getPaymentsByUnexistingId: {
        state: 'it returns an error for unexisting id',
        uponReceiving: 'GET request to get payments data by unexisting id',
        withRequest: {
            method: 'GET',
            path: '/payments/99999999999883344',
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 404,
            body: like(paymentsData.responses.paymentsByUnexistingId)
        },
    },
    getPaymentsByInvalidId: {
        state: 'it returns an error for invalid id',
        uponReceiving: 'GET request to get payments data by invalid id',
        withRequest: {
            method: 'GET',
            path: '/payments/noexist',
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 400,
            body: like(paymentsData.responses.paymentsByInvalidId)
        },
    },

    getPaymentsByAccount: {
        state: 'it has the ability to return correct data by account',
        uponReceiving: 'GET request to get payments data by  account',
        withRequest: {
            method: 'GET',
            path: '/payments',
            query: {accountid: '10'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(paymentsData.responses.paymentsByAccount)
        },
    },
    getPaymentsByUnexistingAccount: {
        state: 'it returns empty array for unexisting account',
        uponReceiving: 'GET request to get payments data by unexisting account',
        withRequest: {
            method: 'GET',
            path: '/payments',
            query: {accountid: 'wowwowowowwowowoowowowooowowowoowowowowowoowow'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(paymentsData.responses.paymentsByUnexistingAccount)
        },
    },

    getPaymentsByAccountDateRange: {
        state: 'it has the ability to return correct data by account with date range',
        uponReceiving: 'GET request to get payments data by  account and date range',
        withRequest: {
            method: 'GET',
            path: '/payments',
            query: {accountid: '123', fromDate:'2020-11-17', toDate:'2020-11-19'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(paymentsData.responses.paymentsByAccountDateRange)
        },
    },
    getPaymentsByDateRange: {
        state: 'it has the ability to return correct data by date range',
        uponReceiving: 'GET request to get payments data by date range',
        withRequest: {
            method: 'GET',
            path: '/payments',
            query: {fromDate:'2021-04-03', toDate:'2021-04-04'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(paymentsData.responses.paymentsByDateRange)
        },
    },
    getPaymentsByInvalidDateRange: {
        state: 'it returns empty array for invalid date range request',
        uponReceiving: 'GET request with invalid date range',
        withRequest: {
            method: 'GET',
            path: '/payments',
            query: {fromDate:'9999-04-03', toDate:'2021-04-04'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(paymentsData.responses.paymentsByInvalidDateRange)
        },
    },

    getPaymentsPaymentBatchId: {
        state: 'it has the ability to return correct data by Payment Batch Id',
        uponReceiving: 'GET request with valid Payment Batch Id',
        withRequest: {
            method: 'GET',
            path: '/payments',
            query: {PaymentBatchId: '0891da65-b462-41ae-94f7-76985dc76c4e'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(paymentsData.responses.paymentsPaymentBatchId)
        },
    },
    getPaymentsInvalidPaymentBatchId: {
        state: 'it has the ability to return validation error for invalid Payment Batch Id',
        uponReceiving: 'GET request with invalid Payment Batch Id',
        withRequest: {
            method: 'GET',
            path: '/payments',
            query: {PaymentBatchId: 'test'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 400,
            body: paymentsData.responses.paymentsInvalidPaymentBatchId
        },
    },
    getPaymentsUnexistingPaymentBatchId: {
        state: 'it has the ability to return empty array when unexisting Payment Batch Id is provided',
        uponReceiving: 'GET request with unexisting Payment Batch Id',
        withRequest: {
            method: 'GET',
            path: '/payments',
            query: {PaymentBatchId: '0339b499-aa98-46f2-b9d8-1eedb22c32b1'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(paymentsData.responses.paymentsUnexistingPaymentBatchId)
        },
    },

    getPaymentsAllQueries: {
        state: 'it has the ability to return valid data when Payment Batch Id & date range & account id are provided',
        uponReceiving: 'GET request with valid Payment Batch Id & date range & account id',
        withRequest: {
            method: 'GET',
            path: '/payments',
            query: {accountid: '140654', fromDate:'2020-04-01', toDate:'2021-04-04',
                paymentBatchId:'48661050-1a74-4601-944e-813046c7eb6f'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: paymentsData.responses.paymentsAllQueries
        },

    },

    postPayments: {
        state: 'it has the ability to create payments',
        uponReceiving: 'POST request with valid body',
        withRequest: {
            method: 'POST',
            path: '/payments',
            body: paymentsData.requests.postPayments,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: paymentsData.responses.postPayments
        },

    },

    getPaymentMethods: {
        state: 'it has the ability to return valid data for payment methods by customer id',
        uponReceiving: 'GET request with valid customer Id',
        withRequest: {
            method: 'GET',
            path: '/paymentmethods',
            query: {customerId: '79779'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: paymentsData.responses.getPaymentMethods
        },
    },
    getPaymentMethodsUnexistingCustomerId: {
        state: 'it has the ability to return empty array for payment methods by unexisting customer id',
        uponReceiving: 'GET request with unexisting customer Id',
        withRequest: {
            method: 'GET',
            path: '/paymentmethods',
            query: {customerId: '7977990909066'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: paymentsData.responses.getPaymentMethodsUnexistingCustomerId
        },
    },
    getPaymentMethodsFailedAttemptedTrue: {
        state: 'it has the ability to return empty array for payment methods by customer id and ' +
            'includeFailedAttempted flag set to True',
        uponReceiving: 'GET request with customer Id and includeFailedAttempted flag set to True',
        withRequest: {
            method: 'GET',
            path: '/paymentmethods',
            query: {customerId: '516021', includeFailedAttempted:'true'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: paymentsData.responses.getPaymentMethodsFailedAttemptedTrue
        },
    },
    getPaymentMethodsFailedAttemptedFalse: {
        state: 'it has the ability to return empty array for payment methods by customer id and ' +
            'includeFailedAttempted flag set to False',
        uponReceiving: 'GET request with customer Id and includeFailedAttempted flag set to False',
        withRequest: {
            method: 'GET',
            path: '/paymentmethods',
            query: {customerId: '302353', includeFailedAttempted:'false'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: paymentsData.responses.getPaymentMethodsFailedAttemptedFalse
        },
    },

    getPaymentMethodsGuid: {
        state: 'it has the ability to return valid data for payment methods by guid',
        uponReceiving: 'GET request with valid customer guid',
        withRequest: {
            method: 'GET',
            path: '/paymentmethods/f82d0cf0-3382-4e27-a4dc-001da80014a4',
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: paymentsData.responses.getPaymentMethodsGuid
        },
    },
    getPaymentMethodsUnexistingGuid: {
        state: 'it has the ability to return error for payment methods by unexisting guid',
        uponReceiving: 'GET request with unexisting customer guid',
        withRequest: {
            method: 'GET',
            path: '/paymentmethods/f82d0cf0-3382-4e27-a4dc-001da80014a6',
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 404,
        },
    },
    getPaymentMethodsInvalidGuid: {
        state: 'it has the ability to return error for payment methods by invalid guid',
        uponReceiving: 'GET request with unexisting customer guid',
        withRequest: {
            method: 'GET',
            path: '/paymentmethods/f82d0cf0-3382-4e27-a4dc-001d',
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 400,
        },
    },

    postPaymentMethods: {
        state: 'it has the ability to create new payment methods',
        uponReceiving: 'POST request with valid data',
        withRequest: {
            method: 'POST',
            path: '/paymentmethods',
            body: paymentsData.requests.postPaymentMethods,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 201,
            body: like(paymentsData.responses.postPaymentMethods)
        },
    },
    postInvalidPaymentMethods: {
        state: 'it has the ability to return validation error for invalid payment methods',
        uponReceiving: 'POST request with invalid data',
        withRequest: {
            method: 'POST',
            path: '/paymentmethods',
            body: paymentsData.requests.postInvalidPaymentMethods,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 400,
        },
    },

    deletePaymentMethodUnexistingId: {
        state: 'it has the ability not to return anything for request with unexisting id',
        uponReceiving: 'DELETE request with no data',
        withRequest: {
            method: 'DELETE',
            path: '/paymentmethods/3dca16e8-20f9-4301-a79a-002384695443',
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 204,
        },
    },
    deletePaymentMethodInvalidId: {
        state: 'it has the ability return validation error for request with invalid id',
        uponReceiving: 'DELETE request validation error',
        withRequest: {
            method: 'DELETE',
            path: '/paymentmethods/test',
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 400,
            body: paymentsData.responses.deletePaymentMethodInvalidId
        },
    },

    postRefundFatZebraError: {
        state: 'it has the ability return validation error from Fat Zebra',
        uponReceiving: 'POST request 3rd vendor validation error',
        withRequest: {
            method: 'POST',
            path: '/662f6753-f115-4708-bb99-fd89935f3472/refund',
            body: {},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 404,
        },
    },
    postRefundUnexistingGuid: {
        state: 'it has the ability return validation error for unexisting GUID',
        uponReceiving: 'POST request validation error',
        withRequest: {
            method: 'POST',
            path: '/662f6753-f115-4708-bb99-fd89935f3478/refund',
            body: {},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 404,
        },
    },

    getPaymentBatchesDate: {
        state: 'it has the ability return payment batches by date',
        uponReceiving: 'GET request returns valid data',
        withRequest: {
            method: 'GET',
            path: '/paymentBatches',
            query: {startDate: '2020-11-11', endDate: '2020-11-15'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: paymentsData.responses.getPaymentBatchesDate
        },
    },
    getPaymentBatchesDateSkipTrue: {
        state: 'it has the ability return payment batches by date and skip flag set to True',
        uponReceiving: 'GET request returns valid data with skipped dups',
        withRequest: {
            method: 'GET',
            path: '/paymentBatches',
            query: {startDate: '2020-11-11', endDate: '2020-11-15', skip: '1'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: paymentsData.responses.getPaymentBatchesDateSkipTrue
        },
    },
    getPaymentBatchesDateSkipFalse: {
        state: 'it has the ability return payment batches by date and skip flag set to False',
        uponReceiving: 'GET request returns valid data without skipped dups',
        withRequest: {
            method: 'GET',
            path: '/paymentBatches',
            query: {startDate: '2020-11-11', endDate: '2020-11-15', skip: '0'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: paymentsData.responses.getPaymentBatchesDateSkipFalse
        },
    },
    getPaymentBatchesDateTakeOne: {
        state: 'it has the ability return payment batches by date and take 1',
        uponReceiving: 'GET request returns valid data without skipped dups and take 1',
        withRequest: {
            method: 'GET',
            path: '/paymentBatches',
            query: {startDate: '2020-11-11', endDate: '2020-11-15', skip: '0', take:'1'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(paymentsData.responses.getPaymentBatchesDateTakeOne)
        },
    },
    getPaymentBatchesDateTakeThree: {
        state: 'it has the ability return payment batches by date and take 3',
        uponReceiving: 'GET request returns valid data without skipped dups and take set to 3',
        withRequest: {
            method: 'GET',
            path: '/paymentBatches',
            query: {startDate: '2020-11-11', endDate: '2020-11-15', skip: '0', take:'3'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(paymentsData.responses.getPaymentBatchesDateTakeThree)
        },
    },

    getPaynowHash: {
        state: 'it has the ability return payment paynow hash',
        uponReceiving: 'GET request returns valid data with payment paynow hash',
        withRequest: {
            method: 'GET',
            path: '/paynow/hash',
            query: {"product":"ZipMoney", "country": "AU",
                "reference": "Batch:DefaultCard:Arrears:DD-A200402-2to250F10T2000N0:158656",
                "amount": "0.01", "paymentType":"gradual"},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(paymentsData.responses.getPaynowHash)
        },
    },
    getPaynowHashInvalidData: {
        state: 'it has the ability return validation error for invalid paynowhash',
        uponReceiving: 'GET request validation error for invalid fields for paynow hash',
        withRequest: {
            method: 'GET',
            path: '/paynow/hash',
            query: {product:"test",country:"test",reference:"test",amount:"test",paymentType:"test"},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 400,
            body: paymentsData.responses.getPaynowHashInvalidData
        },
    },
    getProviderHash: {
        state: 'it has the ability return payment Provider hash',
        uponReceiving: 'GET request returns valid data with payment Provider hash',
        withRequest: {
            method: 'GET',
            path: '/provider/hash',
            query: {"product":"ZipMoney", "country": "AU",
                "reference": "Batch:DefaultCard:Arrears:DD-A200402-2to250F10T2000N0:158656",
                "amount": "0.01", "paymentType":"gradual"},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: paymentsData.responses.getProviderHash
        },
    },
    getProviderHashInvalidData: {
        state: 'it has the ability return validation error for invalid provider hash',
        uponReceiving: 'GET request validation error for invalid fields for provider hash',
        withRequest: {
            method: 'GET',
            path: '/provider/hash',
            query: {product:"test",country:"test",reference:"test",amount:"test",paymentType:"test"},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 400,
            body: paymentsData.responses.getProviderHashInvalidData
        },
    },


};
