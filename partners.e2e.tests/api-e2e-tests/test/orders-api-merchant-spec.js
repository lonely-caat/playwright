import dotenv from 'dotenv-safe'
import 'chai/register-expect';
import data from '../json-data/request-bodies/orders-api';
import supertest from 'supertest';

dotenv.config()
const env = process.env.BASE_URL_ORDERS_API;

describe('GET /orders/merchant/{merchantId} tests;',  function() {
    it.skip('/orders/merchant/{merchantId}. Get orders, check valid data', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders/merchant/2005
       2. Verify correct data for correct merchantid is returned
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders/merchant/2005`)
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.eql(data.responses.getOrdersMerchantId)
            });
    });
    it.skip('/orders/merchant/{merchantId}. Get orders with query params', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders/merchant/2005?Take=1&Skip=1&Type=1
       2. Verify correct data for correct orderId is returned
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders/merchant/2005`)
            .set('Accept', 'application/json')
            .query({'Take': '1'})
            .query({'Skip': '1'})
            .query({'Type': '1'})
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.eql(data.responses.getOrdersMerchantIdQueryParams)
            });
    });
    it('/orders/merchant/{merchantId}. Get orders, with unexisting merchantid', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders/merchant/666
       2. Verify empty array returned
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders/merchant/666`)
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.eql([])
            });
    });
    it('/orders/merchant/{merchantId}. Get orders, with invalid merchantid string', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders/merchant/test
       2. Verify empty array returned
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders/merchant/test`)
            .set('Accept', 'application/json')
            .expect(400)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.an('object').and.is.ok;
                expect(response.body['errors']).to.eql({ merchantId: [ "The value 'test' is not valid." ] })
                expect(response.body['title']).to.eql('One or more validation errors occurred.')
            });
    });
    it('/orders/merchant/{merchantId}. Get orders, with invalid merchantid limit', async function() {
        /*
       Steps:
       1. Send /ordersearch/api/v1/orders/merchant/999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999
       2. Verify empty array returned
       */

        await supertest(env)
            .get(`/ordersearch/api/v1/orders/merchant/999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999`)
            .set('Accept', 'application/json')
            .expect(400)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.an('object').and.is.ok;
                expect(response.body['errors']).to.eql({ merchantId: [ "The value '999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999' is not valid." ] })
                expect(response.body['title']).to.eql('One or more validation errors occurred.')
            });
    });
});