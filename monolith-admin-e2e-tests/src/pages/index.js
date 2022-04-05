const NavBar = require('./NavBar');
const SignIn = require('./SignIn');
const Customers = require('./Customers');
const Applications = require('./Applications');
const Merchants = require('./Merchants');
const Transactions = require('./Transactions');
const UserManagement = require('./UserManagement');


module.exports = (page) => ({
  navBar: NavBar(page),
  customers: Customers(page),
  signIn: SignIn(page),
  applications: Applications(page),
  merchants: Merchants(page),
  transactions: Transactions(page),
  userManagement: UserManagement(page),
});
