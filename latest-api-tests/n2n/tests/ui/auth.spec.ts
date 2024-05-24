import { test, expect } from '@playwright/test';

const username = process.env.USERNAME == null ? 'gv' : process.env.USERNAME;
const password = process.env.PASSWORD == null ? 'gv' : process.env.PASSWORD;
const invalidCredentials = [
  { username: '', password: '' }, // Both fields are empty
  { username: 'user1', password: '' }, // Valid username format but empty password
  { username: '', password: 'pass1' }, // Empty username but valid password format
  { username: 'invalidUser', password: 'invalidPass' }, // Both are invalid
  { username: '±!@#$%^&*()_+', password: '±!@#$%^&*()_+' }, // Both contain special symbols
  { username: username, password: 'invalidPass' }, // valid username invalid password
  { username: 'invalidUser', password: password }, // invalid username valid password
  { username: 'u'.repeat(256), password: 'p'.repeat(256) }, // Huge username and password
  { username: '</input>select * from Users;', password: "<script>alert('Test');</script>",}, // Simple injection
];

test.describe('Login tests', () => {
  test.use({ storageState: { cookies: [], origins: [] } });

  test.beforeEach(async ({ page }) => {
    await page.goto('/ui/');
  });

  invalidCredentials.forEach(({ username, password }) => {
    test(`should fail for invalid credentials - Username: "${username}" / Password: "${password}"`, async ({
      page,
    }) => {
      await page.getByLabel('Username').fill(username);
      await page.getByLabel('Password').fill(password);
      await page.getByRole('button', { name: 'Login' }).click();
      await expect(
        page.getByText('Invalid username or password.'),
      ).toBeVisible();
    });
  });

  test('Check Copyright', async ({ page }) => {
    await expect(
      page.getByText(
        `Copyright ©${new Date().getFullYear()} by GetVisibility`,
      ),
    ).toBeVisible();
  });
});
