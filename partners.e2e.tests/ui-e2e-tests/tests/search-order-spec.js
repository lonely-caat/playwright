import dotenv from 'dotenv-safe';
import dashboardHomePage from '../pages/dashboard-home-page';
import ordersPage from '../pages/orders-page';
import partnerSignInPage from '../pages/partner-signin-page';
import * as constants from '../data/constants';

dotenv.config();

fixture('Partners can search order details in the dashboard')
  .page(process.env.ZIPPARTNER_SIGNIN)
  .beforeEach(async (t) => {
    await t.resizeWindow(1480, 710);
    await partnerSignInPage.signIn(constants.partnerDashboard.merchant1.adminEmail);
    await dashboardHomePage.orderSearchTile();
  });

test('Can search order by Email, Contact No, Order No and Order status',
  async (t) => {
    await t.expect(ordersPage.ordersTitle.textContent).contains('Orders');
    await ordersPage.searchFilter('email');
    await t
      .expect(ordersPage.orderDetailsEmail.innerText)
      .eql(constants.customerEmail.zipPay.email);
    await ordersPage.searchFilter('mobileNumber');
    await t.expect(ordersPage.orderDetailsMobile.innerText).contains(constants.genericContactNo);
    await t.expect(ordersPage.searchDropDown.exists).ok();
    await ordersPage.searchFilter('orderNumber');
    await t.expect(ordersPage.orderDetailsOrderNo.innerText).eql(process.env.ORDERID);
    await ordersPage.searchFilter(constants.orderStatus.Status, constants.orderStatus.Completed);
    await t.expect(ordersPage.orderStatus.innerText).eql(constants.orderStatus.Completed);
    await ordersPage.searchFilter(constants.orderStatus.Status, constants.orderStatus.Authorised);
    await t.expect(ordersPage.orderStatus.innerText).eql(constants.orderStatus.Authorised);
    await ordersPage.searchFilter(constants.orderStatus.Status, constants.orderStatus.Refunded);
    await t.expect(ordersPage.orderStatus.innerText).eql(constants.orderStatus.Refunded);
  },
);
