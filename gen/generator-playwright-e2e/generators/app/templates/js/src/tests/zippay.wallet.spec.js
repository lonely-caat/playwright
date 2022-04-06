const { expect } = require('@playwright/test');
const { zipProduct } = require('../constants/picker');
const test = require('../fixtures/fixtures');

const { describe, beforeAll, afterAll } = test;

/**
 * Incase you are writing a goup of tests in same suite !!!
 */
describe('Wallet Tests', () => {
  beforeAll(() => {
    console.log('This is beforeAll block for ZipPay Wallet tests...');
  });
  afterAll(async () => {
    console.log('This is  afterAll block for ZipPay Wallet tests...');
  });
  test('Account selector zip pay account directs to web wallet', async ({
    pageObjects: { accountSelector, verifyEmail, webWallet },
    signInZipPay: page,
  }) => {
    await page.waitForURL(/account-selector/);
    await accountSelector.selectProduct(zipProduct.ZipPay);
    await verifyEmail.skipVerifyEmail();
    expect((await webWallet.getHeaderText()) === 'Welcome testFirst');
  });
});
