const request = require('superagent');
require('dotenv-safe').config();
const commentData = require('../req-resp/crmServiceProxy');


const MOCK_SERVICE_URL = `http://localhost:${process.env.MOCK_SERVER_PORT}`;

module.exports = {
    async postComment() {
        await request
            .post(`${MOCK_SERVICE_URL}/api/comment`)
            .set('Content-Type', 'application/json')
            .send(commentData.requests.postComment);
    },
    async postCommentMissingParams() {
        await request
            .post(`${MOCK_SERVICE_URL}/api/comment`)
            .set('Content-Type', 'application/json')
            .send(commentData.requests.postCommentMissingParams).catch(error => {error.statusCode = 400});
    },
    async postCommentInvalidData() {
        await request
            .post(`${MOCK_SERVICE_URL}/api/comment`)
            .set('Content-Type', 'application/json')
            .send(commentData.requests.postCommentInvalidData).catch(error => {error.statusCode = 400});
    },

    async getComment() {
        await request
            .get(`${MOCK_SERVICE_URL}/api/comment/customer/123456`)
            .query({pageIndex:'1',pageSize:'100'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getCommentUnexistingCustomerId() {
        await request
            .get(`${MOCK_SERVICE_URL}/api/comment/customer/958635923592`)
            .query({pageIndex:'1',pageSize:'100'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getCommentInvalidCustomerId() {
        await request
            .get(`${MOCK_SERVICE_URL}/api/comment/customer/test`)
            .query({pageIndex:'1',pageSize:'100'})
            .set('Content-Type', 'application/json')
            .send().catch(error => {error.statusCode = 400});
    },
    async getCommentInvalidParams() {
        await request
            .get(`${MOCK_SERVICE_URL}/api/comment/customer/test`)
            .query({pageIndex:'0',pageSize:'0'})
            .set('Content-Type', 'application/json')
            .send().catch(error => {error.statusCode = 400});
    },
};