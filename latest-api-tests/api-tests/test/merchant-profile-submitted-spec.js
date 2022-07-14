const supertest = require('supertest');
const base = require('../utils/base-library');
const submitProfileStatusDetails = require('../json-data/response-bodies/get-merchant-profile-submitted-details.json');
const data = require('../test-data/merchant-data.json');
require('dotenv-safe').config();

const request = supertest(process.env.BASE_URL_STAG);
const merchant_unique_id = data.merchantWithSubmittedProfile.merchantUniqueId;
const merchant_email = data.merchantWithSubmittedProfile.merchantEmail;

describe('Merchant Profile Submitted', () => {
  let authHeaderGeneric;

  beforeAll(async () => {
    authHeaderGeneric = await base.authHeaderGeneric(merchant_email);
  });
  describe('Positive Tests: get merchant details', () => {
    let getResponse;

    beforeAll(async () => {
      getResponse = await request
        .get(`/merchantprofile/${merchant_unique_id}`)
        .set(authHeaderGeneric);
    });

    it('returns 200 status', () => {
      expect(getResponse.statusCode).toBe(200);
    });

    it('returns the expected response', () => {
      expect(getResponse.body).toMatchObject(submitProfileStatusDetails);
    });
  });
  describe('Negative Tests: merchant profile submitted', () => {
    it('returns 401 with invalidate header', async () => {
      getResponse = await request.get(`/merchantprofile/${merchant_unique_id}`);
      expect(getResponse.status).toBe(401);
    });
  });
});
