import { test, expect } from '@playwright/test';
import { AdminPage } from '../../../logic/pages/admin.page';

test.describe('Administration', async () => {
  let adminPage: AdminPage;

  test.beforeEach(async ({ page }) => {
    adminPage = new AdminPage(page);
  });

  test('IAM connections @smoke @ui', async ({ page }) => {
    const urls = ['ldap'];
    for (const url of urls) {
      await adminPage.navigateTo(`/administration/connections/'${url}`);

      await page.getByRole('button', { name: 'New scan' }).click();
      await page.getByRole('button', { name: 'Cancel' }).click();
    }
  });

  test('Pattern matching @smoke @ui', async ({ page }) => {
    await adminPage.navigateTo('/administration/regex');

    await page.getByText('join_innerPattern Matching').click();
    await page.getByLabel('add-new-pattern-button').click();
    await page.getByRole('button', { name: 'Cancel' }).click();
  });

  test('User management @smoke @ui', async ({ page }) => {
    await adminPage.navigateTo('/administration/usermanagement');

    await page.getByRole('button', { name: 'Access User Management' }).click();
  });

  test('Network settings @smoke @ui', async () => {
    await adminPage.navigateTo('/administration/network');
    await adminPage.clickInputSwitch();
    await adminPage.clickInputSwitch();
  });

  test('Webhooks @smoke @ui', async ({ page }) => {
    await adminPage.navigateTo('/administration/webhooks');

    await page.getByRole('button', { name: 'Create Webhook' }).click();
    await page.getByRole('button', { name: 'Cancel' }).click();
  });

  test('Default language settings @smoke @ui', async ({ page }) => {
    await adminPage.navigateTo('/administration/language');
    await adminPage.testDefaultLanguage()
  });

  const languages = {
    'العربية': 'ar-AE',
    'Deutsch': 'de-DE',
    'English': 'en-US',
    'Español': 'es-ES',
    'Français': 'fr-FR',
    'עִברִית': 'he-IL',
    'Polski': 'pl-PL',
    'Português': 'pt-PT',
    'ไทย': 'th-TH',
    '中文': 'zh-Hans',
  };
  test.describe.parallel('Languages settings', () => {
    Object.entries(languages).forEach(([language, language_code]) => {
      test(`system language:: ${language} @smoke @ui`, async ({ page }) => {
        await adminPage.navigateTo('/administration/language');
        await adminPage.testLanguages(language, language_code)
        await adminPage.testDefaultLanguage()
      });
    });
  });
});
