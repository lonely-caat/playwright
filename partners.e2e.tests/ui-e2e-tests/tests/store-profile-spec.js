import dashboardAdminPage from '../pages/dashboard-admin-page';
import dotenv from 'dotenv-safe';
import dashboardHomePage from '../pages/dashboard-home-page';
import StoreProfilePage from '../pages/store-profile-page';
import partnerSignInPage from '../pages/partner-signin-page';
import * as constants from '../data/constants';
import randomstring from 'randomstring';

dotenv.config();


fixture('Partner Admin can setup branding from the store profile page' +
  ':heavy_exclamation_mark: https://zipmoney.atlassian.net/browse/BP-289')
  .page(process.env.ZIPPARTNER_SIGNIN)
  .beforeEach(async (t) => {
    await t.maximizeWindow();
    await partnerSignInPage.signIn(constants.partnerDashboard.merchant2.adminEmail);
    await dashboardHomePage.adminTile();
    await dashboardAdminPage.storeProfileTile();
  });

test.skip('Partner Admin can Edit their info', async (t) => {
  const short_description = randomstring.generate(150);
  const long_description = randomstring.generate(1800);
  const url = 'http://' + randomstring.generate(10) + '.com'
  await t.expect(StoreProfilePage.storeProfileTitle.textContent).eql('Store Directory - Profile');
  await t.typeText(StoreProfilePage.companyShortDiscription, short_description, { replace: true });
  await t.typeText(StoreProfilePage.companyLongDiscription, long_description, { replace: true });
  await t.typeText(StoreProfilePage.companyUrl, url, { replace: true });

  let online_checkbox_checked = await StoreProfilePage.storeOnlineCkbx.getAttribute('aria-checked');
  let offline_checbox_checked = await StoreProfilePage.storeOnlineCkbx.getAttribute('aria-checked');
  if (!online_checkbox_checked){await t.click(StoreProfilePage.storeOnlineCkbx)}
  if (!offline_checbox_checked){await t.click(StoreProfilePage.storeOfflineCkbx)}
  if (online_checkbox_checked && offline_checbox_checked){await t.click(StoreProfilePage.storeOfflineCkbx)}

  await t.click(StoreProfilePage.imageEditTileBtn)
  await t.setFilesToUpload(StoreProfilePage.imageInput, '/Users/max.bilichenko/Downloads/funny-cat.jpeg')
  await t.click(StoreProfilePage.imageContinueBtn)
  await t.setFilesToUpload(StoreProfilePage.imageInput, '/Users/max.bilichenko/Downloads/funny-cat.jpeg')

  await t.click(StoreProfilePage.imageDoneBtn)
  await t.click(StoreProfilePage.submitUpdateBtn)
});