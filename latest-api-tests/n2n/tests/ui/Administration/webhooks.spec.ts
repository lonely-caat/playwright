import { test, expect } from '@playwright/test';
import { WebhooksPage } from '../../../logic/pages/webhooks.page';
import { faker } from '@faker-js/faker';
import { ExplorePage } from '../../../logic/pages/explore.page';
import { deleteWebhookByName, createWebhookByName, getWebhookByName } from '../../../logic/requests/webhooks';

test.describe('Webhooks page', async () => {
  let webhooksPage: WebhooksPage;
  let explorePage: ExplorePage;
  let webhookName: string;

  test.beforeEach(async ({ page }) => {
    webhooksPage = new WebhooksPage(page);
    explorePage = new ExplorePage(page);
    webhookName = faker.word.sample();
  });

  test.skip('Create webhook  @smoke @ui', async ({ page, request }) => {
    //TODO: https://getvisibility.atlassian.net/browse/NJ-85 needs to be fixed for this to work stable
    await webhooksPage.navigateTo('administration/webhooks');

    await webhooksPage.clickCreateWebhook();
    await webhooksPage.gqlFilterClick();
    await explorePage.clickGQLSuggestion('Flow');
    await explorePage.clickGQLSuggestion('=');
    await explorePage.clickGQLSuggestion('CLASSIFICATION');
    await explorePage.search();
    await webhooksPage.assertResultsMoreThanCount(1);

    await webhooksPage.enableWebhookInModal();
    await webhooksPage.testCallbackURL('https://google.com');
    await webhooksPage.fillName(webhookName);
    await webhooksPage.saveAndcheckWebhookActive();

    await webhooksPage.waitAndCheckWebhookActive(webhookName);
    await deleteWebhookByName(request, webhookName);
  });

  test('Edit a webhook  @smoke @ui', async ({ page, request }) => {
    await createWebhookByName(request, webhookName);
    await webhooksPage.navigateTo('administration/webhooks');
    await webhooksPage.editWebhookByName(webhookName);
    const newWebhookName = webhookName + ' edited';
    await deleteWebhookByName(request, newWebhookName);
  });

  test('Delete a webhook  @smoke @ui', async ({ page, request }) => {
    await createWebhookByName(request, webhookName);
    await webhooksPage.navigateTo('administration/webhooks');
    await webhooksPage.deleteWebhookByName(webhookName);
    const response = await getWebhookByName(request, webhookName);
    await expect(response).toBeUndefined();
  });
});
