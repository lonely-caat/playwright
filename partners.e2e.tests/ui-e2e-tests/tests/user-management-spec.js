import dotenv from 'dotenv-safe';
import dashboardHomePage from '../pages/dashboard-home-page';
import partnerSignInPage from '../pages/partner-signin-page';
import * as constants from '../data/constants';
import dashboardAdminPage from '../pages/dashboard-admin-page';
import userManagementPage from '../pages/user-management-page';
import * as data from '../data/app-data.js';
import helper from '../utils/helper';
import { t } from 'testcafe';
dotenv.config();

fixture('Partner Admin can manage users in the dashboard')
  .page(process.env.ZIPPARTNER_SIGNIN)
  .beforeEach(async (t) => {
    await t.maximizeWindow();
    await partnerSignInPage.signIn(constants.partnerDashboard.merchant1.adminEmail);
    await dashboardHomePage.adminTile();
    await dashboardAdminPage.userManagementTile();
  });

test('Admin dashboard user can Add, Edit, Delete manager role', async (t) => {
  const managerEmail = helper.createUserEmail('manager');
  await t.expect(userManagementPage.usersTitle.textContent).contains('Users');
  await t.expect(userManagementPage.searchUser.visible).ok();
  await userManagementPage.addUser(managerEmail, 'manager');
  await t
    .expect(userManagementPage.toastMessage.textContent)
    .contains(data.userManagement.addUser, { timeout: Number(process.env.LONG_SELECTOR_TIMEOUT) });
  await userManagementPage.searchUserDetails(managerEmail);
  await t.expect(userManagementPage.userDetailsEmailId.innerText).eql(managerEmail);
  await userManagementPage.editUser();
  await t
    .expect(userManagementPage.toastMessage.textContent)
    .contains(data.userManagement.editUser, {
      timeout: Number(process.env.LONG_SELECTOR_TIMEOUT),
    });
  await userManagementPage.deleteUser();
  await t
    .expect(userManagementPage.toastMessage.textContent)
    .contains(data.userManagement.deleteUser, {
      timeout: Number(process.env.LONG_SELECTOR_TIMEOUT),
    });
});

test('Admin dashboard user can Add, Edit, Delete storeuser role', async (t) => {
  const storeUserEmail = helper.createUserEmail('storeuser');
  await userManagementPage.addUser(storeUserEmail, 'storeuser');
  await t
    .expect(userManagementPage.toastMessage.textContent)
    .contains(data.userManagement.addUser, { timeout: Number(process.env.LONG_SELECTOR_TIMEOUT) });
  await userManagementPage.searchUserDetails(storeUserEmail);
  await t.expect(userManagementPage.userDetailsEmailId.innerText).eql(storeUserEmail);
  await userManagementPage.editUser();
  await t
    .expect(userManagementPage.toastMessage.textContent)
    .contains(data.userManagement.editUser, {
      timeout: Number(process.env.LONG_SELECTOR_TIMEOUT),
    });
  await userManagementPage.deleteUser();
  await t
    .expect(userManagementPage.toastMessage.textContent)
    .contains(data.userManagement.deleteUser, {
      timeout: Number(process.env.LONG_SELECTOR_TIMEOUT),
    });
});

fixture('Company Admin can manage users in the dashboard')
  .page(process.env.ZIPPARTNER_SIGNIN)
  .beforeEach(async (t) => {
    await partnerSignInPage.signIn(constants.partnerDashboard.company.adminEmail);
    await dashboardHomePage.adminTile();
    await dashboardAdminPage.userManagementTile();
  });

test(
  'Company Admin dashboard user can Add, Edit, Delete webmanager role',
  async (t) => {
    const companyWebmgrEmail = helper.createUserEmail('webmanager');
    await userManagementPage.addUser(companyWebmgrEmail, 'web_manager');
    await t
      .expect(userManagementPage.toastMessage.textContent)
      .contains(data.userManagement.addUser, {
        timeout: Number(process.env.LONG_SELECTOR_TIMEOUT),
      });
    await userManagementPage.searchUserDetails(companyWebmgrEmail);
    await t.expect(userManagementPage.userDetailsEmailId.innerText).eql(companyWebmgrEmail);
    await userManagementPage.editUser();
    await t
      .expect(userManagementPage.toastMessage.textContent)
      .contains(data.userManagement.editUser, {
        timeout: Number(process.env.LONG_SELECTOR_TIMEOUT),
      });
    await userManagementPage.deleteUser();
    await t.expect(userManagementPage.userNotFound.innerText).eql('No user(s) found.');
  },
);

