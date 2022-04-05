import { RequestMock } from 'testcafe';
import dotenv from 'dotenv-safe';
import chalk from 'chalk';
import locationDetailsPage from '../pages/location-details-page';
import personalDetailsPage from '../pages/personal-details-page';
import businessDetailsPage from '../pages/business-details-page';
import businessDetailsMorePage from '../pages/business-details-more-page';
import passwordPage from '../pages/password-set-page';
import partnerSignInPage from '../pages/partner-signin-page';
import helper from '../utils/helper';
import directorDetailsPage from '../pages/director-details-page';
import settlementPage from '../pages/settlement-page';
import * as appData from '../data/app-data.js';
import * as constants from '../data/constants';
import { getByText } from '@testing-library/testcafe';

dotenv.config();

const conversionMock = RequestMock()
  .onRequestTo(
    /https:\/\/api-merchant-profile.stag.au.edge.zip.co\/merchantprofile\/.*\/director-documents/,
  )
  .respond({}, 204, { 'Access-Control-Allow-Origin': '*' })
  .onRequestTo(
    /https:\/\/api-merchant-profile.stag.au.edge.zip.co\/merchantprofile\/.*\/settlement-document/,
  )
  .respond({}, 204, { 'Access-Control-Allow-Origin': '*' });
fixture('Partner Onboarding E2E Test').page(process.env.BASE_URL);
test.requestHooks(conversionMock)('Verify approved partner application', async (t) => {
  if (constants.isOnboardingAvailable) {
    const emailId = helper.createUserEmail('Merchant');
    await t.expect(personalDetailsPage.mainHeader.textContent).contains('Tell us about yourself');
    console.log('Approved partner Email id', emailId);
    await personalDetailsPage.personalDetails(emailId);
    await t
      .expect(personalDetailsPage.mainHeader.textContent)
      .contains('please introduce us to your business', {
        timeout: Number(process.env.LONG_SELECTOR_TIMEOUT),
      });
    await businessDetailsPage.businessDetails('computers');
    await t.expect(personalDetailsPage.mainHeader.textContent).contains('Business details');
    await businessDetailsMorePage.instoreOnlineSales();
    await t
      .expect(personalDetailsPage.mainHeader.textContent)
      .contains(appData.businessdetails.header);
    await locationDetailsPage.locationDetails();
    await t.expect(passwordPage.mainHeader.textContent).contains('Set password for your account');
    await passwordPage.setPassword(constants.partnerPassword);
    await t
      .expect(passwordPage.passwordConfirmation.textContent)
      .contains(`password has been set successfully`);
    await partnerSignInPage.signIn(emailId);

    await t.expect(directorDetailsPage.mainHeader.textContent).contains('Director details'),
      { timeout: Number(process.env.LONG_SELECTOR_TIMEOUT) };

    await t.click(directorDetailsPage.driverLicense);

    await directorDetailsPage.populateDirectorDetails(emailId);
    await directorDetailsPage.addLicenceDetails();
    await t.click(getByText('Submit'));
    await t.click(directorDetailsPage.submit);

    await directorDetailsPage.uploadLicenceFile();

    await t.click(getByText('Confirm'));
    await t.expect(settlementPage.mainHeader.textContent).contains('Settlement');
    await settlementPage.populateSettlement(emailId);
    await t.expect(settlementPage.secondaryHeader.textContent).contains('Application complete');
  } else {
    console.log(
      chalk.green('Skip: Onboarding Tests do not support sandbox env due to product config'),
    );
  }
});
