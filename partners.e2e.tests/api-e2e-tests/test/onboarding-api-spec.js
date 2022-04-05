import dotenv from 'dotenv'
import 'chai/register-expect';
import {requests, responses} from '../json-data/request-bodies/onboarding-api';
import {keys} from '../data/test-data';

import supertest from 'supertest';
dotenv.config()
const env = process.env.BASE_URL_ONBOARDING_API;


describe('POST /merchant;',  function() {
    it('/POST /merchant. Create a new merchant with valid data', async function() {
        /*
       Steps:
       1. Send POST /merchant with a valid body
       2. Verify correct data for correct merchant is returned
       */

        await supertest(env)
            .post(`/merchant`)
            .set('Content-Type', 'application/json')
            .set('Access-Key', keys.accessKey)
            .set('Access-Secret', keys.accessSecret)
            .send(requests.validBody)

            .expect(200)
            .then(response => {
                expect(response.body.success).to.eql(true)
                expect(response.body.message).to.eql('Merchant has been created successfully')
                expect(response.body.errors).to.eql(null)
                expect(response.body.inStoreApiKey).to.be.a('string')
                expect(response.body.onlineApiKey).to.be.a('string')
                expect(response.body.status).to.eql('Active')
                expect(response.body.merchantId).to.be.a('number')
            });
    });
    it('/POST /merchant. With invalid credentials (Access Key)', async function() {
        /*
       Steps:
       1. Send POST /merchant with a valid body
       2. Verify correct data for correct merchant is returned
       */

        await supertest(env)
            .post(`/merchant`)
            .set('Accept', 'application/json')
            .set('Access-Key', 'invalid')
            .set('Access-Secret', keys.accessSecret)
            .send(requests.validBody)

            .expect(401)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.eql({error: { code: 'unauthorized', message: 'Invalid access key and secret' }})
            });
    });
    it('/POST /merchant. With invalid credentials (Access Secret)', async function() {
        /*
       Steps:
       1. Send POST /merchant with a valid body and invalid credentials
       2. Verify correct error is returned
       */

        await supertest(env)
            .post(`/merchant`)
            .set('Accept', 'application/json')
            .set('Access-Key', keys.accessKey)
            .set('Access-Secret', 'test')
            .send(requests.validBody)

            .expect(401)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.eql({error: { code: 'unauthorized', message: 'Invalid access key and secret' }})
            });
    });
    it('/POST /merchant. With invalid credentials (Access Secret & Access Key)', async function() {
        /*
       Steps:
       1. Send POST /merchant with a valid body and invalid credentials
       2. Verify correct error is returned
       */

        await supertest(env)
            .post(`/merchant`)
            .set('Accept', 'application/json')
            .set('Access-Key', 'test')
            .set('Access-Secret', 'test')
            .send(requests.validBody)

            .expect(401)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.eql({error: { code: 'unauthorized', message: 'Invalid access key and secret' }})
            });
    });
    it('/POST /merchant. Without secret headers', async function() {
        /*
       Steps:
       1. Send POST /merchant with a valid body and without secret credentials
       2. Verify correct error is returned
       */

        await supertest(env)
            .post(`/merchant`)
            .set('Accept', 'application/json')
            .send(requests.validBody)

            .expect(401)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.eql({error: { code: 'unauthorized', message: 'Access key and secret are not present in header' }})
            });
    });
    it('/POST /merchant. Without required fields in the body', async function() {
        /*
       Steps:
       1. Send POST /merchant with an empty body
       2. Check validation for each required field
       */

        await supertest(env)
            .post(`/merchant`)
            .set('Accept', 'application/json')
            .set('Access-Key', keys.accessKey)
            .set('Access-Secret', keys.accessSecret)
            .send({})

            .expect(400)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.eql(responses.emptyBody)
            });
    });
    it('/POST /merchant. Duplicate email', async function() {
        /*
       Steps:
       1. Send POST /merchant with a valid body, but an email that was already used
       2. Verify correct error is returned
       */

        await supertest(env)
            .post(`/merchant`)
            .set('Accept', 'application/json')
            .set('Access-Key', keys.accessKey)
            .set('Access-Secret', keys.accessSecret)
            .send(requests.duplicateEmail)

            .expect(400)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.eql(responses.duplicateEmail)
            });
    });
    it('/POST /merchant. Check email validation, empty email', async function() {
        /*
       Steps:
       1. Send POST /merchant with a valid body, but with empty director email
       2. Verify correct error is returned
       */

        await supertest(env)
            .post(`/merchant`)
            .set('Accept', 'application/json')
            .set('Access-Key', keys.accessKey)
            .set('Access-Secret', keys.accessSecret)
            .send(requests.missingEmail)

            .expect(400)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.eql(responses.emptyEmail)
            });
    });
    it('/POST /merchant. Check email validation, invalid email', async function() {
        /*
       Steps:
       1. Send POST /merchant with a valid body, but with invalid director email
       2. Verify correct error is returned
       */

        await supertest(env)
            .post(`/merchant`)
            .set('Accept', 'application/json')
            .set('Access-Key', keys.accessKey)
            .set('Access-Secret', keys.accessSecret)
            .send(requests.invalidEmail)

            .expect(400)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.eql(responses.invalidEmail)
            });
    });
    it('/POST /merchant. Check abn validation, invalid abn', async function() {
        /*
       Steps:
       1. Send POST /merchant with a valid body, but with invalid abn
       2. Verify correct error is returned
       */

        await supertest(env)
            .post(`/merchant`)
            .set('Accept', 'application/json')
            .set('Access-Key', keys.accessKey)
            .set('Access-Secret', keys.accessSecret)
            .send(requests.invalidAbn)

            .expect(400)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.eql(responses.invalidAbn)
            });
    });
    it('/POST /merchant. Check intendedUse validation, invalid intendedUse', async function() {
        /*
       Steps:
       1. Send POST /merchant with a valid body, but with intendedUse as a string
       2. Verify correct error is returned
       */

        await supertest(env)
            .post(`/merchant`)
            .set('Accept', 'application/json')
            .set('Access-Key', keys.accessKey)
            .set('Access-Secret', keys.accessSecret)
            .send(requests.invalidIntendedUse)

            .expect(400)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body.errors.intendedUse[0]).to.contain("Could not convert string to integer: money" +
                    " laundering. Path 'intendedUse'")
            });
    });
});