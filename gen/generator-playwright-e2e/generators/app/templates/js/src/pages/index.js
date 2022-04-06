const transactionMFA = require('./TransactionMFA');
const ConfirmOrder = require('./ConfirmOrder');
const AccountSelector = require('./AccountSelector');
const Merchant = require('./Merchant');
const SignIn = require('./SignIn');
const SignInMFA = require('./SignInMFA');
const VerifyEmail = require('./VerifyEmail');
const WebWallet = require('./WebWallet');

module.exports = (page, frame = null) => ({
  accountSelector: AccountSelector(page),
  confirmOrder: ConfirmOrder(page, frame),
  merchantLuma: Merchant(page),
  signIn: SignIn(page, frame),
  transactionMFA: transactionMFA(page, frame),
  signInMFA: SignInMFA(page, frame),
  verifyEmail: VerifyEmail(page),
  webWallet: WebWallet(page),
});
