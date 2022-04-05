const {byTagAndText} = require("../helpers/selectors");

const actions = require('../helpers/actions');
const { USERS } = require('../constants/usersData');


const elements = {
  header: 'text=Sign in as Zip partner',
  emailField: "input[type='email']",
  passwordField: "input[type='password']",
  submitButton: '[aria-label="Sign in"]',
};

const SignIn = (pageInstance) =>
{

  async function performSignIn(email = USERS.merchant5.adminEmail,
                               password = USERS.merchant5.password) {
    const page = pageInstance;
    const {emailField, passwordField, submitButton} = elements;
    await page.waitForSelector(emailField);
    await page.fill(emailField, email);
    await page.fill(passwordField, password);
    await page.click(submitButton);
    await page.waitForSelector(byTagAndText('h1', 'Dashboard'));

  }
  return { performSignIn }
}

module.exports = SignIn;
