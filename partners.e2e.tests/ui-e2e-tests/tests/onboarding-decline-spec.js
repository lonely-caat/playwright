import dotenv from 'dotenv-safe';
import chalk from 'chalk';
import personalDetailsPage from '../pages/personal-details-page';
import businessDetailsPage from '../pages/business-details-page';
import businessDetailsMorePage from '../pages/business-details-more-page';
import declinedApplicationPage from '../pages/declined-application-page';
import helper from '../utils/helper';
import * as constants from '../data/constants';

dotenv.config();

fixture('Partner Onboarding Negative E2E Test').page(process.env.BASE_URL);
/*
  Prequalification Rules

    Merchants satisfying any of the following rules are declined after Step 3:
        IndustryType = Travel && totalTurnover < 20000000
        SubIndustryType = Tattoo
        SubIndustryType = Piercing
        SalesMethod = Online && OnlineRevenue < 250000 && SubIndustryType !=  Hairdressers && SalesMetod != Shopify/BigCommerce
        TradingSince < 6 Months
 */
test('Verify declined partner application tattoo', async (t) => {
  if (constants.isOnboardingAvailable) {
    const emailId = helper.createUserEmail('Merchant');
    await t.expect(personalDetailsPage.mainHeader.textContent).contains('Tell us about yourself');
    console.log('Declined partner Email id', emailId);
    await personalDetailsPage.personalDetails(emailId);
    await t
      .expect(personalDetailsPage.mainHeader.textContent)
      .contains('please introduce us to your business');
    await businessDetailsPage.businessDetails('tattoo');
    await t.expect(personalDetailsPage.mainHeader.textContent).contains('Business details');
    await businessDetailsMorePage.instoreSales();
    await t
      .expect(declinedApplicationPage.mainHeader.textContent)
      .contains('Application sent!')
      .expect(declinedApplicationPage.declineContent.textContent)
      .contains('Your application has been received and will be reviewed by our dedicated team.')
      .expect(declinedApplicationPage.homePageLink.textContent)
      .contains('Back to homepage');
  } else {
    console.log(
      chalk.green('Skip: Onboarding Tests do not support sandbox env due to product config'),
    );
  }
});
test('Verify declined partner application piercing', async (t) => {
  if (constants.isOnboardingAvailable) {
    const emailId = helper.createUserEmail('Merchant');
    await t.expect(personalDetailsPage.mainHeader.textContent).contains('Tell us about yourself');
    console.log('Declined partner Email id', emailId);
    await personalDetailsPage.personalDetails(emailId);
    await t
      .expect(personalDetailsPage.mainHeader.textContent)
      .contains('please introduce us to your business');
    await businessDetailsPage.businessDetails('piercing');
    await t.expect(personalDetailsPage.mainHeader.textContent).contains('Business details');
    await businessDetailsMorePage.instoreSales();
    await t
      .expect(declinedApplicationPage.mainHeader.textContent)
      .contains('Application sent!')
      .expect(declinedApplicationPage.declineContent.textContent)
      .contains('Your application has been received and will be reviewed by our dedicated team.')
      .expect(declinedApplicationPage.homePageLink.textContent)
      .contains('Back to homepage');
  } else {
    console.log(
      chalk.green('Skip: Onboarding Tests do not support sandbox env due to product config'),
    );
  }
});
test('Verify declined partner application travel below 250k', async (t) => {
  if (constants.isOnboardingAvailable) {
    const emailId = helper.createUserEmail('Merchant');
    await t.expect(personalDetailsPage.mainHeader.textContent).contains('Tell us about yourself');
    console.log('Declined partner Email id', emailId);
    await personalDetailsPage.personalDetails(emailId);
    await t
      .expect(personalDetailsPage.mainHeader.textContent)
      .contains('please introduce us to your business');
    await businessDetailsPage.businessDetails('travel');
    await t.expect(personalDetailsPage.mainHeader.textContent).contains('Business details');

    await businessDetailsMorePage.instoreSales(businessDetailsMorePage.annualSales250k);
    await t
      .expect(declinedApplicationPage.mainHeader.textContent)
      .contains('Application sent!')
      .expect(declinedApplicationPage.declineContent.textContent)
      .contains('Your application has been received and will be reviewed by our dedicated team.')
      .expect(declinedApplicationPage.homePageLink.textContent)
      .contains('Back to homepage');
  } else {
    console.log(
      chalk.green('Skip: Onboarding Tests do not support sandbox env due to product config'),
    );
  }
});

test('Verify declined partner application travel below 1kk', async (t) => {
  if (constants.isOnboardingAvailable) {
    const emailId = helper.createUserEmail('Merchant');
    await t.expect(personalDetailsPage.mainHeader.textContent).contains('Tell us about yourself');
    console.log('Declined partner Email id', emailId);
    await personalDetailsPage.personalDetails(emailId);
    await t
      .expect(personalDetailsPage.mainHeader.textContent)
      .contains('please introduce us to your business');
    await businessDetailsPage.businessDetails('travel');
    await t.expect(personalDetailsPage.mainHeader.textContent).contains('Business details');

    await businessDetailsMorePage.instoreSales(businessDetailsMorePage.annualSales1kk);
    await t
      .expect(declinedApplicationPage.mainHeader.textContent)
      .contains('Application sent!')
      .expect(declinedApplicationPage.declineContent.textContent)
      .contains('Your application has been received and will be reviewed by our dedicated team.')
      .expect(declinedApplicationPage.homePageLink.textContent)
      .contains('Back to homepage');
  } else {
    console.log(
      chalk.green('Skip: Onboarding Tests do not support sandbox env due to product config'),
    );
  }
});
test(
  'Verify declined partner application Online && OnlineRevenue < 250000 && SubIndustryType' +
    ' !=  Hairdressers && SalesMetod != Shopify/BigCommerce',
  async (t) => {
    if (constants.isOnboardingAvailable) {
      const emailId = helper.createUserEmail('Merchant');
      await t.expect(personalDetailsPage.mainHeader.textContent).contains('Tell us about yourself');
      console.log('Declined partner Email id', emailId);
      await personalDetailsPage.personalDetails(emailId);
      await t
        .expect(personalDetailsPage.mainHeader.textContent)
        .contains('please introduce us to your business');
      await businessDetailsPage.businessDetails('travel');
      await t.expect(personalDetailsPage.mainHeader.textContent).contains('Business details');

      await businessDetailsMorePage.onlineSales(businessDetailsMorePage.annualSales250k, 'Magento');
      await t
        .expect(declinedApplicationPage.mainHeader.textContent)
        .contains('Application sent!')
        .expect(declinedApplicationPage.declineContent.textContent)
        .contains('Your application has been received and will be reviewed by our dedicated team.')
        .expect(declinedApplicationPage.homePageLink.textContent)
        .contains('Back to homepage');
    } else {
      console.log(
        chalk.green('Skip: Onboarding Tests do not support sandbox env due to product config'),
      );
    }
  },
);
