import dotenv from 'dotenv-safe'
import 'chai/register-expect';
import data from '../json-data/request-bodies/orders-api';

dotenv.config()
// import supertest from 'supertest';
const supertest = require('supertest');
const env = process.env.BASE_URL_ORDERS_API;

describe('GET /orders/branches;',  function() {
    it.skip('/orders/branches. Get orders by branches, check valid data', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders/branches?Take=8&Skip=4995
       2. Verify correct data for correct branch is returned
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders/branches`)
            .query({'Take': '8'})
            .query({'Skip': '4995'})
            .query({'Query': '389015'})
            .set('Accept', 'application/json')
            .expect(200)
            .then(response => {
                expect(response.body).to.be.eql(data.responses.getOrdersBranchesQueryParams)
            });
    });
});
describe('GET /orders/branches; branch query param',  function() {
    it('/orders/branches. Get orders by specific branch, check valid data', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders/branches?branch=1
       2. Verify correct data for correct branch is returned
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders/branches`)
            .query({'Branch': '1'})
            .query({'take': '2'})
            .set('Accept', 'application/json')
            .expect(200)
            .then(response => {
                expect(response.body).to.have.keys(Object.keys(data.responses.getOrdersBranchQueryParams))
            });
    });
    it('/orders/branches. Get orders by specific branch, unexisting branch', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders/branches?branch=1
       2. Verify empty array is returned
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders/branches`)
            .query({'Branch': '100'})
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.eql([])
            });
    });
    it('/orders/branches. Get orders by specific branch, invalid branch', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders/branches?branch=1
       2. Verify 400 with appropriate error returned
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders/branches`)
            .query({'Branch': 'test'})
            .set('Accept', 'application/json')
            .expect('Content-Type', 'application/problem+json; charset=utf-8')
            .expect(400)
            .then(response => {
                expect(response.body).to.be.an('object').and.is.ok;
                expect(response.body['errors']).to.eql({ branch: [ "The value 'test' is not valid." ] })
                expect(response.body['title']).to.eql('One or more validation errors occurred.')
            });
    });
});