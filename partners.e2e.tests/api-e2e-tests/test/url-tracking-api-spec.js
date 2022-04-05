import supertest from 'supertest';
import dotenv from 'dotenv-safe'
import assert from "assert";

dotenv.config()

const env = process.env.BASE_URL_TRACKING_API_URL;


describe('GET /api/urltracking/url/{key} tests', function() {
    it('check response keys valid input', async function() {
        let key = "04df55de"
        await supertest(env)
            .get(`/api/urltracking/url/${key}`)
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                let response_body = JSON.parse(response.body)
                assert(Object.keys(response_body).includes('Key'))
                assert(Object.keys(response_body).includes('Url'))
            });
    });

    it('check character validation', async function() {
        let key = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~:/?#[]@$&'()*+,;="
        await supertest(env)
            .get(`/api/urltracking/url/${key}`)
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                assert.notStrictEqual(response.body, `{"Key":${key},"Url":"https://www.zippay.com.au/stores/"}`)
            });
    });

    it('check minimum character validation', async function() {
        let key = "w"
        await supertest(env)
            .get(`/api/urltracking/url/${key}`)
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                assert.notStrictEqual(response.body, `{"Key":${key},"Url":"https://www.zippay.com.au/stores/"}`)
            });
    });
    it('check maximum character validation', async function() {
        // check out this SO about maximum URL length
        let key = "w".repeat(250)
        await supertest(env)
            .get(`/api/urltracking/url/${key}`)
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                assert.notStrictEqual(response.body, `{"Key":${key},"Url":"https://www.zippay.com.au/stores/"}`)
            });
    });

    it('check default response unexisting key', async function() {
        let key = "this_key_does_not_exist"
        await supertest(env)
            .get(`/api/urltracking/url/${key}`)
            .set('Accept', 'application/json')
            .expect(200)
            .expect('Content-Type', 'application/json; charset=utf-8')
            .then(response => {
                assert.notStrictEqual(response.body, `{"Key":${key},"Url":"https://www.zippay.com.au/stores/"}`)
            });
    });
});
