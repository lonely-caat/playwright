import dashboardAdminPage from '../pages/dashboard-admin-page';
import dotenv from 'dotenv-safe';
import dashboardHomePage from '../pages/dashboard-home-page';
import StoreCategoriesPage from '../pages/store-categories-page';
import partnerSignInPage from '../pages/partner-signin-page';
import * as constants from '../data/constants';
import helper from '../utils/helper';

dotenv.config();

fixture('Partner Admin can setup different store categories')
  .page(process.env.ZIPPARTNER_SIGNIN)
  .beforeEach(async (t) => {
    await t.maximizeWindow();
    await partnerSignInPage.signIn(constants.partnerDashboard.merchant2.adminEmail);
    await dashboardHomePage.adminTile();
    await dashboardAdminPage.storeCategoriesTile();
  });

test('Partner Admin can save edited categories', async (t) => {
  /*
  Steps:
  Select random category from drop-downs
  Save selection and refresh the page
  Verify Selection is saved for the user
  Revert changes and Save
  Verify Selection is un-saved for the user
   */
  let randomCategory = await helper.getRandomInt(1, 9);

  await t
    .expect(StoreCategoriesPage.storeCategoriesTitle.textContent)
    .eql('Store Directory – Categories');

  await t.click(StoreCategoriesPage.allCategoriesDrpDwns.nth(randomCategory));
  await t.click(StoreCategoriesPage.allVisibleCategoriesDrpDwns);
  await t.click(StoreCategoriesPage.allCategoriesChoices);
  await t.click(StoreCategoriesPage.saveBtn);
  await t.wait(3000);
  await t.eval(() => location.reload(true));

  await t.click(StoreCategoriesPage.allCategoriesDrpDwns.nth(randomCategory));
  await t.click(StoreCategoriesPage.allVisibleCategoriesDrpDwns);
  await t.expect(StoreCategoriesPage.allSelectedCategoriesChoices.exists).ok;
  await t.click(StoreCategoriesPage.allSelectedCategoriesChoices);
  await t.click(StoreCategoriesPage.saveBtn);
});
