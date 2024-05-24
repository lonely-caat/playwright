import { test, expect, Page } from '@playwright/test';
import { AnalyticsPage } from '../../../logic/pages/analytics.page';
import { faker } from '@faker-js/faker';
import { AgentPage } from '../../../logic/pages/agent.page';

test.describe('Analytics', async () => {
  let analyticsPage: AnalyticsPage;
  let agentPage: AgentPage;
  let boardName: string;

  test.beforeEach(async ({ page }) => {
    analyticsPage = new AnalyticsPage(page);
    await analyticsPage.navigateTo('/analytics');
    boardName = faker.word.sample();
  });

  test.afterEach(async ({ request }) => {
    if (!boardName) return;
    const items = await request.get('analytics-hub/reports').then((res) => res.json());
    const boardId = items.find((item) => item.name === boardName)?.id;
    if (boardId) await request.delete(`analytics-hub/report/${boardId}`);
  });

  test('Analytics create board @smoke @ui', async ({ page }) => {
    await analyticsPage.createCustomBoardStep1(boardName, 1, boardName + ' description');
    await expect(page.getByText('Board created successfully')).toBeVisible();
    await expect(
      page.getByText(
        'Your board is empty. Click on the Edit Mode menu to add some widgets or change your board details.',
      ),
    ).toBeVisible();
  });

  const counterDatasets = ['Agent activity dataset', 'Agent management dataset', 'Files dataset'];
  test.describe.parallel('Add counter widget', () => {
    counterDatasets.forEach((dataset) => {
      test(`counter widget with dataset: ${dataset} @smoke @ui`, async ({ page }) => {
        await analyticsPage.createCustomBoardStep1(boardName, 2, boardName + ' description');
        await analyticsPage.searchForBoard(boardName);
        await analyticsPage.editCustomBoard(boardName);
        await analyticsPage.selectWidgetType('Counter');

        await analyticsPage.addCounterWidget(dataset, 'max', 'Widget Title', 'Top');
      });
    });
  });

  const chartDatasets = {
    'Agent activity dataset': 'Department',
    'Agent management dataset': 'Domain',
    'Files dataset': 'Category',
  };
  test.describe.parallel('Add chart widget', () => {
    Object.entries(chartDatasets).forEach(([dataset, value]) => {
      test(`chart widget with dataset: ${dataset} @smoke @ui`, async ({ page }) => {
        await analyticsPage.createCustomBoardStep1(boardName, 2, boardName + ' description');
        await analyticsPage.searchForBoard(boardName);
        await analyticsPage.editCustomBoard(boardName);
        await analyticsPage.selectWidgetType('Chart');
        await analyticsPage.addChartWidget(dataset, value, 'Widget Title', 'subtitle', 'Top');
      });
    });
  });

  test('Add text widget @smoke @ui', async ({ page }) => {
    await analyticsPage.createCustomBoardStep1(boardName, 2, boardName + ' description');
    await analyticsPage.searchForBoard(boardName);
    await analyticsPage.editCustomBoard(boardName);
    await analyticsPage.selectWidgetType('Text');
    await analyticsPage.addTextWidget('Widget text here');
  });

  const tableDatasets = {
    'Agent activity dataset': ['senderEmail', 'tags'],
    'Agent management dataset': ['hostName', 'domain'],
    'Files dataset': ['fileId', 'modelVersion'],
  };
  test.describe.parallel('Add table widget', () => {
    Object.entries(tableDatasets).forEach(([dataset, value]) => {
      test(`table widget with dataset: ${dataset} @smoke @ui`, async ({ page }) => {
        await analyticsPage.createCustomBoardStep1(boardName, 2, boardName + ' description');
        await analyticsPage.searchForBoard(boardName);
        await analyticsPage.editCustomBoard(boardName);
        await analyticsPage.selectWidgetType('Table');
        await analyticsPage.addTableWidget(dataset, value, value[0], 'Widget Title', 'Subtitle', 'Top');
      });
    });
  });
  test('Export data @ui', async ({ page }) => {
    // somehow pdf-generator/generator call is very slow in the pipeline
    test.setTimeout(60000);
    await analyticsPage.createCustomBoardStep1(boardName, 2, boardName + ' description');
    await analyticsPage.searchForBoard(boardName);
    await analyticsPage.editCustomBoard(boardName);
    await analyticsPage.selectWidgetType('Text');
    await analyticsPage.addTextWidget(faker.lorem.sentences(5));
    await page.waitForLoadState('networkidle');
    await page.getByText('Exit edit mode').click();
    await analyticsPage.clickButtonAndVerifyRequest(page.getByText('Export PDF'), 'pdf-generator/generator', 201);
  });
  test(`Verify refresh options @smoke @ui`, async ({ page }) => {
    agentPage = new AgentPage(page);
    // TODO: consider moving to base page or something
    const dropDownOptions = await agentPage.returnAutoRefreshOptions();
    // @ts-ignore
    const expectedValues = '1 minute,5 minutes,10 minutes,30 minutes'.replaceAll(',', '');
    await expect(dropDownOptions).toHaveText(expectedValues);
  });
});
