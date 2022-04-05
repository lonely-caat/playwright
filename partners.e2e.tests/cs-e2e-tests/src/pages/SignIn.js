const { byTagAndText } = require("../helpers/selectors");

const { USERS } = require('../constants/appData');


const elements = {
  emailField: '[data-testid="username"]',
  passwordField: 'input[name="password"]',
  submitButton: 'button:has-text("Continue")',
};

const SignIn = (pageInstance) =>
{

  async function performSignIn(email = USERS.user.adminEmail,
                               password = USERS.user.password) {
    const page = pageInstance;
    const {emailField, passwordField, submitButton} = elements;
    await page.waitForSelector(emailField);
    await page.fill(emailField, email);
    await page.click(submitButton);
    await page.waitForSelector(passwordField);
    await page.fill(passwordField, password);
    await page.click(submitButton);
    await page.waitForSelector(byTagAndText('span', 'Customers'));

  }
  return { performSignIn }
}

module.exports = SignIn;