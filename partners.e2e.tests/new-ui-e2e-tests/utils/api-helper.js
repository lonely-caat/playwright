require('dotenv-safe').config();
import * as data from '../data/app-data';
const supertest = require('supertest');
import { t } from 'testcafe';
import * as constants from '../data/constants';


let mailApi = data.mailinator.baseURL;

class ApiHelper {
  async createUser(
    useremail = null,
    zipproduct = 'zipCredit',
    purchAmount = null,
    zipcharge = null,
  ) {
    const request = supertest(data.createUser.baseUrl);
    const response = await request
      .post(data.createUser.endpoint)
      .set({
        'Content-Type': 'application/json',
        connection: 'keep-alive',
      })
      .send({
        product: zipproduct,
        env: 'sand',
        token: data.createUser.token,
        charge: zipcharge,
        purchaseAmount: purchAmount,
        email: useremail,
      })
      .catch(() => {
        throw new Error(`Could not create test user. Api is non reachable`);
      });
    if (response.statusCode !== 200) {
      throw new Error(
        `Could not create test user. API call returned this error code: ${response.statusCode}`,
      );
    }
    return response;
  }

  async getMockMobileCodeResponse(mobileNumber) {
    try {
      const request = supertest(data.mockApi.baseUrl.dev);
      const response = await request.get(`${data.mockApi.endPoint}/${mobileNumber}`);
      if (response) {
        return response;
      }
    } catch (error) {
      return error.response;
    }
  }

  async getMailinatorInbox(userId) {
    try {
      const request = supertest(mailApi);
      process.env.NODE_TLS_REJECT_UNAUTHORIZED = 0;
      const response = await request.get(`/${userId}`).set({
        'Content-Type': 'application/json',
        'Authorization':`${data.mailinator.apiKey}`
      });
      process.env.NODE_TLS_REJECT_UNAUTHORIZED = 1;
      if (response) {
        return response;
      }
    } catch (error) {
      return error.response;
    }
  }

  async getMailinatorEmailBySubject(userId, subject, wait_time= 9000) {
    try {
      let response, msgCount, msgId;
      let found = false;
      await t.wait(wait_time)
      for (var j = 0; j < 10; j++) {
        new Promise(resolve => setTimeout(resolve, 6000));
        response = await this.getMailinatorInbox(userId);
        msgCount = response.body.msgs.length;
        for (var i = 0; i < msgCount; i++) {
          if (response.body.msgs[i].subject === subject) {
            msgId = response.body.msgs[i].id;
            found = true;
            break;
          }}
        if (found) {break;
        }}
      if (!found){
          return `Failed to find email with subject '${subject}' in ${response.body}`}
      process.env.NODE_TLS_REJECT_UNAUTHORIZED = 0;
      const request = supertest(mailApi);
      response = await request.get(`/${userId}/messages/${msgId}`).set({
        'Content-Type': 'application/json',
        'Authorization':`${data.mailinator.apiKey}`
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
  async createInstorePin(product, email) {
    try {
      const request = supertest(process.env.E2E_API_URL);
      const response = await request
        .post(constants.apiEndPoints.createInstorePin)
        .set({
          'Content-Type': 'application/json',
          connection: 'keep-alive',
        })
        .send({
          classification: product,
          customerEmail: email,
        });
      if (response) {
        return response;
      }
    } catch (error) {
      return error.response;
    }
  }
}

module.exports = new ApiHelper();

