const config = require('../config');

const env = config.isDev ? 'dev' : 'sandbox';
const envToken = config.isDev ? 'dev1' : 'sandbox';
const host = `https://merchant-dash.${env}.zip.co`;
const generateTokenHost = `https://e2eapi.${envToken}.zipmoney.com.au`
const websiteHost = `https://${env}.zip.co`

const urls = {
  hostName: host,
  login: `${host}/login`,
  generateToken: `${generateTokenHost}/createPin`,
  websitePaymentsUrl: `${websiteHost}/au/business/payments/apply`,

};
module.exports = urls;
