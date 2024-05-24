import { test, expect, request } from '@playwright/test';
import { ExplorePage } from '../../../logic/pages/explore.page';
import { AgentPage } from '../../../logic/pages/agent.page';
import { v4 as uuidv4 } from 'uuid';
import { getExtensions, getSource } from '../../../logic/requests/explore';
import {createAndPostConnector, deleteConnector} from "../../../logic/requests/connectors";
import {faker} from "@faker-js/faker";

test.describe('Explore page', () => {
  let explorePage: ExplorePage;
  // TODO: move out export from agent page to a common page like dashboard page
  let agentPage: AgentPage;
  let source;
  let extensions;

  test.beforeAll(async ({ request }) => {
    source = await getSource(request);
    extensions = await getExtensions(request);
  });

  test.beforeEach(async ({ browser, page }) => {
    explorePage = new ExplorePage(page);
    agentPage = new AgentPage(page);
    await explorePage.navigateTo('dashboard/explore-files');
  });

  test.describe('Advanced search mode', () => {
    test(`Verify suggestions (=, OR, <, int) @smoke @ui`, async ({ request }) => {
      // Unless a connector is present in the system, GQL won't find it
      const connectorName = faker.word.sample()
      await createAndPostConnector(request, 'box', connectorName);

      await explorePage.checkModeCheckbox();
      await explorePage.inputGQL('sour');
      await explorePage.clickGQLSuggestion('Source');
      await explorePage.clickGQLSuggestion('=');
      await explorePage.appendTextToGQLInput('box ');
      await explorePage.clickGQLSuggestion('OR');
      await explorePage.inputGQL('ris');
      await explorePage.clickGQLSuggestion('Risk');
      await explorePage.clickGQLSuggestion('<');
      await explorePage.assertQueryHelperMessage('Insert a numeric value');
      await explorePage.appendTextToGQLInput('1');
      await explorePage.search();
      await explorePage.hasAtLeastOneRow();

      await deleteConnector(request, 'sharePointOnPremise', connectorName);
    });

    test(`Verify suggestions (Relative, AND, !=) @smoke @ui`, async ({ page }) => {
      await explorePage.checkModeCheckbox();
      await explorePage.inputGQL('cr');
      await explorePage.clickGQLSuggestion('Created at');
      await explorePage.clickGQLSuggestion('-5Y');
      await explorePage.clickGQLSuggestion('AND');
      await explorePage.inputGQL('cat');
      await explorePage.clickGQLSuggestion('Category');
      await explorePage.clickGQLSuggestion('!=');
      await explorePage.appendTextToGQLInput('HR_Documents');
      await explorePage.search();
      await explorePage.hasAtLeastOneRow();
    });

    test(`Verify suggestions (<=,float,!=) @smoke @ui`, async ({ page }) => {
      await explorePage.checkModeCheckbox();
      await explorePage.inputGQL('cat');
      await explorePage.clickGQLSuggestion('Category');
      await explorePage.clickGQLSuggestion('=');
      await explorePage.appendTextToGQLInput('bus');
      await explorePage.clickGQLSuggestion('Business_Documents');
      await explorePage.clickGQLSuggestion('AND');
      await explorePage.appendTextToGQLInput('sig');
      await explorePage.clickGQLSuggestion('Signature Confidence');
      await explorePage.clickGQLSuggestion('!=');
      await explorePage.assertQueryHelperMessage('Insert a numeric value with a decimal point');
      await explorePage.appendTextToGQLInput('1.6');
      await explorePage.search();
      await explorePage.hasAtLeastOneRow();
    });

    test(`Verify export @smoke @ui`, async ({ page }) => {
      test.slow();
      await agentPage.exportData('jsonl');
    });

    test.skip(`Reset GQL @smoke @ui`, async ({ page }) => {
      await explorePage.checkModeCheckbox();
      await explorePage.inputGQL('fl');
      await explorePage.clickGQLSuggestion('Flow');
      await explorePage.clickGQLSuggestion('=');
      await explorePage.appendTextToGQLInput('cat');
      await explorePage.clickGQLSuggestion('CATALOGING');
      await explorePage.search();
      await explorePage.clearSearch();
      await explorePage.verifyGQLEmpty();
    });

    test('Edit classification @smoke @ui', async ({ page }) => {
      await explorePage.checkModeCheckbox();
      await explorePage.inputGQL('class');
      await explorePage.clickGQLSuggestion('Classification');
      await explorePage.clickGQLSuggestion('=');
      await explorePage.clickGQLSuggestion('Internal');
      await explorePage.search();
      await explorePage.editFirstClassification();
    });
    test('View permissions @smoke @ui', async ({ page }) => {
      await explorePage.checkModeCheckbox();
      await explorePage.inputGQL('clas');
      await explorePage.clickGQLSuggestion('Classification');
      await explorePage.clickGQLSuggestion('=');
      await explorePage.clickGQLSuggestion('Public');
      await explorePage.search();
      await explorePage.viewFirstPermissions();
    });

    test('Send to Classification @smoke @ui', async ({ page }) => {
      await explorePage.checkModeCheckbox();
      await explorePage.inputGQL('fl');
      await explorePage.clickGQLSuggestion('Flow');
      await explorePage.clickGQLSuggestion('=');
      await explorePage.clickGQLSuggestion('CLASSIFICATION');
      await explorePage.search();
      await explorePage.verifySendToClassification();
    });

    test('Save GQL filter @smoke @ui', async ({ page }) => {
      const filterName = uuidv4();
      await explorePage.checkModeCheckbox();
      await explorePage.inputGQL('md');
      await explorePage.clickGQLSuggestion('Document hash');
      await explorePage.clickGQLSuggestion('!=');
      await explorePage.appendTextToGQLInput(filterName);
      await explorePage.search();
      await explorePage.addGQLFilterToFavorite(filterName);

      await explorePage.clearSearch();
      await explorePage.inputGQL(filterName.slice(0, 5));
      await explorePage.clickGQLSuggestion(filterName);
    });
  });

  test.describe('Basic search mode', () => {
    test(`All filter options @smoke @ui`, async ({ page }) => {
      await explorePage.basicFilterMultiSelect('Select file extension', extensions.slice(-2));
      await explorePage.basicFilterSelect('Select source', source);
      await explorePage.basicFilterSelect('Select risk', 'High');
      await explorePage.basicFilterSelectFirstOption('Select Regex');
      await explorePage.basicFilterSelect('Select group', 'sensitive');
      await explorePage.basicFilterSelectFirstOption('Select compliance');
      await explorePage.hasAtLeastOneRow();
    });

    test(`Reset filters @smoke @ui`, async ({ page }) => {
      await explorePage.basicFilterMultiSelect('Select file extension', extensions.slice(0, 2));
      await explorePage.basicFilterSelect('Select source', source);
      await explorePage.clearSearch();
      await expect(page.getByText('Select file extension')).toBeVisible();
      await expect(page.getByText('Select source')).toBeVisible();
    });

    const columns = ['Path MD5', 'File Type', 'File Size', 'File ID', 'Critical'];
    test.skip('Adjust Column visibility @smoke @ui', async ({ page }) => {
      for (const column of columns) {
        await explorePage.changeColumnVisibility(column);
      }
    });
  });
});
