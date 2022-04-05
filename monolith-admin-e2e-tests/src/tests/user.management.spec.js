const { expect } = require('@playwright/test');
const test = require('../fixtures/fixtures');
const expected = require('../constants/zipCreditData');

const { describe } = test;

describe('User management tests', () => {
    test.beforeEach(async ({ page }, testInfo) => {
        console.log(` \n Running ${testInfo.title}`);
        await page.goto('/Information');
    });

    test('User management search by ID', async ({ pageObjects: { navBar, userManagement }, page }) => {
        await navBar.selectSection('sectionUserManagement');
        await userManagement.searchUsers();
        await userManagement.clickDetailsButton();
        const result = await userManagement.returnUserRoles();
        const expectedResult = expected.manageUsersExpectedRoles;

        expect(result).toEqual(expectedResult);
    });
});