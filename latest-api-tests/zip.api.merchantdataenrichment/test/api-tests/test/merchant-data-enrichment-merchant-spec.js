const supertest = require('supertest');
const merchantDetails = require('../json-data/response-bodies/get-merchant-details.json');
require('dotenv-safe').config();

const request = supertest(process.env.BASE_URL);

describe('Merchant tests', () => {
  describe('Positive Tests: merchant', () => {
    // Retrieve merchant detail either from persisted database or from Look Who's Charging 3rd party Api
    it('Merchant Lookup returns valid response, status and headers', async () => {
      let getResponse = await request
        .get(`/api/v1/Merchant/lookup-merchantDetail`)
        .set({accept: 'text/plain'})
        .query({cardAcceptorName: 'Bowery Lane', cardAcceptorCity: 'Sydney' });
        expect(getResponse.status).toBe(200);
      expect(getResponse.headers['content-type']).toBe('application/json; charset=utf-8');
      // TODO dirty workaround; figure out the date format the API uses
      delete getResponse.body['merchantDetail']['updatedDateTime'];
      expect(getResponse.body).toMatchObject(merchantDetails);
    });
    describe('Negative Tests: merchant', () => {
      it(
        'Merchant Lookup returns valid response when unexisting cardAcceptorName is provided' +
          'bug or misconfiguration: "Timeout searching for Does not exist Sydney" in response',
        async () => {
          let getResponse = await request
            .get(`/api/v1/Diagnostics/lookupMerchantDetails`)
            .query({ cardAcceptorName: 'Does not exist', cardAcceptorCity: 'Sydney' });
            expect(getResponse.status).toBe(204);
            expect(getResponse.body['success']).toBeUndefined();
        },
      );

      it('Merchant Lookup returns valid response when invalid city is provided',
        async () => {
          let getResponse = await request
            .get(`/api/v1/Diagnostics/lookupMerchantDetails`)
            .query({ cardAcceptorName: 'Bowery Lane', cardAcceptorCity: 'Paris' });
            expect(getResponse.status).toBe(204);
            expect(getResponse.body['success']).toBeUndefined();
        },
      );

      it('Merchant Lookup returns valid response when required query params are missing' +
          'bug or misconfiguration: "Timeout searching for Does not exist Sydney" in response',
        async () => {
          let getResponse = await request
            .get(`/api/v1/Diagnostics/lookupMerchantDetails`)
            .query({ cardAcceptorName: 'Bowery' });
            expect(getResponse.status).toBe(204);
            expect(getResponse.body['success']).toBeUndefined();
        },
      );

      it('Merchant Lookup returns valid response without required query params', async () => {
        let getResponse = await request.get(`/api/v1/Diagnostics/lookupMerchantDetails`);
        expect(getResponse.status).toBe(424);
        expect(getResponse.body['success']).toEqual(false);
        expect(getResponse.body['error']['message']).toContain("System.Exception: Failed to get success " +
            "HttpResponse when getting MerchantDetail for Request")
      });
    });
  });
});
