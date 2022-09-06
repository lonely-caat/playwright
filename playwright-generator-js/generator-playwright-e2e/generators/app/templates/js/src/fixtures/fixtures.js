const base = require('@playwright/test');
const { TEST_TIMEOUT } = require('../config');
const urls = require('../constants/urls');
const Authenticate = require('../steps/Authenticate');
const createPages = require('../pages/index');
const actions = require('../helpers/actions');

module.exports = base.test.extend({
  page: async ({ page }, use) => {
    page.setDefaultTimeout(TEST_TIMEOUT);
    await use(page);
  },
  /**
   * Retun all initialised common actions
   */
  actions: async ({ page }, use) => {
    page.setDefaultTimeout(TEST_TIMEOUT);
    await use(actions(page));
  },
  /**
   * Retun all initialised page objects
   */
  pageObjects: async ({ page }, use) => {
    page.setDefaultTimeout(TEST_TIMEOUT);
    await use(createPages(page));
  },
  /**
   * Sign in fixtures
   * Sign in and return page with account selector loading.
   * First line in test should be:
   * await page.waitForURL(/account-selector/);
   */

  signInZipPay: async ({ page }, use) => {
    page.setDefaultTimeout(TEST_TIMEOUT);
    await page.goto(urls.customer);
    await Authenticate(page).signInAnd2fa();
    await use(page);
  },
});
