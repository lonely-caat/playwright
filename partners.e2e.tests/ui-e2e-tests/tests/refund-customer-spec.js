import dotenv from 'dotenv-safe';
import dashboardHomePage from '../pages/dashboard-home-page';
import partnerSignInPage from '../pages/partner-signin-page';
import * as constants from '../data/constants';
import ordersPage from '../pages/orders-page';
import randomstring from 'randomstring';

dotenv.config();

fixture(
  'Partners can refund payments for customers' +
    ':heavy_exclamation_mark: https://zipmoney.atlassian.net/browse/BP-296',
)
  .page(process.env.ZIPPARTNER_SIGNIN)
  .beforeEach(async (t) => {
    await t.maximizeWindow();
    await partnerSignInPage.signIn(constants.partnerDashboard.merchant1.adminEmail);
    await dashboardHomePage.refundTile();
  });

test('Can Refund different amounts 0.20$', async (t) => {
  let short_description = randomstring.generate(250);

  await t.expect(ordersPage.ordersTitle.textContent).contains('Orders');
  await ordersPage.searchFilter('status', 'Completed');
  await t.expect(ordersPage.orderStatus.innerText).eql('Completed');

  await ordersPage.actionsDropDown('refund');
  await t.expect(ordersPage.refundModalTitle.exists).ok();
  await ordersPage.branchDropDown('dashboard');
  await t.typeText(ordersPage.refundAmount, '0.20', { replace: true });
  await t.typeText(ordersPage.refundComment, short_description);
  await t.click(ordersPage.refundSubmit);

  await t.click(ordersPage.refundBack);
  await t.click(ordersPage.refundSubmit);
  await t.click(ordersPage.refundConfirm);
});

test('Can Refund different amounts 1.00$', async (t) => {
  await t.expect(ordersPage.ordersTitle.textContent).contains('Orders');
  await ordersPage.searchFilter('status', 'Completed');
  await t.expect(ordersPage.orderStatus.innerText).eql('Completed');

  await ordersPage.actionsDropDown('refund');
  await t.expect(ordersPage.refundModalTitle.exists).ok();
  await ordersPage.branchDropDown('sydney');
  await t.typeText(ordersPage.refundAmount, '1.00', { replace: true });
  await t.click(ordersPage.refundSubmit);

  await t.click(ordersPage.refundBack);
  await t.click(ordersPage.refundSubmit);
  await t.click(ordersPage.refundConfirm);
});

test('Can Change Order reference', async (t) => {
  await t.expect(ordersPage.ordersTitle.textContent).contains('Orders');
  await ordersPage.searchFilter('status', 'Completed');
  await t.expect(ordersPage.orderStatus.innerText).eql('Completed');

  await t.expect(ordersPage.orderReferenceTitle.innerText).eql('Order Reference');
  await t.click(ordersPage.orderReferenceEdit);
  await t.expect(ordersPage.header2.innerText).eql('Edit order reference');

  await t.typeText(ordersPage.orderRefenceInput, 'meow', { replace: true });
  await t.click(ordersPage.orderReferenceApply);

  await ordersPage.searchFilter('reference', constants.partnerReference);
  await t.expect(ordersPage.orderReferenceRow.innerText).eql(constants.partnerReference);
});

test('Can view takings', async (t) => {
  await t.expect(ordersPage.takings.textContent).eql('View Takings');
  await t.click(ordersPage.takings);

  await t.expect(ordersPage.takingsToday.textContent).eql('Today');
  await t.expect(ordersPage.takingsWeek.textContent).eql('Last 7 days');
  await t.expect(ordersPage.takingsMonth.textContent).eql('Last 30 days');
});
