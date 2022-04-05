const { expect } = require('@playwright/test');
const test = require('../fixtures/fixtures');

const { describe } = test;
const createTransaction = require('../utils/createUser');
const generateData = require('../helpers/generateData');

const emailZipPay = generateData.createRandomEmail();
const emailZipMoney = generateData.createRandomEmail();

describe('Transaction details tests', () => {
  test.beforeEach(async ({ page }, testInfo) => {
    console.log(` \n Running ${testInfo.title}`);

    // sad sad day for performance

    await page.goto('/Information');
  });

  test('Create ZipPay transaction and verify expected result', async ({
    pageObjects: { navBar, transactions },
    page,
  }) => {
    await createTransaction.createUser(
      'zipPay',
      'sandbox',
      'AutomationTest',
      true,
      100,
      emailZipPay,
    );
    await page.waitForTimeout(20000);

    await navBar.selectSection('sectionTransactions');

    await transactions.searchTransactions(emailZipPay, 'optionEmailAddress');
    await transactions.clickDetailsButton();

    expect(await transactions.returnTotalFeeAmount()).toEqual('$1.67');
    expect(await transactions.returnCapturedAmount()).toEqual('$100.00');
    expect(await transactions.returnAuthorizedAmount()).toEqual('$0.00');
    expect(await transactions.returnDisbursedAmount()).toEqual('$0.00');
  });

  test('Create ZipMoney transaction and verify expected result', async ({
    pageObjects: { navBar, transactions },
    page,
  }) => {
    await createTransaction.createUser(
      'zipCredit',
      'sandbox',
      'AutomationTest',
      true,
      500,
      emailZipMoney,
    );
    await page.waitForTimeout(20000);

    await navBar.selectSection('sectionTransactions');

    await transactions.searchTransactions(emailZipPay, 'optionEmailAddress');
    await transactions.clickDetailsButton();

    expect(await transactions.returnTotalFeeAmount()).toEqual('$1.67');
    expect(await transactions.returnCapturedAmount()).toEqual('$100.00');
    expect(await transactions.returnAuthorizedAmount()).toEqual('$0.00');
    expect(await transactions.returnDisbursedAmount()).toEqual('$0.00');
  });
});
