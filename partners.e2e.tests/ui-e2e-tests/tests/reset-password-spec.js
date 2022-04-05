import dotenv from 'dotenv-safe';
import partnerSignInPage from '../pages/partner-signin-page';
import mailHelper from '../../ui-e2e-tests/utils/mail-helper.js';
import * as constants from '../data/constants';
import dashboardHomePage from '../pages/dashboard-home-page';

dotenv.config();

fixture('Reset password from the email link').page(process.env.ZIPPARTNER_SIGNIN)
  .beforeEach(async (t) => {
    await t.maximizeWindow()
    t.ctx.email = constants.partnerDashboard.user.userName.split('@')[0]
  });

test.skip('Dashboard users can reset password from the email received.', async (t) => {
  await t.click(partnerSignInPage.forgotPassword)
  await t.typeText(partnerSignInPage.forgotEmail, constants.partnerDashboard.user.userName)
  await t.click(partnerSignInPage.sendLink)

  const mailContent = await mailHelper.getEmailDetails(t.ctx.email, `Here's a link to reset your password`);
  const mailLink = await mailHelper.getURLResetPassword(mailContent);

  await t
    .navigateTo(mailLink);
  await t.expect(partnerSignInPage.resetPasswordHeader.textContent)
    .contains('Reset password');

  await partnerSignInPage.setPassword(constants.partnerPassword);
  await partnerSignInPage.signIn(constants.partnerDashboard.user.userName);
  await t
    .expect(dashboardHomePage.merchantName.textContent)
    .contains(constants.partnerDashboard.merchant1.merchantName);
});

test.skip('Dashboard users can reset password only once.', async (t) => {
  await t.click(partnerSignInPage.forgotPassword)
  await t.typeText(partnerSignInPage.forgotEmail, constants.partnerDashboard.user.userName)
  await t.click(partnerSignInPage.sendLink)

  const mailContent = await mailHelper.getEmailDetails(t.ctx.email, `Here's a link to reset your password`);
  const mailLink = await mailHelper.getURLResetPassword(mailContent);

  await t
    .navigateTo(mailLink);
  await t.expect(partnerSignInPage.resetPasswordHeader.textContent)
    .contains('Reset password');

  await partnerSignInPage.setPassword(constants.partnerPassword);
  await partnerSignInPage.signIn(constants.partnerDashboard.user.userName);
  await t
    .expect(dashboardHomePage.merchantName.textContent)
    .contains(constants.partnerDashboard.merchant1.merchantName);

  await dashboardHomePage.logOut()
  await t
    .navigateTo(mailLink)

  await t.expect(partnerSignInPage.mainHeader.textContent)
    .contains('Sign in as Zip partner');
});