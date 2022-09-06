const { somethingLike: like } = require('@pact-foundation/pact').Matchers;
const communicationData = require('../req-resp/communicationsServiceProxy');

module.exports = {
    postCloseAccount: {
        state: 'it has the ability to return close account json',
        uponReceiving: 'Post request to close account',
        withRequest: {
            method: 'POST',
            path: '/api/emails/send/close-account',
            body: communicationData.requests.postCloseAccount,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(communicationData.responses.postCloseAccount),
        },
    },

    postResetPassword: {
        state: 'it has the ability to return reset password json',
        uponReceiving: 'Post request to reset password',
        withRequest: {
            method: 'POST',
            path: '/api/emails/send/reset-password',
            body: communicationData.requests.postResetPassword,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(communicationData.responses.postResetPassword),
        },
    },

    getSmsContent: {
        state: 'it has the ability to return sms content by type',
        uponReceiving: 'Get request to return sms content',
        withRequest: {
            method: 'GET',
            path: '/api/sms/content/expired%20card',
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(communicationData.responses.getSmsContent),
        },
    },
    postSendSms: {
        state: 'it has the ability to submit sms for fake test number',
        uponReceiving: 'Post request to submit sms for fake test number',
        withRequest: {
            method: 'POST',
            path: '/api/sms/send',
            body: communicationData.requests.postSendSms,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(communicationData.responses.postSendSms),
        },
    },
    postSendSmsPaynowLink: {
        state: 'it has the ability to submit paynow link through sms for fake test number',
        uponReceiving: 'Post request to submit paynow link sms for fake test number',
        withRequest: {
            method: 'POST',
            path: '/api/sms/send/paynowlink',
            body: communicationData.requests.postSendSmsPaynowLink,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(communicationData.responses.postSendSmsPaynowLink),
        },
    },
    postSendEmailAccountPaidout: {
        state: 'it has the ability to submit account paidout link through email',
        uponReceiving: 'Post request to submit account paidout email',
        withRequest: {
            method: 'POST',
            path: '/api/emails/send/account-paidout',
            body: communicationData.requests.postSendEmailAccountPaidout,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(communicationData.responses.postSendEmailAccountPaidout),
        },
    },
};
