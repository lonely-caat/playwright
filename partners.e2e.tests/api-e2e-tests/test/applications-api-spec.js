import dotenv from 'dotenv-safe'
import 'chai/register-expect';
import data from '../json-data/request-bodies/applications-api';
import supertest from 'supertest';

dotenv.config()
const env = process.env.BASE_URL_APPLICATIONS_API;

// Waits for https://zip-co.atlassian.net/browse/BP-1669 to be implemented!!!
// describe('Orders API tests',  function() {
//     beforeAll(async (done) => {
//         const response = await supertest(env).get('/health');
//         console.log(response.statusCode, response.request.url, '99999999999')
//         if (response.statusCode !== 200){
//         console.log("\x1b[31m", `${env} returned ${response.statusCode}! for health check!`);process.exit(1);}
//         done();
//     });

describe('GET /applications tests; "take" query param',  function() {
    it('/applicationsearch/api/v1/applications. Get applications, check pagination', async function() {
        /*
       Steps:
       1. Send /applicationsearch/api/v1/applications?Take=1
       2. Verify only two items returned in response object
       */

        await supertest(env)
            .get(`/applicationsearch/api/v1/applications`)
            .query({'take': '2'})
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.an('array').and.is.ok;
                expect(Object.keys(response.body).length).to.be.equal(2)
            });
    });

    it('/applicationsearch/api/v1/applications. Get applications, check pagination', async function() {
        /*
       Steps:
       1. Send /applicationsearch/api/v1/applications?Take=100
       2. Verify 100 items returned in response object
       */

        await supertest(env)
            .get(`/applicationsearch/api/v1/applications`)
            .query({'take': '100'})
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.an('array').and.is.ok;
                expect(Object.keys(response.body).length).to.be.equal(100)
            });
    });
    it('/applicationsearch/api/v1/applications. Get applications, check pagination', async function() {
        /*
       Steps:
       1. Send /applicationsearch/api/v1/applications?Take=1
       2. Verify only one item returned in response object
       */

        await supertest(env)
            .get(`/applicationsearch/api/v1/applications`)
            .query({'take': '1'})
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.an('array').and.is.ok;
                expect(Object.keys(response.body).length).to.be.equal(1)
            });
    });
    it('/applicationsearch/api/v1/applications. Get applications, check pagination', async function() {
        /*
       Steps:
       1. Send /applicationsearch/api/v1/applications?Take=string
       2. Verify bad request status code returned
       */

        await supertest(env)
            .get(`/applicationsearch/api/v1/applications`)
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
describe('GET /applications tests; "query" query param',  function() {
    it('/applicationsearch/api/v1/applications. Get applications, check data for "query" query parameter', async function() {
        /*
       Steps:
       1. Send /applicationsearch/api/v1/applications?Take=5&Skip=0&Query=222360
       2. Verify correct data for correct orderId is returned
       */

        await supertest(env)
            .get(`/applicationsearch/api/v1/applications`)
            .query({'take': '5'})
            .query({'skip': '0'})
            .query({'query': '222360'})
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.have.keys(Object.keys(data.responses.getApplicationsAccountId));
            });
    });
    it('/applicationsearch/api/v1/applications. Get applications, check empty array returned when no data is present', async function() {
        /*
       Steps:
       1. Send /applicationsearch/api/v1/applications?Take=5&Skip=0&Query=1
       2. Verify empty array is returned
       */

        await supertest(env)
            .get(`/applicationsearch/api/v1/applications`)
            .query({'take': '5'})
            .query({'skip': '0'})
            .query({'query': '111111111'})
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.eql([])
            });
    });
});
describe('GET /applications tests; "skip" query param',  function() {
    it('/applicationsearch/api/v1/applications. Get applications, check data for "skip" query parameter', async function() {
        /*
       Steps:
       1. Send /applicationsearch/api/v1/applications?Take=5&Skip=100&Query=222360
       2. Verify correct data for correct orderId is returned
       */

        await supertest(env)
            .get(`/applicationsearch/api/v1/applications`)
            .query({'take': '5'})
            .query({'skip': '100'})
            .query({'query': '553006'})
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.eql([])
            });
    });
    it('/applicationsearch/api/v1/applications. Get applications, check data for invalid "skip" query parameter', async function() {
        /*
       Steps:
       1. Send /applicationsearch/api/v1/applications?Take=5&Skip=test&Query=222360
       2. Verify bad request returned, along with valid error message
       */

        await supertest(env)
            .get(`/applicationsearch/api/v1/applications`)
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
describe('GET /applications tests; "type" query param',  function() {
    it('/applicationsearch/api/v1/applications. Get applications, check data for "type" query parameter', async function() {
        /*
       Steps:
       1. Send /applicationsearch/api/v1/applications?Take=100&Skip=0&Query=33539&Type=NoFilter
       2. Verify correct data for correct orderId is returned
       */

        await supertest(env)
            .get(`/applicationsearch/api/v1/applications`)
            .query({'skip': '0'})
            .query({'query': '33539'})
            .query({'type': 'NoFilter'})
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.have.keys(Object.keys(data.responses.getApplicationsAccountIdType));
            });
    });
    it('/applicationsearch/api/v1/applications. Get applications, check data for "type"=AccountNumber query parameter', async function() {
        /*
       Steps:
       1. Send /applicationsearch/api/v1/applications?Take=100&Skip=0&Query=33539&Type=AccountNumber
       2. Verify correct data for correct orderId is returned
       */

        await supertest(env)
            .get(`/applicationsearch/api/v1/applications`)
            .query({'query': '420739'})
            .query({'type': 'AccountNumber'})
            .query({'take': '1'})
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.have.keys(Object.keys(data.responses.getApplicationsAccountIdTypeAccountNumber))
            });
    });
    it('/applicationsearch/api/v1/applications. Get applications, check data for invalid "type" query parameter', async function() {
        /*
       Steps:
       1. Send /applicationsearch/api/v1/applications?Take=100&Skip=0&Query=33539&Type=test
       2. Verify correct data for correct orderId is returned
       */

        await supertest(env)
            .get(`/applicationsearch/api/v1/applications`)
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

describe('GET /applications/branches; branch query param',  function() {
    it('/applications/branches. Get applications by specific branch, check valid data', async function() {
        /*
       Steps:
       1. Send /applicationsearch/api/v1/applications/branches?branch=1
       2. Verify correct data for correct branch is returned
       */

        await supertest(env)
            .get(`/applicationsearch/api/v1/applications/branches`)
            .query({'Branch': '1'})
            .query({'take': '2'})
            .set('Accept', 'application/json')
            .expect(200)
            .then(response => {
                expect(response.body).to.have.keys(Object.keys(data.responses.getApplicationsBranchQueryParams))
            });
    });
    it('/applications/branches. Get applications by specific branch, unexisting branch', async function() {
        /*
       Steps:
       1. Send /applicationsearch/api/v1/applications/branches?branch=1
       2. Verify empty array is returned
       */

        await supertest(env)
            .get(`/applicationsearch/api/v1/applications/branches`)
            .query({'Branch': '100'})
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).to.be.eql([])
            });
    });
    it('/applications/branches. Get applications by specific branch, invalid branch', async function() {
        /*
       Steps:
       1. Send /applicationsearch/api/v1/applications/branches?branch=1
       2. Verify 400 with appropriate error returned
       */

        await supertest(env)
            .get(`/applicationsearch/api/v1/applications/branches`)
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
