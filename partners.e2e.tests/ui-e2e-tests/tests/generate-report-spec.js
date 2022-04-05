/* eslint-disable no-undef */
import dotenv from 'dotenv-safe';
import partnerSignInPage from '../pages/partner-signin-page';
import dashboardHomePage from '../pages/dashboard-home-page';
import dashboardAdminPage from '../pages/dashboard-admin-page';
import * as constants from '../data/constants';
import disbursementsPage from '../pages/disbursements-page';

dotenv.config();

fixture('Partners can generate disbursement report in dashboard')
  .page(process.env.ZIPPARTNER_SIGNIN)
  .beforeEach(async (t) => {
    await t.maximizeWindow();
    await partnerSignInPage.signIn(constants.partnerDashboard.merchant1.adminEmail);
  });
test('Generate a disbursement report for the default date range', async (t) => {
  await dashboardHomePage.adminTile();
  await dashboardAdminPage.reportTile();
  await t
    .expect(disbursementsPage.generateReportHeader.textContent)
    .contains('Generate Report')
    .expect(disbursementsPage.downloadText.textContent)
    .contains('Click here to download the report.');
  await disbursementsPage.createReport();
  await t.expect(disbursementsPage.downloadLink.exists).ok();
});

test('Check available Report recurrence options', async (t) => {
  await dashboardHomePage.adminTile();
  await dashboardAdminPage.reportTile();
  await t
    .expect(disbursementsPage.dailyRecurrenceCheckBox.textContent)
    .contains('Daily')
    .expect(disbursementsPage.weeklyRecurrenceCheckBox.textContent)
    .contains('Weekly')
    .expect(disbursementsPage.monthlyRecurrenceCheckBox.textContent)
    .contains('Monthly');
});
