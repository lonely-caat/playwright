import dotenv from 'dotenv-safe';
import passwordPage from '../pages/password-set-page';
import partnerSignInPage from '../pages/partner-signin-page';
import dashboardHomePage from '../pages/dashboard-home-page';
import userManagementPage from '../pages/user-management-page';
import dashboardAdminPage from '../pages/dashboard-admin-page';
import mailHelper from '../../ui-e2e-tests/utils/mail-helper.js';
import * as constants from '../data/constants';
import * as data from '../data/app-data.js';

dotenv.config();

fixture('Set password from the email link').page(process.env.ZIPPARTNER_SIGNIN);
test('Dashboard users can set a password from the email received.', async (t) => {
  await t.resizeWindow(1480, 710);
  await partnerSignInPage.signIn(constants.partnerDashboard.merchant1.adminEmail);
  await dashboardHomePage.adminTile();
  await dashboardAdminPage.userManagementTile();
  let user = await mailHelper.createUser();
  const userEmail = `${user}@${data.mailinator.domain}`;
  await userManagementPage.addUser(userEmail, 'manager');
  await t
    .expect(userManagementPage.toastMessage.textContent)
    .contains(data.userManagement.addUser, { timeout: Number(process.env.LONG_SELECTOR_TIMEOUT) });
  await dashboardHomePage.dashboardLogout();
  console.log(user, '111')
  const mailContent = await mailHelper.getEmailDetails(user, `Set your Zip dashboard password`);
  const mailLink = await mailHelper.getURL(mailContent, 'Set My Password');
  await t.navigateTo(mailLink);
  await t.expect(passwordPage.mainHeader.textContent).contains('Set password for your account');
  await passwordPage.setPassword(constants.partnerPassword);
  await t
    .expect(passwordPage.passwordConfirmation.textContent)
    .contains(`password has been set successfully`);
  await partnerSignInPage.signIn(userEmail);
  await t
    .expect(dashboardHomePage.merchantName.textContent)
    .contains(constants.partnerDashboard.merchant1.merchantName);
});
