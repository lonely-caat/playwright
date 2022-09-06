const { somethingLike: like } = require('@pact-foundation/pact').Matchers;
const userData = require('../req-resp/identityServiceProxy');

module.exports = {
    getUser: {
        state: 'it has the ability to return users json',
        uponReceiving: 'Get request to return users by email',
        withRequest: {
            method: 'GET',
            path: '/api/user/email',
            query: {email:'max.bilichenko@zip.co'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(userData.responses.getUser),
        },
    },
    getUnexistingUser: {
        state: 'it has the ability to return users json error message',
        uponReceiving: 'json error returns users by email',
        withRequest: {
            method: 'GET',
            path: '/api/user/email',
            query: {email:'bastest@zip.co'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 404,
            body: userData.responses.getUnexistingUser,
        },
    },
    getEmptyUser: {
        state: 'it has the ability to return validation error',
        uponReceiving: 'Get request to return users by empty email will return an error',
        withRequest: {
            method: 'GET',
            path: '/api/user/email',
            query: {email:''},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 400,
            body: userData.responses.getEmptyUser,
        },
    },
};
