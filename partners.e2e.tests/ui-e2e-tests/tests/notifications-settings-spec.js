import dashboardAdminPage from '../pages/dashboard-admin-page';
import dotenv from 'dotenv-safe';
import dashboardHomePage from '../pages/dashboard-home-page';
import NotificationSettingsPage from '../pages/notifications-settings-page';
import partnerSignInPage from '../pages/partner-signin-page';
import * as constants from '../data/constants';

dotenv.config();

fixture('Partner Admin can setup different Notification Settings for Orders and Customers')
  .page(process.env.ZIPPARTNER_SIGNIN)
  .beforeEach(async (t) => {
    await t.maximizeWindow();
    await partnerSignInPage.signIn(constants.partnerDashboard.merchant1.adminEmail);
    await dashboardHomePage.adminTile();
    await dashboardAdminPage.notificationsPageTile();
  });

test('Partner Admin can save edited notification settings', async (t) => {
  /*
  Steps:
  Check all notifications check boxes
  Save selection and refresh the page
  Verify Selection is saved for the user
  Revert changes and Save
   */
  await t
    .expect(NotificationSettingsPage.notificationSettingsTitle.textContent)
    .eql('Notification Settings');

  for (let checkbox of NotificationSettingsPage.allCheckboxes) {
    await t.click(checkbox);
  }
  await t.click(NotificationSettingsPage.saveBtn);
  await t.wait(3000);
  await t.eval(() => location.reload(true));

  for (let checkbox of NotificationSettingsPage.allCheckboxes) {
    await t.expect(checkbox.getAttribute('aria-checked')).ok();
  }

  for (let checkbox of NotificationSettingsPage.allCheckboxes) {
    await t.click(checkbox);
  }
  await t.click(NotificationSettingsPage.saveBtn);
});
