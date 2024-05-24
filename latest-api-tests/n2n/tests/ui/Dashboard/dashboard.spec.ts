import { test } from '@playwright/test';
import { DashboardPage } from '../../../logic/pages/dashboard.page';

test.describe('Dashboard', async () => {
  let dashboardPage: DashboardPage;

  test.beforeEach(async ({ page }) => {
    dashboardPage = new DashboardPage(page);
  });

  test('Scan progress @smoke @ui', async () => {
    test.slow();
    await dashboardPage.navigateTo('/dashboard/scan-progress');
    await dashboardPage.toggleInputSwitch();
  });

  test('ML tag training @smoke @ui', async () => {
    await dashboardPage.navigateTo('/dashboard/ml-tag-training');
    await dashboardPage.clickExportButton('Export Signatures');
  });
});
