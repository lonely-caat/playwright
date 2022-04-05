const { chromium } = require('@playwright/test');
const { USERS } = require('../constants/usersData');
const urls = require("../constants/urls");
const {byTagAndText} = require("../helpers/selectors");

const email = USERS.merchant5.adminEmail
const password = USERS.merchant5.password


const elements = {
  header: 'text=Sign in as Zip partner',
  emailField: "#username",
  passwordField: "#password",
  continueButton: 'button:has-text("Continue")',
  skipAddon: '[data-testid="skip-extension"]',
  productSearch: '#search-input',
  adminSandBoxSelector: '[aria-label="Launch Admin - Sandbox"] div',
};

module.exports = async config => {

    const browser = await chromium.launch();
    const page = await browser.newPage();
    await page.goto(urls.login);

    const {emailField, passwordField, submitButton} = elements;
    await page.waitForSelector(emailField);
    await page.fill(emailField, email);
    await page.fill(passwordField, password);
    await page.click(submitButton);
    await page.waitForSelector(byTagAndText('h1', 'Dashboard'));

    await page.context().storageState({ path: 'storageState.json' });
}
