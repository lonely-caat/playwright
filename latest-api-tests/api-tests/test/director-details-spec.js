const supertest = require('supertest');
const base = require('../utils/base-library');
const directorDetails = require('../json-data/request-bodies/create-director-details.json');
const getDirectorDetails = require('../json-data/response-bodies/get-director-details.json');
const data = require('../test-data/merchant-data.json');
require('dotenv-safe').config();

const request = supertest(process.env.BASE_URL_STAG);
const merchant_unique_id = data.merchantForContactDirectorDetails.merchantUniqueId;
const merchant_email = data.merchantForContactDirectorDetails.merchantEmail;

describe('Director details tests', () => {
  let authHeaderGeneric;

  beforeAll(async () => {
    authHeaderGeneric = await base.authHeaderGeneric(merchant_email);
  });
  describe('Positive Tests: submit director details', () => {
    let postResponse;
    let positiveDirectorDetails = directorDetails.positive_data;
    beforeAll(async () => {
      postResponse = await request
        .put(`/merchantprofile/${merchant_unique_id}/director-details`)
        .set(authHeaderGeneric)
        .send(positiveDirectorDetails);
    });

    it('returns 200 status', () => {
      expect(postResponse.statusCode).toBe(200);
    });
  });

  describe('Positive Tests: get director details', () => {
    let getResponse;
    beforeAll(async () => {
      getResponse = await request
        .get(`/merchantprofile/${merchant_unique_id}/director-details`)
        .set(authHeaderGeneric);
    });
    it('returns 200 status', async () => {
      expect(getResponse.statusCode).toBe(200);
    });
    it('returns the expected response', async () => {
      expect(getResponse.body).toMatchObject(getDirectorDetails);
    });
  });
  describe('Negative Tests: get director details with invalid header', () => {
    let getResponse;
    beforeAll(async () => {
      getResponse = await request.get(`/merchantprofile/${merchant_unique_id}/director-details`);
    });
    it('returns 401 status', async () => {
      expect(getResponse.statusCode).toBe(401);
    });
  });
});
