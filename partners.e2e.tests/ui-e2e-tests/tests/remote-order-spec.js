/* eslint-disable no-undef */
import { ClientFunction, t } from 'testcafe';
import dotenv from 'dotenv-safe';
import remoteOrderPage from '../pages/remote-order-page';
import dashboardHomePage from '../pages/dashboard-home-page';
import partnerSignInPage from '../pages/partner-signin-page';
import walletPage from '../pages/wallet-page';
import * as constants from '../data/constants';
import mailHelper from '../../ui-e2e-tests/utils/mail-helper.js';
import * as data from '../../ui-e2e-tests/data/app-data';
dotenv.config();


fixture('Partners can send an email Order to existing customers').page(
  process.env.ZIPPARTNER_SIGNIN,
);
test.skip('Customers can accept email order', async (t) => {
  await partnerSignInPage.signIn(constants.partnerDashboard.merchant1.adminEmail);
  let user = await mailHelper.createUser();
  const userEmail = `${user}@${data.mailinator.domain}`;

  const getLocation = ClientFunction(() => window.location.href);
  await dashboardHomePage.remoteOrderTile();
  await t.expect(remoteOrderPage.mainHeader.textContent).contains('Send Order');
  await t.expect(remoteOrderPage.branch.exists).ok();
  await remoteOrderPage.sendOrder(userEmail);

  await t.expect(remoteOrderPage.mainHeader.textContent).contains('Success');
  await t.expect(remoteOrderPage.orderSent.textContent).contains('Order was successfully sent.');
  const mailContent = await mailHelper.getEmailDetails(
    user,
    `Complete your DashboardAutomationMerchant1 order with Zip Pay`,
  );
  const mailLink = await mailHelper.getURL(mailContent, 'Click to complete my order');

  await t.navigateTo(mailLink);
  await t
    .expect(getLocation())
    .contains(`${process.env.ZP_SIGNIN_URL}`)
    .expect(remoteOrderPage.accountHeader.textContent)
    .contains('Own it now. Pay later.');
  await walletPage.zipMoneySignIn();
  await walletPage.populateLoginDetails(userEmail);
  await t.expect(walletPage.customerOrderHeader.textContent).contains('Sign back into Zip Money');

  // users cannot no longer create account from remote order
  // await walletPage.originationOrderConfirm();
  // await walletPage.mobileVerification();
  // await t.expect(getLocation()).contains(`${process.env.ZM_SIGNIN_URL}`, {
  //   timeout: Number(process.env.LONG_ASSERTION_TIMEOUT),
  // });
});
