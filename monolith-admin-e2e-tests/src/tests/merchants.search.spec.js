const { expect } = require('@playwright/test');
const test = require('../fixtures/fixtures');
const expected = require('../constants/zipCreditData');

const { describe } = test;

describe('Merchants search tests', () => {
  test.beforeEach(async ({ page }, testInfo) => {
    console.log(` \n Running ${testInfo.title}`);
    await page.goto('/Information');
  });

  test('Merchants search by merchant name', async ({ pageObjects: { navBar, merchants } }) => {
    await navBar.selectSection('sectionMerchants');
    await merchants.searchMerchants('AU');
    const result = await merchants.returnTableElements();
    expect(result.slice(0, 7)).toEqual(expected.merchantsResults.slice(0, 7));
  });

  test('Merchants search by partial, case insensitive merchant name where country is New Zealand', async ({
    pageObjects: { navBar, merchants },
  }) => {
    await navBar.selectSection('sectionMerchants');
    await merchants.searchMerchants('NZ', 'zippay');
    const result = await merchants.returnTableElements();
    expect(result.slice(0, 7)).toEqual(expected.merchantsResultsNZ.slice(0, 7));
  });
});
