const supertest = require('supertest');
const base = require('../utils/base-library');
const contactDetails = require('../json-data/request-bodies/create-contact-details.json');
const getContactDetails = require('../json-data/response-bodies/get-contact-details.json');
const data = require('../test-data/merchant-data.json');
require('dotenv-safe').config();

const request = supertest(process.env.BASE_URL_STAG);
const merchant_unique_id = data.merchantForContactDirectorDetails.merchantUniqueId;
const merchant_email = data.merchantForContactDirectorDetails.merchantEmail;

describe('Contact details tests', () => {
  let authHeaderGeneric;

  beforeAll(async () => {
    authHeaderGeneric = await base.authHeaderGeneric(merchant_email);
  });
  describe('Positive Tests: submit contact details', () => {
    let postResponse;
    let positiveContactDetails = contactDetails.positive_data;
    beforeAll(async () => {
      postResponse = await request
        .put(`/merchantprofile/${merchant_unique_id}/contact-details`)
        .set(authHeaderGeneric)
        .send(positiveContactDetails);
    });

    it('returns 200 status', () => {
      expect(postResponse.statusCode).toBe(200);
    });
  });
  describe('Negative Tests: submit contact details', () => {
    it('returns 400 with incorrect first name', async () => {
      let negativeContactDetails = contactDetails.negative_data.invalid_name;
      putResponse = await request
        .put(`/merchantprofile/${merchant_unique_id}/contact-details`)
        .set(authHeaderGeneric)
        .send(negativeContactDetails);
      expect(putResponse.status).toBe(400);
    });
    it('returns 400 with incorrect email address', async () => {
      let negativeContactDetails = contactDetails.negative_data.invalid_email;
      putResponse = await request
        .put(`/merchantprofile/${merchant_unique_id}/contact-details`)
        .set(authHeaderGeneric)
        .send(negativeContactDetails);
      console.log(putResponse.text)
      expect(putResponse.status).toBe(400);
    });
    it('returns 400 with incorrect phone number', async () => {
      let negativeContactDetails = contactDetails.negative_data.invalid_contact;
      putResponse = await request
        .put(`/merchantprofile/${merchant_unique_id}/contact-details`)
        .set(authHeaderGeneric)
        .send(negativeContactDetails);
      expect(putResponse.status).toBe(400);
    });
  });
  describe('Positive Tests: get contact details', () => {
    let getResponse;
    beforeAll(async () => {
      getResponse = await request
        .get(`/merchantprofile/${merchant_unique_id}/contact-details`)
        .set(authHeaderGeneric);
    });
    it('returns 200 status', async () => {
      expect(getResponse.statusCode).toBe(200);
    });
    it('returns the expected response', async () => {
      expect(getResponse.body).toMatchObject(getContactDetails);
    });
  });
  describe('Negative Tests: get contact details with invalidate header', () => {
    let getResponse;
    beforeAll(async () => {
      getResponse = await request.get(`/merchantprofile/${merchant_unique_id}/contact-details`);
    });
    it('returns 401 status', async () => {
      expect(getResponse.statusCode).toBe(401);
    });
  });
});
