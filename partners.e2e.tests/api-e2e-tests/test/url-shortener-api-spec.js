import supertest from 'supertest';
import dotenv from 'dotenv'
import helper from '../utils/api-helper';
import urlShortenerJson from '../json-data/request-bodies/url-shortener';
import urlShortenerImage from '../json-data/request-bodies/url-shortener-image.json';
import urlShortenerUtms from '../json-data/request-bodies/url-shortener-utms.json';
import assert from "assert";

dotenv.config()
const env = process.env.BASE_URL_SHORTENER_API_URL;


describe.skip('GET URL-Shortener API tests :heavy_exclamation_mark: https://zip-co.atlassian.net/browse/BP-958', function() {
  let UrlShortenerHash;

  it('GET /api/UrlShortener verify created config can be fetched', async function() {
    // Pre-create a config to make sure we have what to pull
    UrlShortenerHash = await helper.getUrlShortenerHash();
    await supertest(env)
      .get(`/api/UrlShortener?page=1&pageSize=100000000`)
      .set('Accept', 'application/json')
      .expect(200)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then(response => {
        let returned_json
        //Verify that the returned response contains URL we've pre-created
        for(let element in response.body){
          if (element['id'] == UrlShortenerHash){
            returned_json = element}
          else {return `${UrlShortenerHash} Element not found in response`}}
        expect(returned_json ==  urlShortenerJson)
      });
    });

  it('GET /api/UrlShortener check response keys valid params', async function() {
    await supertest(env)
      .get(`/api/UrlShortener?page=1&pageSize=10`)
      .set('Accept', 'application/json')
      .expect(200)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then(response => {
        assert(Object.keys(response.body).includes('totalCount'))
        assert(Object.keys(response.body).includes('current'))
        assert(Object.keys(response.body).includes('pageSize'))
        assert(Object.keys(response.body).includes('totalPages'))
        assert(Object.keys(response.body).includes('items'))
      });
    });

  it('GET /api/UrlShortener check current page param', async function() {
    let page = 2
    await supertest(env)
      .get(`/api/UrlShortener?page=${page}&pageSize=10`)
      .set('Accept', 'application/json')
      .expect(200)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then(response => {
        assert.strictEqual(response.body['current'], page)
      });
  });

  it('GET /api/UrlShortener check page size param', async function() {
    let page_size = 2
    await supertest(env)
      .get(`/api/UrlShortener?page=1&pageSize=${page_size}`)
      .set('Accept', 'application/json')
      .expect(200)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then(response => {
        assert.strictEqual(response.body['pageSize'], page_size)
      });
  });

describe('POST URL-Shortener API  tests', function() {
  it('POST /api/UrlShortener check response valid data', async function() {
    let post_response = await helper.getUrlShortenerHash()
        // Verify that response body is not empty when POSTing a valid config
        assert(post_response)
      });

  it('POST /api/UrlShortener check UTMs', async function() {
    await supertest(env)
      .post(`/api/UrlShortener`)
      .set('Content-type', 'application/json')
      .set('Accept', 'application/json')
      .send(urlShortenerUtms)
      .expect('Content-Type', 'text/plain; charset=utf-8')
      .expect(200)
      .then(response => {
        // Verify that response body is not empty when POSTing a valid config
        assert(response)
      });
  });

  it('POST api/image check response valid data', async function() {
    await supertest(env)
      .post(`/api/image?filename=testimage`)
      .set('Accept', 'application/json')
      .send(urlShortenerImage)
      .expect(200)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then(response => {
        assert.strictEqual(response.body, '200')
      });
  });

});
});