const supertest = require('supertest');
const base = require('../utils/base-library');
const profileDetails = require('../json-data/response-bodies/get-profile-details.json');
const data = require('../test-data/merchant-data.json');
require('dotenv-safe').config();

const request = supertest(process.env.BASE_URL_STAG);
const merchant_unique_id = data.merchantForContactDirectorDetails.merchantUniqueId;
const merchant_email = data.merchantForContactDirectorDetails.merchantEmail;

describe('Merchant Profile tests', () => {
  let authHeaderGeneric;

beforeAll(async () => {
    authHeaderGeneric = await base.authHeaderGeneric(merchant_email);
  });
describe('Positive Tests: get merchant details', () => {
    let getResponse;
    let inProgressMerchantDetails = profileDetails.inProgressMerchantProfile;

  it('returns the expected response', async() => {
      getResponse = await request
        .get(`/merchantprofile/${merchant_unique_id}`)
        .set(authHeaderGeneric);
      console.log(getResponse.request)
      expect(getResponse.statusCode).toBe(200);
      expect(getResponse.body).toMatchObject(inProgressMerchantDetails);
      expect(getResponse.headers['content-type']).toStrictEqual('application/json; charset=utf-8')
    });
  });

describe('Positive Tests: post merchant details', () => {

    it('returns the expected response', async() => {
      /*
      Steps:
      1. Send POST /merchantprofile/{id} to change profile details
      2. Send GET /merchantprofile/{id} to verify profile details were changed
      3. Send POST /merchantprofile/{id} to change profile details back to the original value
      */
      await request
        .post(`/merchantprofile/${merchant_unique_id}`)
        .set(authHeaderGeneric)
        .set("Content-Type", "application/json")
        .set("Accept", "application/json")
        .send('{"submitted": false,"flow": "B"}')
        .expect(200)

      await request
        .get(`/merchantprofile/${merchant_unique_id}`)
        .set(authHeaderGeneric)
        .expect(200)
        .expect((response) => {
          response.text.includes({"submitted": false,"flow": "B"})});

      await request
        .post(`/merchantprofile/${merchant_unique_id}`)
        .set(authHeaderGeneric)
        .set("Content-Type", "application/json")
        .set("Accept", "application/json")
        .send('{"submitted": true,"flow": "A"}')
        .expect(200)
    });
  });

describe('Negative Tests: merchant profile', () => {
  it('returns 401 with no body when Auth header is missing', async () => {
      let getResponse = await request
        .get(`/merchantprofile/${merchant_unique_id}`);
      expect(getResponse.status).toBe(401);
      expect(getResponse.headers['content-length']).toBe("0")
    });

  it('returns 401 with no body when Auth header is invalid', async () => {
    let getResponse = await request
      .get(`/merchantprofile/${merchant_unique_id}`)
      .set({Authorization: 'Bearer meow'});
    expect(getResponse.status).toBe(401);
    expect(getResponse.headers['content-length']).toBe("0")
  });

  it('returns 401 with no body when merchantUniqueId is invalid', async () => {
    let getResponse = await request
      .get(`/merchantprofile/idont-exist-at-all-404`)
      .set(authHeaderGeneric);
    expect(getResponse.status).toBe(401);
  });
  });
});
