import dotenv from 'dotenv-safe';
import chalk from 'chalk';
import dashboardHomePage from '../pages/dashboard-home-page';
import customersPage from '../pages/customers-page';
import partnerSignInPage from '../pages/partner-signin-page';
import * as constants from '../data/constants';

dotenv.config();

fixture('Partners can search customers in the dashboard')
  .page(process.env.ZIPPARTNER_SIGNIN)
  .beforeEach(async (t) => {
    await t.resizeWindow(1480, 710);
  });

test('Can search customer by Email, Contact No. and Application Status', async (t) => {
    await partnerSignInPage.signIn(constants.partnerDashboard.merchant1.adminEmail);
    await dashboardHomePage.customerSearchTile();
    await t.expect(customersPage.customerTitle.textContent).contains('Customers');
    await customersPage.searchFilter('email');
    await t
      .expect(customersPage.customerDetailsEmail.innerText)
      .eql(constants.customerEmail.zipCredit.email);
    await customersPage.searchFilter('mobileNumber');
    await t
      .expect(customersPage.customerDetailsMobile.innerText)
      .contains(constants.customerEmail.zipCredit.mobileNumber);
    await customersPage.searchFilter('surname');
    await t
      .expect(customersPage.customerDetailsSurname.innerText)
      .contains(constants.sendInviteCustomer.lastName);
    await customersPage.searchFilter(
      constants.applicationStatus.Status,
      constants.applicationStatus.Registered,
    );
    await t
      .expect(customersPage.customerStatus.innerText)
      .eql(constants.applicationStatus.Registered);
    await customersPage.searchFilter(
      constants.applicationStatus.Status,
      constants.applicationStatus.Approved,
    );
    await t
      .expect(customersPage.customerStatus.innerText)
      .eql(constants.applicationStatus.Approved);
    await customersPage.searchFilter(
      constants.applicationStatus.Status,
      constants.applicationStatus.Declined,
    );
    await t
      .expect(customersPage.customerStatus.innerText)
      .eql(constants.applicationStatus.Declined);
});
