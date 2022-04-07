const { somethingLike: like } = require('@pact-foundation/pact').Matchers;
const commentData = require('../req-resp/crmServiceProxy');

module.exports = {
    postComment: {
        state: 'it has the ability to create comment json',
        uponReceiving: 'POST request to create comment',
        withRequest: {
            method: 'POST',
            path: '/api/comment',
            body: commentData.requests.postComment,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(commentData.responses.postComment),
        },
    },
    postCommentMissingParams: {
        state: 'it has the ability to return required fields validation error',
        uponReceiving: 'POST request to create comment without all required fields',
        withRequest: {
            method: 'POST',
            path: '/api/comment',
            body: commentData.requests.postCommentMissingParams,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 400,
            body: like(commentData.responses.postCommentMissingParams),
        },
    },
    postCommentInvalidData: {
        state: 'it has the ability to return invalid  values validation error',
        uponReceiving: 'POST request to create comment with invalid fields data',
        withRequest: {
            method: 'POST',
            path: '/api/comment',
            body: commentData.requests.postCommentInvalidData,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 400,
            body: like(commentData.responses.postCommentInvalidData),
        },
    },

    getComment: {
        state: 'it has the ability to return comment data',
        uponReceiving: 'GET request to return comment data',
        withRequest: {
            method: 'GET',
            path: '/api/comment/customer/123456',
            query: {pageIndex:'1',pageSize:'100'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(commentData.responses.getComment),
        },
    },
    getCommentUnexistingCustomerId: {
        state: 'it has the ability to return comment data regardless of unexisting customer id',
        uponReceiving: 'GET request to return comment data for unexisting customerid',
        withRequest: {
            method: 'GET',
            path: '/api/comment/customer/958635923592',
            query: {pageIndex:'1',pageSize:'100'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(commentData.responses.getCommentUnexistingCustomerId),
        },
    },
    getCommentInvalidCustomerId: {
        state: 'it has the ability to return comment data validation error',
        uponReceiving: 'GET request to return comment data with invalid customer id',
        withRequest: {
            method: 'GET',
            path: '/api/comment/customer/test',
            query: {pageIndex:'1',pageSize:'100'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 400,
            body: like(commentData.responses.getCommentInvalidCustomerId),
        },
    },
    getCommentInvalidParams: {
        state: 'it has the ability to return comment parameters validation error',
        uponReceiving: 'GET request to return comment data with invalid params',
        withRequest: {
            method: 'GET',
            path: '/api/comment/customer/test',
            query: {pageIndex:'0',pageSize:'0'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 400,
            body: like(commentData.responses.getCommentInvalidParams),
        },
    },
};
