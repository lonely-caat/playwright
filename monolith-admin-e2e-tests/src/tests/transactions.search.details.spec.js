const { expect } = require('@playwright/test');
const test = require('../fixtures/fixtures');
const expected = require('../constants/zipCreditData');

const { describe } = test;

describe('Transaction details tests', () => {
  test.beforeEach(async ({ page }, testInfo) => {
    console.log(` \n Running ${testInfo.title}`);
    await page.goto('/Information');
  });

  test('Transaction get details', async ({ pageObjects: { navBar, transactions } }) => {
    await navBar.selectSection('sectionTransactions');
    await transactions.switchTransactionType();
    await transactions.setSearchDate('2021-11-18', '2021-11-19');
    await transactions.searchTransactionsLegacy(expected.zipCredit.transactionNumber);
    await transactions.clickDetailsButton();
    const result = await transactions.returnDetailTablesElements();

    expect(result.slice(0, 1000)).toEqual(
      expected.transactionsFilteredResultsDetails.slice(0, 1000),
    );
  });
});
