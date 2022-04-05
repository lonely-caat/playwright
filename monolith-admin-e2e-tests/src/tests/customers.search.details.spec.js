const { expect } = require('@playwright/test');
const test = require('../fixtures/fixtures');
const expected = require('../constants/zipCreditData');

const { describe } = test;

describe('Customers search tests', () => {
  test('Customer search by account number with default filter selected, check details', async ({
    pageObjects: { navBar, customers },
    page,
  }) => {
    await page.goto('/Information');
    await navBar.selectSection('sectionCustomers');
    await customers.searchCustomers('8367');
    await customers.clickDetailsButton();
    await page.waitForTimeout(2000)
    let result = await customers.returnDetailTablesElements();
    const expectedResult = expected.customersResultsDetails.split(',').filter(item => !item.startsWith('Next Scheduled Date'));
    result = result.split(',').filter(item => !item.startsWith('Next Scheduled Date'));
    expect(result).toEqual(expectedResult);
  });
});
