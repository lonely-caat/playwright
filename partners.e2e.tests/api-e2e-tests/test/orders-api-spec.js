import dotenv from 'dotenv-safe'
import 'chai/register-expect';
import data from '../json-data/request-bodies/orders-api';
import supertest from 'supertest';

dotenv.config()
const env = process.env.BASE_URL_ORDERS_API;

// Waits for https://zip-co.atlassian.net/browse/BP-1669 to be implemented!!!
// describe('Orders API tests',  function() {
//     beforeAll(async (done) => {
//         const response = await supertest(env).get('/health');
//         console.log(response.statusCode, response.request.url, '99999999999')
//         if (response.statusCode !== 200){
//         console.log("\x1b[31m", `${env} returned ${response.statusCode}! for health check!`);process.exit(1);}
//         done();
//     });

describe('GET /orders tests; "take" query param',  function() {
    it('/ordersearch/api/v1/orders. Get orders, check pagination', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders?Take=1
       2. Verify only two items returned in response object
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders`)
            .query({'take': '2'})
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.an('array').and.is.ok;
                expect(Object.keys(response.body).length).to.be.equal(2)
    });
});

it('/ordersearch/api/v1/orders. Get orders, check pagination', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders?Take=100
       2. Verify 100 items returned in response object
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders`)
            .query({'take': '100'})
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.an('array').and.is.ok;
                expect(Object.keys(response.body).length).to.be.equal(100)
            });
    });
    it('/ordersearch/api/v1/orders. Get orders, check pagination', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders?Take=1
       2. Verify only one item returned in response object
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders`)
            .query({'take': '1'})
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.an('array').and.is.ok;
                expect(Object.keys(response.body).length).to.be.equal(1)
            });
    });
    it('/ordersearch/api/v1/orders. Get orders, check pagination', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders?Take=string
       2. Verify bad request status code returned
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders`)
            .query({'take': 'string'})
            .set('Accept', 'application/json')
            .expect(400)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.an('object').and.is.ok;
                expect(response.body['errors']).to.eql({ Take: [ "The value 'string' is not valid for Take." ] })
            });
    });
});
describe('GET /orders tests; "query" query param',  function() {
    it('/ordersearch/api/v1/orders. Get orders, check data for "query" query parameter', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders?Take=5&Skip=0&Query=222360
       2. Verify correct data for correct orderId is returned
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders`)
            .query({'take': '5'})
            .query({'skip': '0'})
            .query({'query': '222360'})
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.eql(data.responses.getOrdersAccountId)
            });
    });
    it('/ordersearch/api/v1/orders. Get orders, check empty array returned when no data is present', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders?Take=5&Skip=0&Query=1
       2. Verify empty array is returned
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders`)
            .query({'take': '5'})
            .query({'skip': '0'})
            .query({'query': '1'})
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.eql([])
            });
    });
});
describe('GET /orders tests; "skip" query param',  function() {
    it('/ordersearch/api/v1/orders. Get orders, check data for "skip" query parameter', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders?Take=5&Skip=100&Query=222360
       2. Verify correct data for correct orderId is returned
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders`)
            .query({'take': '5'})
            .query({'skip': '100'})
            .query({'query': '553006'})
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.have.keys(Object.keys(data.responses.getOrdersAccountIdSkip))
            });
    });
    it('/ordersearch/api/v1/orders. Get orders, check data for invalid "skip" query parameter', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders?Take=5&Skip=test&Query=222360
       2. Verify bad request returned, along with valid error message
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders`)
            .query({'take': '5'})
            .query({'skip': 'test'})
            .query({'query': '222360'})
            .set('Accept', 'application/json')
            .expect(400)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.an('object').and.is.ok;
                expect(response.body['errors']).to.eql({ Skip: [ "The value 'test' is not valid for Skip." ] })
            });
    });
});
describe('GET /orders tests; "type" query param',  function() {
    it('/ordersearch/api/v1/orders. Get orders, check data for "type" query parameter', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders?Take=100&Skip=0&Query=33539&Type=NoFilter
       2. Verify correct data for correct orderId is returned
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders`)
            .query({'skip': '0'})
            .query({'query': '33539'})
            .query({'type': 'NoFilter'})
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.eql(data.responses.getOrdersAccountIdType)
            });
    });
    it('/ordersearch/api/v1/orders. Get orders, check data for "type"=AccountNumber query parameter', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders?Take=100&Skip=0&Query=33539&Type=AccountNumber
       2. Verify correct data for correct orderId is returned
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders`)
            .query({'skip': '500'})
            .query({'query': '553006'})
            .query({'type': 'AccountNumber'})
            .query({'take': '1'})
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.have.keys(Object.keys(data.responses.getOrdersAccountIdTypeAccountNumber))
            });
    });
    it('/ordersearch/api/v1/orders. Get orders, check data for invalid "type" query parameter', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders?Take=100&Skip=0&Query=33539&Type=test
       2. Verify correct data for correct orderId is returned
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders`)
            .query({'skip': '0'})
            .query({'query': '33539'})
            .query({'type': 'test'})
            .set('Accept', 'application/json')
            .expect(400)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.an('object').and.is.ok;
                expect(response.body['errors']).to.eql({ Type: [ "The value 'test' is not valid for SearchType." ] })
            });
    });
});