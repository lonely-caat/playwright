import supertest from 'supertest';
import dotenv from 'dotenv-safe'
import assert from "assert";
import base from "../utils/base-library";
import expectedData from "../data/expectedData";
import helper from "../utils/api-helper";

dotenv.config()

const env = process.env.BASE_URL_MERCHANT_DASHBOARD_REPORTS;
let validBearerHeader


describe('Merchant Dashboard Reports Functional Tests', function(){
    beforeAll(async () => {
        validBearerHeader = await base.authHeaderGeneric()
        validBearerHeader = validBearerHeader.body['access_token']
    });
describe('GET /Customer/ endpoint tests', function() {

    it('check response with valid input for /new-customers', async function() {
        await supertest(env)
            .get(`/Customer/new-customers`)
            .set('Accept', 'application/json')
            .set(`authorization`, `Bearer ${validBearerHeader}`)
            .query({'startDate':'2013-01-01'})
            .query({'endDate':'2018-01-09'})
            .expect(204)
    });
    it('check response with valid input for /total-customers', async function() {
        await supertest(env)
            .get(`/Customer/total-customers`)
            .set('Accept', 'application/json')
            .set(`authorization`, `Bearer ${validBearerHeader}`)
            .set('md-branch-id', '-1')
            .query({'startDate':'2019-01-01'})
            .query({'endDate':'2019-01-09'})
            .expect(200)
            .then(response => {
                expect(response.body).toEqual(expectedData.totalCustomers)
            });
    });

    it('check response with valid input for /overview', async function() {
        await supertest(env)
            .get(`/Customer/overview`)
            .set('Accept', 'application/json')
            .set(`authorization`, `Bearer ${validBearerHeader}`)
            .set('md-branch-id', '-1')
            .query({'startDate':'2013-01-01'})
            .query({'endDate':'2022-01-22'})
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).toEqual(expectedData.customerOverview)
            });
    });

    it('Negative:: check response without branch', async function() {
        await supertest(env)
            .get(`/Customer/overview`)
            .set('Accept', 'application/json')
            .set(`authorization`, `Bearer ${validBearerHeader}`)
            .query({'startDate':'2013-01-01'})
            .query({'endDate':'2022-01-22'})
            .expect(500)
            .expect('Content-Type', 'application/json')
            .then(response => {
                expect(response.body).toEqual(expectedData.customerOverviewNoBranch)
            });
    });

    it('Negative:: check response Invalid auth header', async function() {
        await supertest(env)
            .get(`/Customer/overview`)
            .set('Accept', 'application/json')
            .set(`authorization`, `Invalid`)
            .query({'startDate':'2013-01-01'})
            .query({'endDate':'2022-01-22'})
            .expect(401)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                expect(response.body).toEqual('Forbidden')
            });
    });

    it('Negative: check response No startDate', async function() {
        await supertest(env)
            .get(`/Customer/total-customers`)
            .set('Accept', 'application/json')
            .set(`authorization`, `Bearer ${validBearerHeader}`)
            .expect(400)
            .then(response => {
                // remove traceId as it's random each time and we don't care about it
                delete response.body['traceId']
                delete expectedData.totalCustomersNoDate['traceId']
                expect(response.body).toEqual(expectedData.totalCustomersNoDate)
            });
    });

});
    describe('GET & POST /Disbursment/ endpoint tests', function() {
        it('check response with valid input for /net-volume', async function() {
            await supertest(env)
                .get(`/Disbursement/net-volume`)
                .set('Accept', 'application/json')
                .set(`authorization`, `Bearer ${validBearerHeader}`)
                .query({'startDate':'2019-01-01'})
                .query({'endDate':'2019-01-09'})
                .expect(200)
                .then(response => {
                    expect(response.body).toEqual(expectedData.netVolume)
                });
        });

        it('check response for /net-volume with period and frequency params', async function() {
            await supertest(env)
                .get(`/Disbursement/net-volume`)
                .set('Accept', 'application/json')
                .set(`authorization`, `Bearer ${validBearerHeader}`)
                .query({'startDate':'2019-01-01'})
                .query({'endDate':'2019-01-09'})
                .query({'Frequency': '1'})
                .query({'Period': '1'})
                .expect(200)
                .then(response => {
                    const response_parsed = JSON.parse(response.text)
                    expect(response_parsed).toEqual(expectedData.netVolumeWithParams)
                });
        });

        it('check response with valid input for /Schedules', async function() {
            await supertest(env)
                .get(`/Disbursement/Schedules`)
                .set('Accept', 'application/json')
                .set(`authorization`, `Bearer ${validBearerHeader}`)
                .expect(200)
                .then(response => {
                    expect(response.body).toEqual(expectedData.schedules)
                });
        });

        it('check response with valid input for /report', async function() {
            await supertest(env)
                .get(`/Disbursement/report`)
                .set('Accept', 'application/json')
                .set(`authorization`, `Bearer ${validBearerHeader}`)
                .query({'FileName': '2134/downloads/custom-reports/Disbursements_M2134_10-Mar-2028-to-10-Mar-2028_1.csv'})
                .expect(200)
                .then(response => {
                    expect(response.body).toEqual(expectedData.reports)
                });
        });

        it('check response with valid input for POST /report', async function() {
            await supertest(env)
                .post(`/Disbursement/report`)
                .set('Accept', 'application/json')
                .set(`authorization`, `Bearer ${validBearerHeader}`)
                .send({ "fromDate": "2029-03-10T15:05:54.503Z", "toDate": "2029-03-10T15:05:55.503Z" })
                .expect(200)
                .then(response => {
                    expect(response.body).toMatch(/.*\/downloads\/custom-reports\/Disbursements.*\.csv/)
                });
        });

        it('Negative: check response with unexisting report name', async function() {
            await supertest(env)
                .get(`/Disbursement/report`)
                .set('Accept', 'application/json')
                .set(`authorization`, `Bearer ${validBearerHeader}`)
                .query({'FileName': 'meow'})
                .expect(424)
                .then(response => {
                    expect(response.body).toEqual(expectedData.reportsInvalidName)
                });
        });

        it('Negative: check response with invalid report range', async function() {
            await supertest(env)
                .post(`/Disbursement/report`)
                .set('Accept', 'application/json')
                .set(`authorization`, `Bearer ${validBearerHeader}`)
                .send({ "fromDate": "2026-03-10T15:05:54.503Z", "toDate": "2029-03-10T15:05:55.503Z" })
                .expect(400)
                .then(response => {
                    // remove traceId as it's random each time and we don't care about it
                    delete response.body['traceId']
                    delete expectedData.reportsInvalidRange['traceId']
                    expect(response.body).toEqual(expectedData.reportsInvalidRange)
                });
        });
    });


    describe('GET /Order/ endpoint tests', function() {

        it.each`
              input    | expected
              ${0}     | ${{"orderCounts":{"NotStarted":0}}}
              ${1}     | ${{"orderCounts":{"InProgress":6}}}
              ${2}     | ${{"orderCounts":{"UnderReview":0}}}
              ${3}     | ${{"orderCounts":{"ContractPending":0}}}
              ${4}     | ${{"orderCounts":{"Declined":0}}}
              ${5}     | ${{"orderCounts":{"Authorised":0}}}
              ${6}     | ${{"orderCounts":{"Completed":79}}}
              ${7}     | ${{"orderCounts":{"Refunded":0}}}
              ${8}     | ${{"orderCounts":{"Cancelled":0}}}
              ${9}     | ${{"orderCounts":{"Removed":0}}}
              ${10}    | ${{"orderCounts":{"DepositRequired":0}}}
              ${11}    | ${{"orderCounts":{"PartiallyCaptured":0}}}
              `('check response with valid input for /orders-summary with parameters Status:$input', async ({ input, expected }) => {
              await supertest(env)
                  .get(`/Order/orders-summary`)
                  .set('Accept', 'application/json')
                  .set(`authorization`, `Bearer ${validBearerHeader}`)
                  .query({'startDate':'2019-01-01'})
                  .query({'endDate':'2020-01-09'})
                  .query({'Statuses': input})
                  .expect(200)
                  .then(response => {
                      expect(response.body).toEqual(expected)
                  });
            });

        it('Negative: check response without Statuses param', async function() {
            await supertest(env)
                .get(`/Order/orders-summary`)
                .set('Accept', 'application/json')
                .set(`authorization`, `Bearer ${validBearerHeader}`)
                .query({'startDate':'2019-01-01'})
                .query({'endDate':'2020-01-09'})
                .expect(400)
                .then(response => {
                    // remove traceId as it's random each time and we don't care about it
                    delete response.body['traceId']
                    delete expectedData.orderSummaryNoStatus['traceId']
                    expect(response.body).toEqual(expectedData.orderSummaryNoStatus)
                });
        });
        it.each`
              input    | expected
              ${12}     | ${{"orderCounts":{"NotStarted":0}}}
              ${'meow'}     | ${{"orderCounts":{"InProgress":6}}}
              ${-1}     | ${{"orderCounts":{"UnderReview":0}}}
              ${''}     | ${{"orderCounts":{"ContractPending":0}}}
              `('Negative: check response invalid value Statuses param Status:$input', async ({ input, expected }) => {
            await supertest(env)
                .get(`/Order/orders-summary`)
                .set('Accept', 'application/json')
                .set(`authorization`, `Bearer ${validBearerHeader}`)
                .query({'startDate':'2019-01-01'})
                .query({'endDate':'2020-01-09'})
                .expect(400)
                });
        });
    describe('GET /Transaction/ endpoint tests', function() {
        it('check response with valid input for /transactions', async function() {
            await supertest(env)
                .get(`/Transaction/transactions`)
                .set('Accept', 'application/json')
                .set(`authorization`, `Bearer ${validBearerHeader}`)
                .query({'startDate':'2013-01-01'})
                .query({'endDate':'2022-01-09'})
                .expect(204)
        });

        it('check response with valid input for /gross-volume', async function() {
            await supertest(env)
                .get(`/Transaction/gross-volume`)
                .set('Accept', 'application/json')
                .set(`authorization`, `Bearer ${validBearerHeader}`)
                .query({'startDate':'2013-01-01'})
                .query({'endDate':'2022-01-09'})
                .expect(204)
        });
        });
});