// const { chromium } = require('playwright');
import { users } from '../data/test-data';
import { chromium } from 'playwright';
import 'core-js';
import dotenv from "dotenv";

dotenv.config();


let getToken = async () => {
  const browser = await chromium.launch({
    // headless: true
  });
  const context = await browser.newContext();

  // Open new page
  const page = await context.newPage();

  // // Go to https://merchant-dash.dev.zip.co/
  // await page.goto('https://merchant-dash.dev.zip.co/');

  // Go to https://merchant-login.dev.zip.co/login?
  await page.goto(`${process.env.BASE_URL_OAUTH}/login?`);

  // Click [placeholder="example@email.com"]
  await page.click('[placeholder="example@email.com"]');

  // Fill [placeholder="example@email.com"]
  await page.fill('[placeholder="example@email.com"]', `${users.user}`);

  // Press Tab
  await page.press('[placeholder="example@email.com"]', 'Tab');

  // Fill [placeholder="Enter password"]
  await page.fill('[placeholder="Enter password"]', `${users.password}`);

  // Press Enter
  await page.press('[placeholder="Enter password"]', 'Enter');
  const response = await page.waitForResponse(
    (response) => response.url().includes('/oauth/token') && response.status() === 200,
  );
  const body = await response.body();
  const { access_token } = JSON.parse(body.toString());
  await context.close();
  await browser.close();
  return access_token;
};

module.exports = getToken;
