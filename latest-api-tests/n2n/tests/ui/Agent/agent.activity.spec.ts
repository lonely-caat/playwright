import { test, expect } from '@playwright/test';
import { ExplorePage } from '../../../logic/pages/explore.page';
import { AgentPage } from '../../../logic/pages/agent.page';

test.describe('Agent activity page', () => {
  let agentPage: AgentPage;
  let explorePage: ExplorePage;

  test.beforeEach(async ({ browser, page }) => {
    agentPage = new AgentPage(page);
    explorePage = new ExplorePage(page);
    await agentPage.navigateTo('/agent/overview/activity');
  });

  test(`Verify export @smoke @ui`, async ({ page }) => {
    test.slow();
    await agentPage.exportData('jsonl');
  });

  test(`Verify suggestions (=, OR, <, relative time) @smoke @ui`, async ({ page }) => {
    await explorePage.checkModeCheckbox();
    await explorePage.inputGQL('rec');
    await explorePage.clickGQLSuggestion('Email Recipients');
    await explorePage.clickGQLSuggestion('=');
    await explorePage.appendTextToGQLInput('meow@getvisibility.com ');
    await explorePage.clickGQLSuggestion('OR');
    await explorePage.appendTextToGQLInput('crea');
    await explorePage.clickGQLSuggestion('Created At');
    await explorePage.clickGQLSuggestion('-3H');
    await explorePage.search();
    await explorePage.hasAtLeastOneRow();
  });

  test(`Verify refresh options @smoke @ui`, async ({ page }) => {
    const dropDownOptions = await agentPage.returnAutoRefreshOptions();
    // @ts-ignore
    const expectedValues = '1 minute,5 minutes,10 minutes,30 minutes'.replaceAll(',', '');
    await expect(dropDownOptions).toHaveText(expectedValues);
  });

  test(`Reset GQL @smoke @ui`, async ({ page }) => {
    await explorePage.checkModeCheckbox();
    await explorePage.inputGQL('ta');
    await explorePage.clickGQLSuggestion('Tags');
    await explorePage.clickGQLSuggestion('!=');
    await explorePage.appendTextToGQLInput('Secret');;
    await explorePage.search();
    await explorePage.clearSearch();
    await explorePage.verifyGQLEmpty();
  });

  test.describe('Column Sort', () => {
    test(`Verify Column sort: Event Time`, async ({ page }) => {
      await explorePage.clickButtonAndVerifyRequest(
        page.getByText('Event time'),
        '/agent-edge/audit/v2/activities?sort=column%3DeventTime%252Corder%3DASC',
        200,
      );
    });
  });

  test.describe('Column Sort', () => {
    test(`Verify Column sort: Operation`, async ({ page }) => {
      await explorePage.clickButtonAndVerifyRequest(
        page.getByText('Operation'),
        '/agent-edge/audit/v2/activities?sort=column%3Doperation%252Corder%3DASC',
        200,
      );
    });
  });
});
