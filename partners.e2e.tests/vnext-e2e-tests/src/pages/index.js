const Dashboard = require('./Dashboard');
const SignIn = require('./SignIn');
const RepaymentCalculator = require('./RepaymentCalculator');
const CreateOrder = require('./CreateOrder');
const InviteCustomer = require('./InviteCustomer');
const CustomerSearch = require('./CustomerSearch');
const Onboarding = require('./Onboarding');


module.exports = (page) => ({
  dashboard: Dashboard(page),
  repaymentCalculator: RepaymentCalculator(page),
  createOrder: CreateOrder(page),
  signIn: SignIn(page),
  inviteCustomer: InviteCustomer(page),
  customerSearch: CustomerSearch(page),
  onboarding: Onboarding(page),
});
