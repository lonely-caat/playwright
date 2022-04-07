const { somethingLike: like } = require('@pact-foundation/pact').Matchers;
const payNow = require('../req-resp/payNowLinkService');

module.exports = {
    postPaynow: {
        state: 'it has the ability to return countries json',
        uponReceiving: 'Post request to return paynow link',
        withRequest: {
            method: 'POST',
            path: '/api/paynowlink/consumer',
            body : payNow.requests.postPaynow,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(payNow.responses.postPaynow),
        },
    },
    postPaynowStringValues: {
        state: 'it has the ability to return an obscure validation error for invalid data type',
        uponReceiving: 'Post request to return paynow link with values as strings',
        withRequest: {
            method: 'POST',
            path: '/api/paynowlink/consumer',
            body : payNow.requests.postPaynowStringValues,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 400,
            body: like(payNow.responses.postPaynowStringValues),
        },
    },
    postPaynowUnexistingConsumerId: {
        state: 'it has the ability to return validation error for unexisting consumer id',
        uponReceiving: 'Post request to return paynow link with unexisting consumer id value',
        withRequest: {
            method: 'POST',
            path: '/api/paynowlink/consumer',
            body : payNow.requests.postPaynowUnexistingConsumerId,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 500,
            body: like(payNow.responses.postPaynowUnexistingConsumerId),
        },
    },
};
