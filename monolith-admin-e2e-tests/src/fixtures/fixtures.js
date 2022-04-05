const base = require('@playwright/test');
const { TEST_TIMEOUT } = require('../config');
const urls = require('../constants/urls');
const createPages = require('../pages/index');
const actions = require('../helpers/actions');
const browser = require('@playwright/test');


module.exports = base.test.extend({
  page: async ({ page }, use) => {
    page.setDefaultTimeout(TEST_TIMEOUT);
    await use(page);
  },

  // context: async ({ context }, use) => {
  //   page.setDefaultTimeout(TEST_TIMEOUT);
  //   await use(page);
  // },
  /**
   * Return all initialised common actions
   */
  actions: async ({ page }, use) => {
    page.setDefaultTimeout(TEST_TIMEOUT);
    await use(actions(page));
  },
  /**
   * Return all initialised page objects
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

  signInAdmin: async ({ page }, use) => {
    page.setDefaultTimeout(TEST_TIMEOUT);
    await page.goto(urls.oneLogin);
  }

});
