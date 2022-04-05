const { expect } = require('@playwright/test');
const test = require('../fixtures/fixtures');
const expected = require('../constants/zipCreditData');

const { describe } = test;

describe('Applications search tests', () => {
  test.beforeEach(async ({ page }, testInfo) => {
    console.log(` \n Running ${testInfo.title}`);
    await page.goto('/Information');
  });

  test('Application search by account number with default filter selected and limited time range', async ({
    pageObjects: { navBar, applications },
  }) => {
    await navBar.selectSection('sectionApplications');
    await applications.switchApplicationType();
    await applications.setSearchDate();
    await applications.searchApplicationsLegacy('optionCreditProfileState', 'optionTypesApproved');
    const result = await applications.returnTableElements();
    expect(result.slice(0, 100)).toEqual(expected.applicationsResultsApprovedByDate.slice(0, 100));
  });

  test('Application search by account number but with default filter selected', async ({
    pageObjects: { navBar, applications },
  }) => {
    await navBar.selectSection('sectionApplications');
    await applications.searchApplications('8367');
    const result = await applications.returnTableElements();
    expect(result.slice(0, 10)).toEqual(expected.applicationsResults.slice(0, 10));
  });

  test('Application search by last name', async ({ pageObjects: { navBar, applications } }) => {
    await navBar.selectSection('sectionApplications');
    await applications.searchApplications(expected.zipCredit.LastName, 'optionLastName');
    const result = await applications.returnTableElements();
    expect(result.slice(0, 10)).toEqual(expected.applicationsResults.slice(0, 10));
  });

  test('Application search by email address', async ({ pageObjects: { navBar, applications } }) => {
    await navBar.selectSection('sectionApplications');
    await applications.searchApplications(expected.zipCredit.EmailAddress, 'optionEmailAddress');
    const result = await applications.returnTableElements();
    expect(result.slice(0, 10)).toEqual(expected.applicationsResults.slice(0, 10));
  });

  test('Application search by mobile phone number', async ({
    pageObjects: { navBar, applications },
  }) => {
    await navBar.selectSection('sectionApplications');
    await applications.searchApplications(expected.zipCredit.MobileNumber, 'optionMobileNumber');
    const result = await applications.returnTableElements();
    expect(result.slice(0, 10)).toEqual(expected.applicationsResultsMobile.slice(0, 10));
  });

  test('Application search by consumer id', async ({ pageObjects: { navBar, applications } }) => {
    await navBar.selectSection('sectionApplications');
    await applications.searchApplications(expected.zipCredit.ConsumerId, 'optionConsumerId');
    const result = await applications.returnTableElements();
    expect(result.slice(0, 10)).toEqual(expected.applicationsResults.slice(0, 10));
  });
});
