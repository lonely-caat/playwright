const { expect } = require('@playwright/test');
const test = require('../fixtures/fixtures');
const expected = require('../constants/zipCreditData');

const { describe } = test;

describe('Applications search tests', () => {
  test('Account search by account number with default filter selected and limited time range to get detailed info',
    async ({ pageObjects: { navBar, applications }, page }) => {
      await page.goto('/Information');
      await navBar.selectSection('sectionApplications');
      await applications.switchApplicationType();
      await applications.setSearchDate();
      await applications.searchApplicationsLegacy(
        'optionCreditProfileState',
        'optionTypesApproved',
      );
      await applications.clickDetailsButton();
      const result = await applications.returnDetailTablesElements();
      expect(result.substr(0, 3000)).toContain(expected.applicationResultsApprovedByDateDetails.substr(0, 3000));
    },
  );
});
