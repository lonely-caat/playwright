import { test, expect } from '@playwright/test';
import { retryCheck } from '../../../../logic/retryCheck';
import { AdminPage } from '../../../../logic/pages/admin.page';
import { v4 as uuidv4 } from 'uuid';
import { createAndPostConnector, deleteConnector } from '../../../../logic/requests/connectors';
import { ExplorePage } from "../../../../logic/pages/explore.page";
import { BasePage } from "../../../../logic/pages/base.page";

test.describe(
  'Scan and classification pipeline e2e test as described in ' +
  'https://getvisibility.atlassian.net/wiki/spaces/GS/pages/460226565/031+-+n2n+test+to+verify+scan+and+classification+pipeline',
  async () => {
    let adminPage: AdminPage;
    let explorePage: ExplorePage;
    let basePage: BasePage;
      // Set retries to 0 to avoid long test duration but set timeout to 10 minutes to make sure we capture the flow
      test.describe.configure({
          retries: 0,
      });

    test('Connectors: SharePoint Online @e2e @ui', async ({ page, request }) => {

      test.setTimeout(600000);

      adminPage = new AdminPage(page);
      basePage = new BasePage(page);
      explorePage = new ExplorePage(page);
      //TODO: consider the need to automate, might be a bad idea
      const filePath = 'sites/test QA/automation dont modify';


      await adminPage.navigateTo('/administration/connections/sharepoint-online');
      // const connectorName = uuidv4();
      const connectorName = 'meow?';

      await createAndPostConnector(request, 'sharePointOnLine', connectorName, filePath);

      await adminPage.clickButtonOnRowByName(connectorName, 'scan');
      await basePage.clickOnAcceptButton();

      await explorePage.navigateTo('dashboard/explore-files');
      await explorePage.checkModeCheckbox();
      await explorePage.inputGQL(`source=SHAREPOINT_ONLINE  AND ingestedAt="-1HOURS" AND path="${filePath}"`);
      await explorePage.search()



      console.log(await explorePage.returnTablePaths(), '!!!!!!!!!!!!!!!!');
      // await expect(retryCheck(page, await explorePage.search, await explorePage.returnTablePaths(), ['element1', 'element2', 'element3'])).resolves.toBe(true);
        await expect(retryCheck(page,
            () => explorePage.search(),
            () => explorePage.returnTablePaths(),
            ['element1', 'element2', 'element3'])).resolves.toBe(true);
    });
  },
);
