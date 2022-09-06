const { expect } = require('@playwright/test');
const test = require('../fixtures/fixtures');
const { isSandbox } = require('../config');
const createPages = require('../pages/index');
const Authenticate = require('../steps/Authenticate');
const urls = require('../constants/urls');

test('iFrame merchant checkout', async ({
  pageObjects: { merchantLuma },
  actions: { switchToiFrameBySelector },
  browserName,
  page,
}) => {
  test.skip(!isSandbox, 'Merchants only available on Sandbox');
  test.skip(browserName === 'webkit', 'iframe only supported on chrome');
  await page.goto(urls.merchantLuma);
  await merchantLuma.performCheckout();
  await page.pause();
  const frame = await switchToiFrameBySelector('#zipmoney-iframe');
  // Create iFrame pages
  const { confirmOrder, transactionMFA } = createPages(page, frame);
  await page.waitForTimeout(10000);
  await Authenticate(page, frame).signInNo2fa();
  await confirmOrder.selectContinue();
  await transactionMFA.verifyMobileCode();
  await merchantLuma.waitForPurchaseSuccess();
  expect(await merchantLuma.getPageTitle()).toBe('Thank you for your purchase!');
});
