const { chromium } = require('@playwright/test');
const { USERS } = require('../constants/usersData');
const urls = require('../constants/urls')


const elements = {
  header: 'text=Sign in as Zip partner',
  emailFieldMS: '//input[@type="email"]',
  passwordFieldMS: '//input[@type="password"]',
  continueButtonMS: '//input[@type="submit"]',
  // need this until onelogin/my apps integration bugs are fixed
  applicationsTab: '//span[contains(text(), "Applications")]'
};

module.exports = async config => {

    const browser = await chromium.launch();
    const page = await browser.newPage();
    await page.goto(urls.monolithLandingPage)

    const {passwordFieldMS, emailFieldMS, continueButtonMS, applicationsTab} = elements;
    // Handle MS My Apps
    await page.waitForSelector(emailFieldMS);
    await page.fill(emailFieldMS, USERS.oneLoginUser.adminEmail);
    await page.click(continueButtonMS);
    await page.fill(passwordFieldMS, USERS.oneLoginUser.adminPassword);
    await page.click(continueButtonMS);

    await page.waitForSelector(applicationsTab)
    await page.context().storageState({path: 'storageState.json'});
}
