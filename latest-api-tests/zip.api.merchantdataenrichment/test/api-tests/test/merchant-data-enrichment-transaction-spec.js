const supertest = require('supertest');
const transactionPayload = require('../json-data/request-bodies/post-merchant-transaction.json');
require('dotenv-safe').config();

const request = supertest(process.env.BASE_URL);

describe('Transaction tests', () => {
describe('Positive Tests: transactions', () => {
    it('Post merchant transaction returns valid response, status and headers', async () => {
        // Create vcn transaction and enrich merchant detail, should be used by Enrichment Worker only
        let postResponse = await request
            .post(`/api/v1/Transaction/create-vcn-transaction`)
            .send(transactionPayload);
        expect(postResponse.status).toBe(201);
        expect(postResponse.headers['content-type']).toBe('application/json; charset=utf-8');
        expect(Object.keys(postResponse.body)).toContain('transactionId')
    });
});

describe('Negative Tests: transactions', () => {
    it('Post merchant transaction without body returns valid status and headers', async () => {
        let postResponse = await request
            .post('/api/v1/Transaction/create-vcn-transaction')
            .set({ 'Content-Type': 'application/json'});
        expect(postResponse.status).toBe(400);
        expect(postResponse.headers['content-type']).toBe('application/problem+json; charset=utf-8');
        expect(postResponse.body['errors']['']).toStrictEqual([ 'A non-empty request body is required.' ])
    });

    it('Post merchant transaction without all required values returns valid response, status and headers', async () => {
        let postResponse = await request
            .post('/api/v1/Transaction/create-vcn-transaction')
            .set({ 'Content-Type': 'application/json'})
            .send({"accountId": 1, "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"})
        expect(postResponse.status).toBe(400);
        expect(postResponse.headers['content-type']).toBe('application/problem+json; charset=utf-8');
        expect(postResponse.body['errors']).toEqual(
            {
                VcnCardId: [ "'Vcn Card Id' must not be empty." ],
                CardAcceptorCity: [ "'Card Acceptor City' must not be empty." ],
                CardAcceptorName: [ "'Card Acceptor Name' must not be empty." ]
            })
    });

    it('Post merchant invalid CustomerId returns valid response, status and headers', async () => {
        let postResponse = await request
            .post('/api/v1/Transaction/create-vcn-transaction')
            .set({ 'Content-Type': 'application/json'})
            .send({"accountId": 1, "vcnCardId": "meow", "cardAcceptorCity": "Buerrgh"})
        expect(postResponse.status).toBe(400);
        expect(postResponse.headers['content-type']).toBe('application/problem+json; charset=utf-8');
        expect(postResponse.body['errors']).toEqual(
            {
                CustomerId: [
                    "'Customer Id' must not be empty.",
                    "The specified condition was not met for 'Customer Id'."],
                CardAcceptorName: [ "'Card Acceptor Name' must not be empty." ]
            })
    });
});
});