test('Company Admin dashboard user can Add, Edit, Delete Reporting User role', async (t) => {
  const companyReportingUserEmail = helper.createUserEmail('reporting_user');
  await userManagementPage.addUser(companyReportingUserEmail, 'reporting_user');
  await t
    .expect(userManagementPage.toastMessage.textContent)
    .contains(data.userManagement.addUser, { timeout: Number(process.env.LONG_SELECTOR_TIMEOUT) });
  await userManagementPage.searchUserDetails(companyReportingUserEmail);
  await t.expect(userManagementPage.userDetailsEmailId.innerText).eql(companyReportingUserEmail);
  await userManagementPage.editUser();
  await t
    .expect(userManagementPage.toastMessage.textContent)
    .contains(data.userManagement.editUser, {
      timeout: Number(process.env.LONG_SELECTOR_TIMEOUT),
    });
  await userManagementPage.deleteUser();
  await t.expect(userManagementPage.userNotFound.innerText).eql('No user(s) found.');
});

test('Company Admin dashboard user can Add, Edit, Delete Manager role', async (t) => {
  const companyManagerEmail = helper.createUserEmail('manager');
  await userManagementPage.addUser(companyManagerEmail, 'manager');
  await t
    .expect(userManagementPage.toastMessage.textContent)
    .contains(data.userManagement.addUser, { timeout: Number(process.env.LONG_SELECTOR_TIMEOUT) });
  await userManagementPage.searchUserDetails(companyManagerEmail);
  await t.expect(userManagementPage.userDetailsEmailId.innerText).eql(companyManagerEmail);
  await userManagementPage.editUser();
  await t
    .expect(userManagementPage.toastMessage.textContent)
    .contains(data.userManagement.editUser, {
      timeout: Number(process.env.LONG_SELECTOR_TIMEOUT),
    });
  await userManagementPage.deleteUser();
  await t.expect(userManagementPage.userNotFound.innerText).eql('No user(s) found.');
});

test('Company Admin dashboard user can Add, Edit, Delete Store User role', async (t) => {
  const companyStoreUserEmail = helper.createUserEmail('store_user');
  await userManagementPage.addUser(companyStoreUserEmail, 'store_user');
  await t
    .expect(userManagementPage.toastMessage.textContent)
    .contains(data.userManagement.addUser, { timeout: Number(process.env.LONG_SELECTOR_TIMEOUT) });
  await userManagementPage.searchUserDetails(companyStoreUserEmail);
  await t.expect(userManagementPage.userDetailsEmailId.innerText).eql(companyStoreUserEmail);
  await userManagementPage.editUser();
  await t
    .expect(userManagementPage.toastMessage.textContent)
    .contains(data.userManagement.editUser, {
      timeout: Number(process.env.LONG_SELECTOR_TIMEOUT),
    });
  await userManagementPage.deleteUser();
  await t.expect(userManagementPage.userNotFound.innerText).eql('No user(s) found.');
});

test.skip('Company Admin dashboard user can Add, Edit, Delete Api Admin user role' +
    ':exclamation: https://zip-co.atlassian.net/browse/BP-1523 :exclamation:',
  async (t) => {
    const companyApiAdminEmail = helper.createUserEmail('api_admin');
    await userManagementPage.addUser(companyApiAdminEmail, 'api_admin');
    await t
      .expect(userManagementPage.toastMessage.textContent)
      .contains(data.userManagement.addUser, {
        timeout: Number(process.env.LONG_SELECTOR_TIMEOUT),
      });
    await userManagementPage.searchUserDetails(companyApiAdminEmail);
    await t.expect(userManagementPage.userDetailsEmailId.innerText).eql(companyApiAdminEmail);
    await userManagementPage.editUser();
    await t
      .expect(userManagementPage.toastMessage.textContent)
      .contains(data.userManagement.editUser, {
        timeout: Number(process.env.LONG_SELECTOR_TIMEOUT),
      });
    await userManagementPage.deleteUser();
    await t.expect(userManagementPage.userNotFound.innerText).eql('No user(s) found.');
  },
);

test('Company Admin dashboard user can Add, Edit, Delete Marketing User role', async (t) => {
  const companyMarketingUserEmail = helper.createUserEmail('marketing_user');
  await userManagementPage.addUser(companyMarketingUserEmail, 'marketing_user');
  await t
    .expect(userManagementPage.toastMessage.textContent)
    .contains(data.userManagement.addUser, { timeout: Number(process.env.LONG_SELECTOR_TIMEOUT) });
  await userManagementPage.searchUserDetails(companyMarketingUserEmail);
  await t.expect(userManagementPage.userDetailsEmailId.innerText).eql(companyMarketingUserEmail);
  await userManagementPage.editUser();
  await t
    .expect(userManagementPage.toastMessage.textContent)
    .contains(data.userManagement.editUser, {
      timeout: Number(process.env.LONG_SELECTOR_TIMEOUT),
    });
  await userManagementPage.deleteUser();
  await t.expect(userManagementPage.userNotFound.innerText).eql('No user(s) found.');
});
