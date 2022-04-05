const { expect } = require('@playwright/test');
const test = require('../fixtures/fixtures');
const expected = require('../constants/zipCreditData');

const { describe } = test;

describe('Transaction search tests', () => {
  test.beforeEach(async ({ page }, testInfo) => {
    console.log(` \n Running ${testInfo.title}`);
    await page.goto('/Information');
  });

  test('Transaction search by Zip Order ID', async ({ pageObjects: { navBar, transactions } }) => {
    await navBar.selectSection('sectionTransactions');
    await transactions.searchTransactions(expected.zipCredit.transactionNumber);
    const result = await transactions.returnTableElements();
    expect(result.slice(0, 10)).toEqual(expected.transactionsResults.slice(0, 10));
  });

  test('Transaction search by old search and time limit', async ({
    pageObjects: { navBar, transactions },
  }) => {
    await navBar.selectSection('sectionTransactions');
    await transactions.switchTransactionType();
    await transactions.setSearchDate('2021-11-18', '2021-11-19');
    await transactions.searchTransactionsLegacy(expected.zipCredit.transactionNumber);
    const result = await transactions.returnTableElements();
    expect(result.slice(0, 1000)).toEqual(expected.transactionsFilteredResults.slice(0, 1000));
  });
});
