const config = require('../config');

const env = config.isDev ? 'dev' : 'sandbox';
const envToken = config.isDev ? 'dev1' : 'sandbox';
const host = `https://merchant-dash.${env}.zip.co`;
const generateTokenHost = `https://e2eapi.${envToken}.zipmoney.com.au`
const websiteHost = `https://${env}.zip.co`

const oneLoginHost = 'https://zip-money-au.onelogin.com'
const monolithHost = `https://admin.${env}.zipmoney.com.au`

const urls = {
  login: `${host}/login`,
  generateToken: `${generateTokenHost}/createPin`,
  websitePaymentsUrl: `${websiteHost}/au/business/payments/apply`,

  oneLogin: `${oneLoginHost}/login2`,
  monolithLandingPage: `${monolithHost}/Information`

};
module.exports = urls;
