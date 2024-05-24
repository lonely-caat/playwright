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

  test('Connectors: Azure Blob Storage @smoke @ui', async ({ page, request }) => {
    await adminPage.navigateTo('/administration/connections/azure-blob');
    const connectorName = uuidv4();

    await page.getByRole('button', { name: 'New scan' }).click();
    await adminPage.azureFillRequiredFields(connectorName);
    await adminPage.clickSaveSnake();

    await adminPage.checkRowExists(connectorName);
    await deleteConnector(request, 'azureBlob', connectorName);
  });
});
test.describe('Administration, connectors', async () => {
  let adminPage: AdminPage;
  let connectorName = faker.word.sample();

  test.beforeEach(async ({ page, request }) => {
    adminPage = new AdminPage(page);
    await createAndPostConnector(request, 'azureBlob', connectorName);
  });

  test('Azure blob: delete connection @ui', async ({ page }) => {
    await adminPage.navigateTo('/administration/connections/azure-blob');
    await adminPage.clickButtonOnRowByName(connectorName, 'delete');
    await adminPage.clickAccept();
    await page.isVisible("text='Connection deleted'");
    await adminPage.waitForRowToNotExist(connectorName);
  });

  test('Azure blob: edit connection @ui', async ({ request }) => {
    const editedConnectorName = faker.word.sample();
    await adminPage.navigateTo('/administration/connections/azure-blob');
    await adminPage.clickButtonOnRowByName(connectorName, 'edit');

    await adminPage.replaceName(editedConnectorName);
    await adminPage.clickSaveSnake();
    await adminPage.rowWithNameExists(editedConnectorName);
    await deleteConnector(request, 'azureBlob', editedConnectorName);
  });
});
