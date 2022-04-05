const { expect } = require('@playwright/test');
const test = require('../fixtures/fixtures');
const generateData = require('../helpers/generateData');




const { describe } = test;

describe('Failed onboarding flow tests', () => {
    /**
     Prequalification Rules
     Merchants satisfying any of the following rules are declined after Step 3:
     IndustryType = Travel && totalTurnover < 20000000
     SubIndustryType = Tattoo
     SubIndustryType = Piercing
     SalesMethod = Online && OnlineRevenue < 250000 && SubIndustryType !=  Hairdressers && SalesMetod != Shopify/BigCommerce
     TradingSince < 6 Months
     */
    test('Failed onboarding flow - industry Piercings', async ({
                                                pageObjects: { onboarding },
                                                startOnboarding: page,
                                            }) => {

        const random_email = generateData.createRandomEmail()

        await onboarding.tellAboutYourself(random_email)
        expect((await onboarding.getH2Text()).includes('please introduce us to your business'))
        await onboarding.tellAboutYourBusiness('Piercings')
        expect((await onboarding.getH2Text()).includes('Business details'))
        await onboarding.tellBusinessDetailsOnlineAndBoth()
        await onboarding.tellBusinessService()
        await onboarding.tellBusinessAddress(true)
        expect((await onboarding.verifyOnboardingSuccessfull(false)) === true)
    });
});
test('Failed onboarding flow - industry Travel && totalTurnover < 20000000', async ({
                                                               pageObjects: { onboarding },
                                                               startOnboarding: page,
                                                           }) => {

    const random_email = generateData.createRandomEmail()

    await onboarding.tellAboutYourself(random_email)
    expect((await onboarding.getH2Text()).includes('please introduce us to your business'))
    await onboarding.tellAboutYourBusiness('Airlines')
    expect((await onboarding.getH2Text()).includes('Business details'))
    await onboarding.tellBusinessDetailsInStore('mid')
    await onboarding.tellBusinessService()
    await onboarding.tellBusinessAddress(true)
    expect((await onboarding.verifyOnboardingSuccessfull(false)) === true)
});

test('Failed onboarding flow - SalesMethod = Online && OnlineRevenue < 250000 && SubIndustryType !=  ' +
    'Hairdressers && SalesMetod != Shopify/BigCommerce', async ({
                                                                            pageObjects: { onboarding },
                                                                            startOnboarding: page,
                                                                         }) => {

    const random_email = generateData.createRandomEmail()

    await onboarding.tellAboutYourself(random_email)
    expect((await onboarding.getH2Text()).includes('please introduce us to your business'))
    await onboarding.tellAboutYourBusiness('Tutoring')
    expect((await onboarding.getH2Text()).includes('Business details'))
    await onboarding.tellBusinessDetailsOnlineAndBoth('online', 'magento');
    await onboarding.tellBusinessService('magento')
    await onboarding.tellBusinessAddress(true)
    expect((await onboarding.verifyOnboardingSuccessfull(false)) === true)
});