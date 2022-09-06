import {
  Browser, BrowserContext, Page, test as base,
} from '@playwright/test';
import { TEST_TIMEOUT } from '../config';
import urls from '../constants/urls';
import Authenticate from '../steps/Authenticate';
import createPages from '../pages/index';
import actions from '../helpers/actions';

type DefaultParams = {
  page: Page,
  context?: BrowserContext,
  browser?: Browser,
};

type CustomTestType = {
  page: Page,
  actions: ReturnType<typeof actions>,
  pageObjects: ReturnType<typeof createPages>,
  signInZipPay: Page,
};

const customTest = base.extend<CustomTestType>({
  page: async ({ page }, use) => {
    page.setDefaultTimeout(TEST_TIMEOUT);
    await use(page);
  },
  /*
        Retun all initialised common actions
    */
  actions: async ({ page }: DefaultParams, use: any) => {
    page.setDefaultTimeout(TEST_TIMEOUT);
    await use(actions(page));
  },
  /*
        Retun all initialised page objects
    */
  pageObjects: async ({ page }: DefaultParams, use: any) => {
    page.setDefaultTimeout(TEST_TIMEOUT);
    await use(createPages(page));
  },
  /*
        Sign in fixtures
        Sign in and return page with account selector loading.
        First line in test should be:
            await page.waitForURL(/account-selector/);
    */
  signInZipPay: async ({ page }: DefaultParams, use: any) => {
    page.setDefaultTimeout(TEST_TIMEOUT);
    await page.goto(urls.customer);
    await Authenticate(page).signInAnd2fa();
    await use(page);
  },
});

export default customTest;
