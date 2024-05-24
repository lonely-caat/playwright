import { test } from '@playwright/test';
import { AdminPage } from '../../../../logic/pages/admin.page';
import { faker } from '@faker-js/faker';
import { v4 as uuidv4 } from 'uuid';
import { createAndPostConnector, deleteConnector } from "../../../../logic/requests/connectors";

test.describe('Administration, connectors', async () => {
  let adminPage: AdminPage;

  test.beforeEach(async ({ page }) => {
    adminPage = new AdminPage(page);
  });

  test('Connectors: AWS s3  @smoke @ui', async ({ page, request }) => {
    await adminPage.navigateTo('/administration/connections/aws-s3');
    const connectorName = uuidv4();

    await page.getByRole('button', { name: 'New scan' }).click();
    await adminPage.awsFillRequiredFields(connectorName);
    await adminPage.clickSaveSnake();

    await adminPage.checkRowExists(connectorName);
    await deleteConnector(request, 'aws', connectorName);
  });
});
test.describe('Administration, connectors', async () => {
  let adminPage: AdminPage;
  let connectorName = faker.word.sample();

  test.beforeEach(async ({ page, request }) => {
    adminPage = new AdminPage(page);
    await createAndPostConnector(request, 'aws', connectorName);
  });

  test('AWS s3: delete connection @ui', async ({ page }) => {
    await adminPage.navigateTo('/administration/connections/aws-s3');
    await adminPage.clickButtonOnRowByName(connectorName, 'delete');
    await adminPage.clickAccept();
    await page.isVisible("text='Connection deleted'");
    await adminPage.waitForRowToNotExist(connectorName);
  });

  test('AWS s3: edit connection @ui', async ({ request }) => {
    const editedConnectorName = faker.word.sample();
    await adminPage.navigateTo('/administration/connections/aws-s3');
    await adminPage.clickButtonOnRowByName(connectorName, 'edit');

    await adminPage.replaceName(editedConnectorName);
    await adminPage.clickSaveSnake();
    await adminPage.rowWithNameExists(editedConnectorName);
    await deleteConnector(request, 'aws', editedConnectorName);
  });
});
