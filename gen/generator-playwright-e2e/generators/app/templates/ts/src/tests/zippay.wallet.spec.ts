import { expect } from '@playwright/test';
import { ZipProduct } from '../constants/picker';
import test from '../fixtures/fixtures';

test('Account selector zip pay account directs to web wallet', async ({
  pageObjects: {
    accountSelector,
    verifyEmail,
    webWallet,
  },
  signInZipPay: page,
}) => {
  await page.waitForURL(/account-selector/);
  await accountSelector.selectProduct(ZipProduct.ZipPay);
  await verifyEmail.skipVerifyEmail();
  expect(await webWallet.getHeaderText() === 'Welcome testFirst');
});
