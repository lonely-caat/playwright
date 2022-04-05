import dotenv from 'dotenv-safe'
import assert from "assert";
import supertest from "supertest";

dotenv.config()
const env = process.env.BASE_URL_COMMUNICATIONS_API;


describe('GET /api/sms/ tests',  function() {

  it('GET /arrears-notification-types Check that expected JSON of notification types is returned', async function() {
    /*
   Steps:
   1. Send GET /arrears-notification-types
   2. Verify response
   */
    await supertest(env)
      .get(`/api/sms/arrears-notification-types`)
      .set('Accept', 'application/json')
      .expect(200)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then(response => {
        assert.strictEqual(response.text, "[{\"key\":1,\"value\":\"Expired Card\"},{\"key\":3,\"value\":\"Insufficient Funds\"},{\"key\":4,\"value\":\"SMS REM 1 (3 Days)\"},{\"key\":5,\"value\":\"SMS REM 2 (7 Days)\"},{\"key\":6,\"value\":\"SMS REM 3 (LPF Warning)\"},{\"key\":7,\"value\":\"SMS STRONG\"},{\"key\":15,\"value\":\"Bank DD Incorrect Details\"},{\"key\":19,\"value\":\"PayNow Link\"},{\"key\":20,\"value\":\"Courteous Callback Request\"},{\"key\":21,\"value\":\"Failed Callback Request\"},{\"key\":22,\"value\":\"Update Card Details\"}]")
      });
  });

  it('GET "/Expired Card" Check that response is valid for each notification type', async function() {
    /*
   Steps:
   1. Send GET /Expired Card
   2. Verify response
   */
    await supertest(env)
      .get(`/api/sms/content/expired%20card`)
      .set('Accept', 'application/json')
      .expect(200)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then(response => {
        let response_parsed = JSON.parse(response.text)
        assert.strictEqual(response_parsed['content'], "Just letting you know the card used for your {classification} repayments has now expired. Please go to {account-url} to update your details and make a payment")
      });
  });
  it('GET "/insufficient funds" Check that response is valid for each notification type', async function() {
    /*
   Steps:
   1. Send GET /insufficient funds
   2. Verify response
   */
    await supertest(env)
      .get(`/api/sms/content/insufficient%20funds`)
      .set('Accept', 'application/json')
      .expect(200)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then(response => {
        let response_parsed = JSON.parse(response.text)
        assert.strictEqual(response_parsed['content'], "Just letting you know there were insufficient funds for your {classification} repayment. Please go to {account-url} to make payment or re-schedule")
      });
  });

  it('GET "/SMS REM 1 (3 Days)" Check that response is valid for each notification type', async function() {
    /*
   Steps:
   1. Send GET /insufficient funds
   2. Verify response
   */
    await supertest(env)
      .get(`/api/sms/content/SMS%20REM%201%20(3%20Days)`)
      .set('Accept', 'application/json')
      .expect(200)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then(response => {
        let response_parsed = JSON.parse(response.text)
        assert(response_parsed['content'].includes("Just a friendly reminder that your {classification} account is OVERDUE"))
      });
  });
  it('GET "/SMS REM 2 (7 Days)" Check that response is valid for each notification type', async function() {
    /*
   Steps:
   1. Send GET /SMS REM 2 (7 Days)
   2. Verify response
   */
    await supertest(env)
      .get(`/api/sms/content/SMS%20REM%202%20(7%20Days)`)
      .set('Accept', 'application/json')
      .expect(200)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then(response => {
        let response_parsed = JSON.parse(response.text)
        assert.strictEqual(response_parsed['content'], "Please note your {classification} account remains OVERDUE. Please contact us or make payment at {account-url} asap")
      });
  });
  it('GET "/SMS REM 3 (LPF Warning)" Check that response is valid for each notification type', async function() {
    /*
   Steps:
   1. Send GET /SMS REM 3 (LPF Warning)
   2. Verify response
   */
    await supertest(env)
      .get(`/api/sms/content/SMS%20REM%203%20(LPF%20Warning)`)
      .set('Accept', 'application/json')
      .expect(200)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then(response => {
        let response_parsed = JSON.parse(response.text)
        assert(response_parsed['content'].includes("{classification} OVERDUE - to avoid a Late Payment Fee"))
      });
  });

  it('GET "/SMS STRONG" Check that response is valid for each notification type', async function() {
    /*
   Steps:
   1. Send GET /SMS STRONG 2 (7 Days)
   2. Verify response
   */
    await supertest(env)
      .get(`/api/sms/content/SMS%20STRONG`)
      .set('Accept', 'application/json')
      .expect(200)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then(response => {
        let response_parsed = JSON.parse(response.text)
        assert.strictEqual(response_parsed['content'], "Your {classification} account is now SERIOUSLY OVERDUE. Please contact us at info@zipmoney.com.au or (02) 8003 4322 immediately regarding your account")
      });
  });
  it('GET "PayNow Link" Check that response is valid for each notification type', async function() {
    /*
   Steps:
   1. Send GET /PayNow Link
   2. Verify response
   */
    await supertest(env)
      .get(`/api/sms/content/PayNow%20Link`)
      .set('Accept', 'application/json')
      .expect(200)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then(response => {
        let response_parsed = JSON.parse(response.text)
        assert.strictEqual(response_parsed['content'], "Hi {first-name}, your {classification} account is OVERDUE. Pay now by clicking here {payNowUrl}")
      });
  });
});