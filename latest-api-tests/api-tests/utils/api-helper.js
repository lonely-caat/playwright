const supertest = require('supertest');
const chalk = require('chalk');
const base = require('./base-library');
const contactDetails = require('../json-data/request-bodies/create-contact-details.json');
require('dotenv-safe').config();
const merchant_unique_id = process.env.MERCHANT_UNIQUE_ID;

const request = supertest(process.env.BASE_URL_STAG);

module.exports = {
  async submitContactDetails() {
    const response = await request
      .put(`/merchantprofile/${merchant_unique_id}/contact-details`)
      .set(await base.authHeaderGeneric())
      .send(contactDetails)
      .catch((error) => error.response);
    if (response.status === 200) {
      return response.status;
    }
    return console.log(
      chalk.red('ERROR: submitting contact detail has failed, unable to update contact details'),
    );
  },
};
