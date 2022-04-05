const { v4 } = require('uuid');

module.exports = {
  createRandomEmail() {
    `qa-${v4()}@zipteam222898.testinator.com`;
  },
  randomId() {
    v4().replace(/-/g, '');
  },
};
