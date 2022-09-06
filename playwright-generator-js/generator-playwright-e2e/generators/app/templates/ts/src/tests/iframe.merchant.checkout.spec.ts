import { expect } from '@playwright/test';
import test from '../fixtures/fixtures';
import { isSandbox } from '../config';
import createPages from '../pages/index';
import Authenticate from '../steps/Authenticate';
import urls from '../constants/urls';

test('iFrame merchant checkout via sign in', async ({
  pageObjects: {
    merchantLuma,
  },
  actions: {
    switchToiFrameBySelector,
  },
  browserName,
  page,
}) => {
  test.skip(!isSandbox, 'Merchants only available on Sandbox');
  test.skip(browserName === 'webkit', 'Merchants only available on Sandbox');
  await page.goto(urls.merchantLuma);
  await merchantLuma.performCheckout();
  const frame = await switchToiFrameBySelector('#zipmoney-iframe');

  // Create iFrame pages
  const {
    confirmOrder,
    transactionMFA,
  } = createPages(page, frame);
  await page.waitForTimeout(10000);
  await Authenticate(page, frame).signInNo2fa();
  await confirmOrder.selectContinue();
  await transactionMFA.verifyMobileCode();
  await merchantLuma.waitForPurchaseSuccess();
  expect(await merchantLuma.getPageTitle()).toBe('Thank you for your purchase!');
});
