require('dotenv-safe').config();

module.exports = {
  generateId() {
    return Math.floor(Math.random() * 1000000);
  },
};
