const request = require('superagent');
require('dotenv-safe').config();

  async function getAuthToken(email) {
    return request
     .post(`${process.env.BASE_ZIP_URL}/login/connect/token`)
     .set('Content-Type', 'application/x-www-form-urlencoded')
     .send({ client_id: process.env.CLIENT_ID })
     .send({ client_secret: process.env.CLIENT_SECRET })
     .send({ grant_type: process.env.GRANT_TYPE })
     .send({ username: `${email}` })
     .send({ password: process.env.PASSWORD })
     .catch((error) => error.response);
 }

module.exports = {
   async getAuthHeader(email) {
      const response = await getAuthToken(email);
      return  `${response.body.access_token}`
    },
};
