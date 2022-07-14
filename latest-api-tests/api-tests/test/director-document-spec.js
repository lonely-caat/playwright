const supertest = require('supertest');
const base = require('../utils/base-library');
const data = require('../test-data/merchant-data.json');
const uploadData = require('../json-data/request-bodies/create-upload-document.json');
const directorUploadData = require('../json-data/request-bodies/create-director-upload-document.json');
const directorDocumentDetails = require('../json-data/response-bodies/get-director-document-details.json');
require('dotenv-safe').config();

const request = supertest(process.env.BASE_URL_STAG);
const merchant_unique_id = data.merchantForDirector.merchantUniqueId;
const merchant_email = data.merchantForDirector.merchantEmail;

describe('Merchant Profile - Director Documents', () => {
  let authHeaderGeneric, getRequest;

  beforeAll(async () => {
    authHeaderGeneric = await base.authHeaderGeneric(merchant_email);
  });
  describe('Positive Tests: upload driving license document', () => {
    beforeAll(async () => {
      postRequest = await request
        .post(`/merchantprofile/${merchant_unique_id}/bank-documents/`)
        .set(authHeaderGeneric)
        .send(uploadData);
    });
    it('returns 200 status', () => {
      expect(postRequest.statusCode).toBe(200);
    });
    it('returns number and non null value for uploaded document', () => {
      expect(postRequest.body.documentId).not.toBeNull();
      expect(postRequest.body.documentId).toEqual(expect.any(Number));
    });
  });

  describe('Negative Tests: upload driving license', () => {
    it('returns 401 response with invalid credential', async () => {
      let postRequest = await request
        .post(`/merchantprofile/${merchant_unique_id}/bank-documents/`)
        .send(uploadData);
      expect(postRequest.statusCode).toBe(401);
    });
    it('returns 400 response without upload content', async () => {
      let postRequest = await request
        .post(`/merchantprofile/${merchant_unique_id}/bank-documents/`)
        .set(authHeaderGeneric);
      expect(postRequest.statusCode).toBe(400);
    });
  });

  describe('Positive Tests: upload director documents', () => {
    beforeAll(async () => {
      postRequest = await request
        .put(`/merchantprofile/${merchant_unique_id}/director-documents/`)
        .set(authHeaderGeneric)
        .send(directorUploadData);
    });
    it('returns 204 status', () => {
      expect(postRequest.statusCode).toBe(204);
    });
  });

  describe('Negative Tests: upload director documents', () => {
    it('returns 401 response with invalid credential', async () => {
      let postRequest = await request
        .post(`/merchantprofile/${merchant_unique_id}/director-documents/`)
        .send(uploadData);
      expect(postRequest.statusCode).toBe(401);
    });
    it('returns 400 response without upload content', async () => {
      let postRequest = await request
        .post(`/merchantprofile/${merchant_unique_id}/director-documents/`)
        .set(authHeaderGeneric);
      expect(postRequest.statusCode).toBe(400);
    });
  });
  describe('Positive Tests: get director document', () => {
    beforeAll(async () => {
      getRequest = await request
        .get(`/merchantprofile/${merchant_unique_id}/director-documents/`)
        .set(authHeaderGeneric);
    });
    it('returns 200 status', () => {
      expect(getRequest.statusCode).toBe(200);
    });
    it('returns the expected response', () => {
      expect(getRequest.body).toMatchObject(directorDocumentDetails);
      expect(getRequest.body.id).toEqual(expect.any(Number));
    });
  });
  describe('Negative Tests: get director document', () => {
    it('returns 401 response with invalid credential', async () => {
      getRequest = await request.get(`/merchantprofile/${merchant_unique_id}/director-documents/`);
      expect(getRequest.statusCode).toBe(401);
    });
  });
});
