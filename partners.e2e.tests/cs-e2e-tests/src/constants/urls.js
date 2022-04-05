const config = require('../config');

const env = config.isDev ? 'dev' : 'sand';
const host = `https://admin-cs.${env}.au.edge.zip.co`;
const urls = {
  login: `${host}/customers/182597`,
};
module.exports = urls;
