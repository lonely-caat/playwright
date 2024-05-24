import { test, expect } from '@playwright/test';
import { ExplorePage } from '../../../logic/pages/explore.page';
import { AgentPage } from '../../../logic/pages/agent.page';
import { v4 as uuidv4 } from 'uuid';


test.describe('Agent management page', () => {
    let agentPage: AgentPage;
    let explorePage: ExplorePage;

    test.beforeEach(async ({ browser, page }) => {
        agentPage = new AgentPage(page);
        explorePage = new ExplorePage(page);
        await agentPage.navigateTo('agent/overview/management');
    });

    test(`Verify suggestions (!=, OR, <, relative time) @smoke @ui`, async ({ page }) => {
        await explorePage.checkModeCheckbox();
        await explorePage.inputGQL('sta');
        await explorePage.clickGQLSuggestion('Online Status');
        await explorePage.clickGQLSuggestion('!=');
        await explorePage.appendTextToGQLInput('ONLINE ');
        await explorePage.clickGQLSuggestion('OR');
        await explorePage.appendTextToGQLInput('last');
        await explorePage.clickGQLSuggestion('Last Seen');
        await explorePage.clickGQLSuggestion('-3H');
        await explorePage.search();
        await explorePage.hasAtLeastOneRow();
    });

    test('Save GQL filter @smoke @ui', async ({ page }) => {
        const filterName = uuidv4();
        await explorePage.inputGQL('dev');
        await explorePage.clickGQLSuggestion('Device ID');
        await explorePage.clickGQLSuggestion('=');
        await explorePage.appendTextToGQLInput(filterName);
        await explorePage.search();
        await explorePage.addGQLFilterToFavorite(filterName);

        await explorePage.clearSearch();
        await explorePage.inputGQL(filterName.slice(0, 5));
        await explorePage.clickGQLSuggestion(filterName);
    });

    test(`Reset GQL @smoke @ui`, async ({ page }) => {
        await explorePage.checkModeCheckbox();
        await explorePage.inputGQL('os');
        await explorePage.clickGQLSuggestion('=');
        await explorePage.appendTextToGQLInput('Windows');
        await explorePage.search();
        await explorePage.clearSearch();
        await explorePage.verifyGQLEmpty();
    });

    test(`Verify export @smoke @ui`, async ({ page }) => {
        test.slow();
        await agentPage.exportData('jsonl');
    });

    test(`Verify suggestions (Relative, AND, !=) @smoke @ui`, async ({ page }) => {
        const dropDownOptions = await agentPage.returnAutoRefreshOptions();
        // @ts-ignore
        const expectedValues = '1 minute,5 minutes,10 minutes,30 minutes'.replaceAll(',', '');
        await expect(dropDownOptions).toHaveText(expectedValues);
    });

    test.describe('Column Sort', () => {
        test(`Verify Column sort: Host name`, async ({ page }) => {
            await explorePage.clickButtonAndVerifyRequest(
                page.getByText('Host name'),
                '/agent-edge/agent/v2/agents?sort=column%3DhostName%252Corder%3DASC&perPage=20&pageIndex=0',
                200,
            );
        });
    });

    test.describe('Column Sort', () => {
        test(`Verify Column sort: Online status`, async ({ page }) => {
            await explorePage.clickButtonAndVerifyRequest(
                page.getByText('Online status'),
                '/agent-edge/agent/v2/agents?sort=column%3Dstatus%252Corder%3DASC&perPage=20&pageIndex=0',
                200,
            );
        });
    });
});
