import { test, expect, type Page } from '@playwright/test';
import { AgentPage } from '../../../logic/pages/agent.page';

test.describe('Administration', async () => {
  let agentPage: AgentPage;

  test.beforeEach(async ({ page }) => {
    agentPage = new AgentPage(page);
  });

  test('Agent configuration  @smoke @ui', async ({ page }) => {
    await agentPage.navigateTo('/agent/overview/installation');

    await page.getByRole('link', { name: 'Go to expert mode' }).click();
    await page.getByRole('button', { name: 'Cancel' }).click();
    // TODO I have commented this part of the code because we have a problem on the UI
    // when the user setup a custom configuration that is not supported by the wizard
    // wizard failed with the error, At the moment delta team is working on deprecation
    // wizard and the full agent flow
    // await page.getByRole('link', { name: 'Go to configuration wizard' }).click();
    // await page.getByRole('button', { name: 'previous' }).click();
  });

  test('Agent management @smoke @ui', async ({ page }) => {
    await agentPage.navigateTo('/agent/overview/management');

    await page
      .getByRole('textbox', { name: 'Find everything using GQL' })
      .fill('Test');
    await page.getByRole('button', { name: 'Clear search' }).click();
  });

  const exportTypes = ['jsonl', 'csv'];
  test.describe.parallel('Agent management export', () => {
    test.slow();
    exportTypes.forEach((exportType) => {
      test(`Export type: ${exportType} @smoke @ui`, async ({ page }) => {
        await agentPage.navigateTo('/agent/overview/management');
        await agentPage.exportData(exportType);
      });
    });
  });

  test.describe.parallel('Agent activity export', () => {
    exportTypes.forEach((exportType) => {
      test(`Export type: ${exportType} @smoke @ui`, async ({ page }) => {
        test.slow();
        await agentPage.navigateTo('/agent/overview/activity');
        await agentPage.exportData(exportType);
      });
    });
  });
});
