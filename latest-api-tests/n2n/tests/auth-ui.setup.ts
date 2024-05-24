import { test as setup, expect, APIRequestContext } from '@playwright/test';

const authFile = 'playwright/.auth/user.json';

setup('authenticate', async ({ request, page, context }) => {
  // Perform authentication steps.
  await page.goto('/ui/');

  const username = process.env.USERNAME == null ? 'gv' : process.env.USERNAME;
  const password = process.env.PASSWORD == null ? 'gv' : process.env.PASSWORD;

  await page.getByLabel('Username').click();
  await page.getByLabel('Username').fill(username);
  await page.getByLabel('Password').click();
  await page.getByLabel('Password').fill(password);
  await page.getByRole('button', { name: 'Login' }).click();

  // End of authentication steps.
  await page.context().storageState({ path: authFile });
});
