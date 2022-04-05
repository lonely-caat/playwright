const SignIn = require('./SignIn');
const Refund = require('./Refund');

module.exports = (page) => ({
  refund: Refund(page),
  signIn: SignIn(page),
});
