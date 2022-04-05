/* eslint-disable no-undef */
import { ClientFunction } from 'testcafe';
import dotenv from 'dotenv-safe';
import partnerSignInPage from '../pages/partner-signin-page';
import dashboardHomePage from '../pages/dashboard-home-page';
import createInvitePage from '../pages/create-invite-page';
import mailHelper from '../../ui-e2e-tests/utils/mail-helper.js';
import * as constants from '../data/constants';
import * as data from '../../ui-e2e-tests/data/app-data';

dotenv.config();

fixture('Partners can send invite to new customers through dashboard')
  .page(process.env.ZIPPARTNER_SIGNIN)
  .beforeEach(async () => {
    await partnerSignInPage.signIn(constants.partnerDashboard.merchant1.adminEmail);
  });

test('Send invite to a new customer', async (t) => {
  let user = await mailHelper.createUser();
  const userEmail = `${user}@${data.mailinator.domain}`;
  const getLocation = ClientFunction(() => window.location.href);
  await dashboardHomePage.createInviteTile();
  await t.expect(createInvitePage.mainHeader.textContent).contains('Invite Customer');
  await t.expect(createInvitePage.branch.exists).ok();
  await createInvitePage.sendInvite(userEmail);
  await t.expect(createInvitePage.mainHeader.textContent).contains('Success');
  await t.expect(createInvitePage.inviteSent.textContent).contains('Invite was successfully sent.');
  const mailContent = await mailHelper.getEmailDetails(
    user,
    `Complete your DashboardAutomationMerchant1 application with Zip Pay`,
  );
  const mailLink = await mailHelper.getURL(mailContent, 'Create Account');
  await t.navigateTo(mailLink);
  await t
    .expect(getLocation())
    .contains(`${process.env.ZP_SIGNIN_URL}?m=${process.env.MERCHANTID}&b=${process.env.BRANCHID}`)
    .expect(createInvitePage.accountHeader.textContent)
    .contains('Own it now. Pay later.');
});
