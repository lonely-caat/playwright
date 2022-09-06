const request = require('superagent');
require('dotenv-safe').config();
const cardData = require('../req-resp/cardsApi');


const MOCK_SERVICE_URL = `http://localhost:${process.env.MOCK_SERVER_PORT}`;

module.exports = {
    async getCardData() {
        await request
            .get(`${MOCK_SERVICE_URL}/internal/cards/e16a5f31-a6bf-43f7-b43a-bf5a4e381326`)
            .set('Content-Type', 'application/json')
            .send();
    },
    async getUnexistingCardData() {
        await request
            .get(`${MOCK_SERVICE_URL}/internal/cards/noexist`)
            .set('Content-Type', 'application/json')
            .send().catch(error => {error.statusCode = 404});
    },

    async getCardDataByExternalId() {
        await request
            .get(`${MOCK_SERVICE_URL}/internal/cards`)
            .query({externalId:'0581c676-246f-4eeb-b520-f9cd7140dadb'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getCardDataByUnexistingExternalId() {
        await request
            .get(`${MOCK_SERVICE_URL}/internal/cards`)
            .query({externalId:'noexist'})
            .set('Content-Type', 'application/json')
            .send();
    },

    async getCardDataByCustomerId() {
        await request
            .get(`${MOCK_SERVICE_URL}/internal/cards`)
            .query({customerId:'8b4450c9-895a-433e-882d-bb024fe5de47'})
            .set('Content-Type', 'application/json')
            .send();
    },
    async getCardDataByUnexistingCustomerId() {
        await request
            .get(`${MOCK_SERVICE_URL}/internal/cards`)
            .query({customerId:'noexist'})
            .set('Content-Type', 'application/json')
            .send();
    },

    async putBlockValidCard() {
        await request
            .put(`${MOCK_SERVICE_URL}/cards/7edca99d-857a-4252-8454-f8a0786f4a24/block`)
            .set('Content-Type', 'application/json')
            .set('Customer-Id', '8b4450c9-895a-433e-882d-bb024fe5de47')
            .send();
    },
    async putBlockUnexistingCard() {
        await request
            .put(`${MOCK_SERVICE_URL}/cards/noexist/block`)
            .set('Content-Type', 'application/json')
            .set('Customer-Id', '8b4450c9-895a-433e-882d-bb024fe5de47')
            .send().catch(error => {error.statusCode = 404});
    },
    async putBlockUnexistingCustomerId() {
        await request
            .put(`${MOCK_SERVICE_URL}/cards/7edca99d-857a-4252-8454-f8a0786f4a24/block`)
            .set('Content-Type', 'application/json')
            .set('Customer-Id', 'noexist')
            .send().catch(error => {error.statusCode = 401});
    },

    async putUnBlockValidCard() {
        await request
            .put(`${MOCK_SERVICE_URL}/cards/7edca99d-857a-4252-8454-f8a0786f4a24/unblock`)
            .set('Content-Type', 'application/json')
            .set('Customer-Id', '8b4450c9-895a-433e-882d-bb024fe5de47')
            .send();
    },
    async putUnBlockUnexistingCard() {
        await request
            .put(`${MOCK_SERVICE_URL}/cards/noexist/unblock`)
            .set('Content-Type', 'application/json')
            .set('Customer-Id', '8b4450c9-895a-433e-882d-bb024fe5de47')
            .send().catch(error => {error.statusCode = 404});
    },
    async putUnBlockUnexistingCustomerId() {
        await request
            .put(`${MOCK_SERVICE_URL}/cards/7edca99d-857a-4252-8454-f8a0786f4a24/unblock`)
            .set('Content-Type', 'application/json')
            .set('Customer-Id', 'noexist')
            .send().catch(error => {error.statusCode = 401});
    },

    async postTransitionValidCard() {
        await request
            .post(`${MOCK_SERVICE_URL}/internal/digitalwallet/tokentransition`)
            .set('Content-Type', 'application/json')
            .send(cardData.requests.TransitionValidCard);
    },
    async postTransitionInValidCard() {
        await request
            .post(`${MOCK_SERVICE_URL}/internal/digitalwallet/tokentransition`)
            .set('Content-Type', 'application/json')
            .send(cardData.requests.TransitionInValidCard).catch(error => {error.statusCode = 400});
    },
};