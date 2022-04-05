import dotenv from 'dotenv-safe';
import dashboardHomePage from '../pages/dashboard-home-page';
import RepaymentCalculatorPage from '../pages/repayment-calculator-page';
import partnerSignInPage from '../pages/partner-signin-page';
import * as constants from '../data/constants';

dotenv.config();

fixture('Partners can calculate repayments for customers')
  .page(process.env.ZIPPARTNER_SIGNIN)
  .beforeEach(async (t) => {
    await t.maximizeWindow();
    await partnerSignInPage.signIn(constants.partnerDashboard.merchant1.adminEmail);
    await dashboardHomePage.repaymentCalculator();
  });

test('Can check different repayment options, 100$', async (t) => {
  await t.expect(RepaymentCalculatorPage.repaymentsTitle.textContent).eql(' Repayment Calculator ');
  await t.typeText(RepaymentCalculatorPage.priceInput, '100');
  await t.expect(RepaymentCalculatorPage.sendOrderBtn.exists).ok();
  await t.expect(RepaymentCalculatorPage.monthlyRepayment.textContent).eql('$40.00');
  await t.expect(RepaymentCalculatorPage.establishmentFee.textContent).eql('$0.00');
  await t.expect(RepaymentCalculatorPage.monthlyAccountFee.textContent).eql('$7.95');
  await t.expect(RepaymentCalculatorPage.returnBtn.exists).ok();
});

test('Can check different repayment options, 10000$', async (t) => {
  await t.typeText(RepaymentCalculatorPage.priceInput, '10000', { replace: true });
  await t.click(RepaymentCalculatorPage.repaymentsTitle);
  await t.expect(RepaymentCalculatorPage.sendOrderBtn.exists).notOk();
  await t.expect(RepaymentCalculatorPage.monthlyRepayment.exists).notOk();
  await t.expect(RepaymentCalculatorPage.returnBtn.exists).ok();
  await t.expect(RepaymentCalculatorPage.maxAmountErr.exists).ok();
});
