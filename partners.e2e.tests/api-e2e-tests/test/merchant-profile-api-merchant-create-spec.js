import dotenv from 'dotenv';
import 'chai/register-expect';
import supertest from 'supertest';
import { requests, responses } from '../json-data/request-bodies/merchant-profile-api-merchant-create';
import { keys } from '../data/test-data';

dotenv.config();
const env = process.env.BASE_URL_MERCHANT_PROFILE_API;
jest.setTimeout(600000)


describe.skip('POST /merchantprovision/create; :heavy_exclamation_mark: blocked by BP-1213', function () {

  it('/POST /merchantprovision/create. Create a new merchant with valid data', async function () {
    /*
       Steps:
       1. Send POST /merchantprovision/create with a valid body
       2. Verify correct data for correct merchant is returned
       */

    await supertest(env)
      .post(`/merchantprovision/create`)
      .set('Content-Type', 'application/json')
      .set('Authorization', keys.mpAuthKey)
      .send(requests.validBody)

      .expect(200)
      .then((response) => {
        expect(response.body.success).to.eql(true);
        expect(response.body.message).to.eql('Merchant has been created successfully');
        expect(response.body.errors).to.eql(null);
        expect(response.body.inStoreApiKey).to.be.a('string');
        expect(response.body.onlineApiKey).to.be.a('string');
        expect(response.body.status).to.eql('Active');
        expect(response.body.merchantId).to.be.a('number');
      });
  });
  it('/POST /merchantprovision/create. Without credentials (Access Key)', async function () {
    /*
       Steps:
       1. Send POST /merchantprovision/create with a valid body
       2. Verify correct data for correct merchant is returned
       */

    await supertest(env)
      .post(`/merchantprovision/create`)
      .set('Content-Type', 'application/json')
      .send(requests.validBody)

      .expect(401)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then((response) => {
        expect(response.body).to.eql({
          error: { code: 'unauthorized', message: 'Invalid access key and secret' },
        });
      });
  });

  it('/POST /merchantprovision/create. Without json header', async function () {
    /*
       Steps:
       1. Send POST /merchantprovision/create with a valid body and without secret credentials
       2. Verify correct error is returned
       */

    await supertest(env)
      .post(`/merchantprovision/create`)
      .send(requests.validBody)

      .expect(401)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then((response) => {
        expect(response.body).to.eql({
          error: {
            code: 'unauthorized',
            message: 'Access key and secret are not present in header',
          },
        });
      });
  });
  it('/POST /merchantprovision/create. Without required fields in the body', async function () {
    /*
       Steps:
       1. Send POST /merchantprovision/create with an empty body
       2. Check validation for each required field
       */

    await supertest(env)
      .post(`/merchantprovision/create`)
      .set('Content-Type', 'application/json')
      .set('Authorization', keys.mpAuthKey)
      .send({})

      .expect(400)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then((response) => {
        expect(response.body).to.eql(responses.emptyBody);
      });
  });
  it('/POST /merchantprovision/create. Duplicate email', async function () {
    /*
       Steps:
       1. Send POST /merchantprovision/create with a valid body, but an email that was already used
       2. Verify correct error is returned
       */

    await supertest(env)
      .post(`/merchantprovision/create`)
      .set('Content-Type', 'application/json')
      .set('Authorization', keys.mpAuthKey)
      .send(requests.duplicateEmail)

      .expect(400)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then((response) => {
        expect(response.body).to.eql(responses.duplicateEmail);
      });
  });
  it('/POST /merchantprovision/create. Check email validation, empty email', async function () {
    /*
       Steps:
       1. Send POST /merchantprovision/create with a valid body, but with empty director email
       2. Verify correct error is returned
       */

    await supertest(env)
      .post(`/merchantprovision/create`)
      .set('Content-Type', 'application/json')
      .set('Authorization', keys.mpAuthKey)
      .send(requests.missingEmail)

      .expect(400)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then((response) => {
        expect(response.body).to.eql(responses.emptyEmail);
      });
  });
  it('/POST /merchantprovision/create. Check email validation, invalid email', async function () {
    /*
       Steps:
       1. Send POST /merchantprovision/create with a valid body, but with invalid director email
       2. Verify correct error is returned
       */

    await supertest(env)
      .post(`/merchantprovision/create`)
      .set('Content-Type', 'application/json')
      .set('Authorization', keys.mpAuthKey)
      .send(requests.invalidEmail)

      .expect(400)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then((response) => {
        expect(response.body).to.eql(responses.invalidEmail);
      });
  });
  it('/POST /merchantprovision/create. Check abn validation, invalid abn', async function () {
    /*
       Steps:
       1. Send POST /merchantprovision/create with a valid body, but with invalid abn
       2. Verify correct error is returned
       */

    await supertest(env)
      .post(`/merchantprovision/create`)
      .set('Content-Type', 'application/json')
      .set('Authorization', keys.mpAuthKey)
      .send(requests.invalidAbn)

      .expect(400)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then((response) => {
        expect(response.body).to.eql(responses.invalidAbn);
      });
  });
  it('/POST /merchantprovision/create. Check intendedUse validation, invalid intendedUse', async function () {
    /*
       Steps:
       1. Send POST /merchantprovision/create with a valid body, but with intendedUse as a string
       2. Verify correct error is returned
       */

    await supertest(env)
      .post(`/merchantprovision/create`)
      .set('Content-Type', 'application/json')
      .set('Authorization', keys.mpAuthKey)
      .send(requests.invalidIntendedUse)

      .expect(400)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then((response) => {
        expect(response.body.errors.intendedUse[0]).to.contain(
          'Could not convert string to integer: money' + " laundering. Path 'intendedUse'",
        );
      });
  });
});

