import supertest from 'supertest';
import urlShortenerJson from '../json-data/request-bodies/url-shortener';
import dotenv from 'dotenv-safe';
import { mailinator } from '../data/test-data'

dotenv.config()
const request_url_shortener_api = supertest(process.env.BASE_URL_SHORTENER_API_URL);
const request_communications_api = supertest(process.env.BASE_URL_COMMUNICATIONS_API);
const mailApi = mailinator.baseURL;



module.exports = {
  async getUrlShortenerHash() {
    /* Function returns URL hash object that can be used for retrieving shortened URL data
       Used as an entry point for url-shortener API GET tests
     */
    const response = await request_url_shortener_api
      .post(`/api/UrlShortener`)
      .set({ 'Content-Type': 'application/json' })
      .send(urlShortenerJson)
      .catch((error) => error.response);
    if (response.status === 200) {
      return response.text;
    } else {
      return console.log(`POST /api/UrlShortener returned ${response.status} error`)
    }
  },
  getRandomString(characterLength) {
    let randomText = '';
    const possible = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
    for (let i = 0; i < characterLength; i += 1) {
      randomText += possible.charAt(Math.floor(Math.random() * possible.length));
    }
    return randomText;
  },
  async send_close_account_email(email) {
    /* Function requests /api/emails/send/close-account endpoint to send email to ${email}
     */
    const response = await request_communications_api
      .post(`/api/v2/Email/send/credit-decline`)
      .set('Accept', 'application/json')
      .set('Content-Type', 'application/json')
      .send({ "firstName": "name", "product": "ZipMoney", "accountNumber": "123", "email": email })
      .catch((error) => error.response);
    if (response.status === 200) {
      return response.json;
    } else {
      return console.log(`POST /api/UrlShortener returned ${response.status} error`)
    }
  },
  async getMailinatorInbox(userId) {
    try {
      const request = supertest(mailApi);
      process.env.NODE_TLS_REJECT_UNAUTHORIZED = 0;
      const response = await request.get(`/${userId}`).set({
        'Content-Type': 'application/json',
        'Authorization':`${mailinator.apiKey}`
      });
      process.env.NODE_TLS_REJECT_UNAUTHORIZED = 1;
      if (response) {
        console.log(response, '88888888888888898989989898989989898')
        return response;
      }
    } catch (error) {
      return error.response;
    }
  },
  async getMailinatorEmailBySubject(userId, subject, wait_time= 9000) {
    try {
      let response, msgCount, msgId;
      let found = false;
      await new Promise(resolve => setTimeout(resolve, wait_time));
      for (let j = 0; j < 10; j++) {
        new Promise(resolve => setTimeout(resolve, 6000));
        response = await this.getMailinatorInbox(userId);
        msgCount = response.body.msgs.length;
        for (let i = 0; i < msgCount; i++) {
          if (response.body.msgs[i].subject === subject) {
            msgId = response.body.msgs[i].id;
            found = true;
            break;
          }}
        if (found) {break;
        }}
      if (!found){
        console.log(response.body.msgs)
        return `Failed to find email with subject '${subject}' in ${response.body}`}
      process.env.NODE_TLS_REJECT_UNAUTHORIZED = 0;
      const request = supertest(mailApi);
      response = await request.get(`/${userId}/messages/${msgId}`).set({
        'Content-Type': 'application/json',
        'Authorization':`${mailinator.apiKey}`
      });
      process.env.NODE_TLS_REJECT_UNAUTHORIZED = 1;
      let content = JSON.stringify(response.body.parts[0].headers);
      if (response) {
        if (content.includes('text/html')) {
          return JSON.stringify(response.body.parts[0].body);
        }
        else {
          return JSON.stringify(response.body.parts[1].body);
        }

      }
    } catch (error) {
      return error;
    }
  }
}


