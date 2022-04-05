const { expect } = require('@playwright/test');
const test = require('../fixtures/fixtures');
const expected = require('../constants/zipCreditData');

const { describe } = test;

describe('Merchants Details', () => {
  test.beforeEach(async ({ page }, testInfo) => {
    console.log(` \n Running ${testInfo.title}`);
    await page.goto('/Information');
  });
  test('Merchants detailed info', async ({ pageObjects: { navBar, merchants } }) => {
    await navBar.selectSection('sectionMerchants');
    await merchants.searchMerchants('AU', 'Bas Loves Cats');
    await merchants.clickDetailsButton();
    const results = await merchants.returnMerchantsDetailTablesElements();
    expect(results).toContain(expected.merchantsResultsDetails);
  });

  test('Companies detailed info', async ({ pageObjects: { navBar, merchants } }) => {
    await navBar.selectSection('sectionMerchants');
    await merchants.selectMerchantsSubTabs('companies');
    await merchants.searchCompanies('AU', 'AutomationCompany-donotuse');
    await merchants.clickDetailsButton();
    const results = await merchants.returnMerchantsDetailTablesElements();
    expect(results).toContain(expected.companiesResultsDetails);
  });
});
