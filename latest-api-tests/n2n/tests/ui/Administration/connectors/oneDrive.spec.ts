import { test } from '@playwright/test';
import { AdminPage } from '../../../../logic/pages/admin.page';
import { faker } from '@faker-js/faker';
import { v4 as uuidv4 } from 'uuid';
import {createAndPostConnector, deleteConnector} from "../../../../logic/requests/connectors";

test.describe('Administration, connectors', async () => {
  let adminPage: AdminPage;
  let connectorName;

  test.beforeEach(async ({ page }) => {
    adminPage = new AdminPage(page);
  });

  test('Connectors: One Drive @smoke @ui', async ({ page, request }) => {
    await adminPage.navigateTo('/administration/connections/one-drive');
    connectorName = uuidv4();
    await page.getByRole('button', { name: 'New scan' }).click();
    await adminPage.oneDriveFillRequiredFields(connectorName);
    await adminPage.clickSaveSnake();
    await adminPage.checkRowExists(connectorName);
    await deleteConnector(request, 'oneDrive', connectorName);
  });
});
test.describe('Administration, connectors', async () => {
  let adminPage: AdminPage;
  let connectorName = faker.word.sample();

  test.beforeEach(async ({ page, request }) => {
    adminPage = new AdminPage(page);
    await createAndPostConnector(request, 'oneDrive', connectorName);
  });

  test('One Drive delete connection @ui', async ({ page }) => {
    await adminPage.navigateTo('/administration/connections/one-drive');
    await adminPage.clickButtonOnRowByName(connectorName, 'delete');
    await adminPage.clickAccept();
    await page.isVisible("text='Connection deleted'");
    await adminPage.waitForRowToNotExist(connectorName);
  });

  test('One Drive edit connection @ui', async ({ request }) => {
    const editedConnectorName = faker.word.sample();
    await adminPage.navigateTo('/administration/connections/one-drive');
    await adminPage.clickButtonOnRowByName(connectorName, 'edit');

    await adminPage.replaceName(editedConnectorName);
    await adminPage.clickSaveSnake();
    await adminPage.rowWithNameExists(editedConnectorName);
    await deleteConnector(request, 'oneDrive', editedConnectorName);
  });
});
