const { expect } = require('@playwright/test');
const test = require('../fixtures/fixtures');
const expected = require('../constants/zipCreditData');

const { describe } = test;

describe('Customers search tests', () => {
  test.beforeEach(async ({ page }, testInfo) => {
    console.log(` \n Running ${testInfo.title}`);
    await page.goto('/Information');
  });

  test('Customer search by account number but with default filter selected', async ({
    pageObjects: { navBar, customers },
  }) => {
    await navBar.selectSection('sectionCustomers');
    await customers.searchCustomers('8367');
    const result = await customers.returnTableElements();
    expect(result.slice(0, 10)).toEqual(expected.customersResults.slice(0, 10));
  });

  test('Customer search by account number', async ({ pageObjects: { navBar, customers } }) => {
    await navBar.selectSection('sectionCustomers');
    await customers.searchCustomers('8367', 'optionAccountNumber');
    const result = await customers.returnTableElements();
    expect(result.slice(0, 10)).toEqual(expected.customersResults.slice(0, 10));
  });

  test('Customer search by last name', async ({ pageObjects: { navBar, customers } }) => {
    await navBar.selectSection('sectionCustomers');
    await customers.searchCustomers(expected.zipCredit.LastName, 'optionLastName');
    const result = await customers.returnTableElements();
    expect(result.slice(0, 10)).toEqual(expected.customersResults.slice(0, 10));
  });

  test('Customer search by email address', async ({ pageObjects: { navBar, customers } }) => {
    await navBar.selectSection('sectionCustomers');
    await customers.searchCustomers(expected.zipCredit.EmailAddress, 'optionEmailAddress');
    const result = await customers.returnTableElements();
    expect(result.slice(0, 10)).toEqual(expected.customersResults.slice(0, 10));
  });

  test('Customer search by mobile phone number', async ({
    pageObjects: { navBar, customers },
    page,
  }) => {
    await navBar.selectSection('sectionCustomers');
    await customers.searchCustomers(expected.zipCredit.MobileNumber, 'optionMobileNumber');
    const result = await customers.returnTableElements();
    expect(result).toEqual(expected.customersResultsMobile);
  });

  test('Customer search by consumer id', async ({ pageObjects: { navBar, customers } }) => {
    await navBar.selectSection('sectionCustomers');
    await customers.searchCustomers(expected.zipCredit.ConsumerId, 'optionConsumerId');
    const result = await customers.returnTableElements();
    expect(result.slice(0, 10)).toEqual(expected.customersResults.slice(0, 10));
  });
});
