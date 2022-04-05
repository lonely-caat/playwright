import dashboardAdminPage from '../pages/dashboard-admin-page';
import dotenv from 'dotenv-safe';
import dashboardHomePage from '../pages/dashboard-home-page';
import PosMaterialsPage from '../pages/pos-materials-page';
import partnerSignInPage from '../pages/partner-signin-page';
import * as constants from '../data/constants';
import { ClientFunction } from 'testcafe';

dotenv.config();

fixture('Partner Admin can navigate from the dashboard to pos materials website')
  .page(process.env.ZIPPARTNER_SIGNIN)
  .beforeEach(async (t) => {
    await t.maximizeWindow();
    await partnerSignInPage.signIn(constants.partnerDashboard.merchant2.adminEmail);
    await dashboardHomePage.adminTile();
    await dashboardAdminPage.posMaterialsTile();
  });

test.skip('Partner Admin can be redirected to pos materials with his credentials as params', async (t) => {
  await t.expect(PosMaterialsPage.posMaterialsTitle.textContent).eql('In-store POS Materials');
  await t.expect(PosMaterialsPage.orderBtn.exists).ok();
  await t.click(PosMaterialsPage.orderBtn2);
  await t.wait(10000);
  let getURL = await ClientFunction(() => window.location.href)();
  await t.expect(getURL).eql('https://web2print.bluestargroup.com.au/zipmoney/index.cgi');
});
