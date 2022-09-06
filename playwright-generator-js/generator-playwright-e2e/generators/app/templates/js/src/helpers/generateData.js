const { v4 } = require('uuid');

module.exports = {
  createRandomEmail() {
    `qa-${v4()}@mailinator.com`;
  },
  randomId() {
    v4().replace(/-/g, '');
  },
};
