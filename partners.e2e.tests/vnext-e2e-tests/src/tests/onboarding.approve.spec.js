const { expect } = require('@playwright/test');
const test = require('../fixtures/fixtures');
const generateData = require('../helpers/generateData');




const { describe } = test;

describe('Approved onboarding flow tests', () => {
    test('Approved onboarding flow', async ({
                                       pageObjects: { dashboard, onboarding },
                                       startOnboarding: page,
                                   }) => {
        const random_email = generateData.createRandomEmail()

        await onboarding.tellAboutYourself(random_email)
        expect((await onboarding.getH2Text()).includes('please introduce us to your business'))
        await onboarding.tellAboutYourBusiness()
        expect((await onboarding.getH2Text()).includes('Business details'))
        await onboarding.tellBusinessDetailsOnlineAndBoth()
        await onboarding.tellBusinessService()
        await onboarding.tellBusinessAddress(true)
        expect((await onboarding.verifyOnboardingSuccessfull(true)).includes('Great, your business is eligible for Zip'))
    });
});
test('Approved onboarding flow business details - online', async ({
                                             pageObjects: { dashboard, onboarding },
                                             startOnboarding: page,
                                         }) => {
    const random_email = generateData.createRandomEmail()

    await onboarding.tellAboutYourself(random_email)
    expect((await onboarding.getH2Text()).includes('please introduce us to your business'))
    await onboarding.tellAboutYourBusiness()
    expect((await onboarding.getH2Text()).includes('Business details'))
    await onboarding.tellBusinessDetailsOnlineAndBoth('online')
    await onboarding.tellBusinessService()
    await onboarding.tellBusinessAddress(true)
    expect((await onboarding.verifyOnboardingSuccessfull(true)).includes('Great, your business is eligible for Zip'))
});

test('Approved onboarding flow business details - business service', async ({
                                                                      pageObjects: { dashboard, onboarding },
                                                                      startOnboarding: page,
                                                                  }) => {
    const random_email = generateData.createRandomEmail()

    await onboarding.tellAboutYourself(random_email)
    expect((await onboarding.getH2Text()).includes('please introduce us to your business'))
    await onboarding.tellAboutYourBusiness()
    expect((await onboarding.getH2Text()).includes('Business details'))
    await onboarding.tellBusinessDetailsOnlineAndBoth('online')
    await onboarding.tellBusinessService(true)
    await onboarding.tellBusinessAddress(true)
    expect((await onboarding.verifyOnboardingSuccessfull(true)).includes('Great, your business is eligible for Zip'))
});

test('Approved onboarding flow business details - instore', async ({
                                             pageObjects: { dashboard, onboarding },
                                             startOnboarding: page,
                                         }) => {
    const random_email = generateData.createRandomEmail()

    await onboarding.tellAboutYourself(random_email)
    expect((await onboarding.getH2Text()).includes('please introduce us to your business'))
    await onboarding.tellAboutYourBusiness()
    expect((await onboarding.getH2Text()).includes('Business details'))
    await onboarding.tellBusinessDetailsInStore()
    await onboarding.tellBusinessService(true)
    await onboarding.tellBusinessAddress(true)
    expect((await onboarding.verifyOnboardingSuccessfull(true)).includes('Great, your business is eligible for Zip'))
});