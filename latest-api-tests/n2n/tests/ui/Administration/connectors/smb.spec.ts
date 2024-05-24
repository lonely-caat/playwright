import { test } from '@playwright/test';
import { AdminPage } from '../../../../logic/pages/admin.page';
import { faker } from '@faker-js/faker';
import { v4 as uuidv4 } from 'uuid';
import {createAndPostConnector, deleteConnector} from "../../../../logic/requests/connectors";

test.describe('Administration, connectors', async () => {
  let adminPage: AdminPage;

  test.beforeEach(async ({ page }) => {
    adminPage = new AdminPage(page);
  });

  test('Connectors: smb @smoke @ui', async ({ page, request }) => {
    await adminPage.navigateTo('/administration/connections/smb');
    const connectorName = uuidv4();

    await page.getByRole('button', { name: 'New scan' }).click();
    await adminPage.smbFillRequiredFields(connectorName);
    await adminPage.clickSaveSnake();

    await adminPage.rowWithNameExists(connectorName);
    await deleteConnector(request, 'smb', connectorName);
  });
});
test.describe.skip('Administration, connectors very brittle due to ROC-796, push to get it fixed!', async () => {
  //TODO: very brittle due to https://getvisibility.atlassian.net/browse/ROC-796, push to get it fixed!
  let adminPage: AdminPage;
  let connectorName = faker.word.sample();

  test.beforeEach(async ({ page, request }) => {
    adminPage = new AdminPage(page);
    await createAndPostConnector(request, 'smb', connectorName);
  });

  test('smb: delete connection @ui', async ({ page }) => {
    await adminPage.navigateTo('/administration/connections/smb');
    await adminPage.clickButtonOnRowByName(connectorName, 'delete');
    await adminPage.clickAccept();
    await page.isVisible("text='Connection deleted'");
    await adminPage.waitForRowToNotExist(connectorName);
  });

  test('smb: edit connection @ui', async ({ request }) => {
    const editedConnectorName = faker.word.sample();
    await adminPage.navigateTo('/administration/connections/smb');
    await adminPage.clickButtonOnRowByName(connectorName, 'edit');

    await adminPage.replaceName(editedConnectorName);
    await adminPage.clickSaveSnake();
    await adminPage.rowWithNameExists(editedConnectorName);
    await deleteConnector(request, 'smb', editedConnectorName);
  });
});
