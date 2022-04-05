import dotenv from 'dotenv-safe';
import createOrderPage from '../pages/create-order-page';
import completeOrderPage from '../pages/complete-order-page';
import dashboardHomePage from '../pages/dashboard-home-page';
import partnerSignInPage from '../pages/partner-signin-page';
import * as constants from '../data/constants';

dotenv.config();

fixture('Partners can create order through dashboard').page(process.env.ZIPPARTNER_SIGNIN);
test.skip('Verify direct order capture', async (t) => {
  await partnerSignInPage.signIn(constants.partnerDashboard.merchant1.adminEmail);
  await t
    .expect(dashboardHomePage.merchantName.textContent)
    .contains(constants.partnerDashboard.merchant1.merchantName);
  await dashboardHomePage.createOrderTile();
  await t.expect(createOrderPage.mainHeader.textContent).contains('Create order');
  await t.expect(createOrderPage.branch.exists).ok();
  await createOrderPage.createOrder(constants.customerEmail.zipCredit.email);
  await t.expect(createOrderPage.succesCheckMark.exists).ok();
  await createOrderPage.submitOrder();
  await t.expect(completeOrderPage.mainHeader.textContent).contains('All done!');
  const receiptId = await createOrderPage.getUrlReceiptId('completed');
  await t.expect(completeOrderPage.receiptNumber.innerText).contains(receiptId);
  await t
    .expect(completeOrderPage.orderComplete.textContent)
    .contains('Your order has been processed!');
});
