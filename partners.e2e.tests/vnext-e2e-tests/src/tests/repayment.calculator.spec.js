const { expect } = require('@playwright/test');
const test = require('../fixtures/fixtures');
const { byTagAndText } = require('../helpers/selectors')


const { describe, beforeAll, afterAll } = test;

describe('Repayment calculator tests', () => {
    test('debug Repayment calculator - 12 Months', async ({
                                                        pageObjects: { dashboard, repaymentCalculator },
                                                        signInVnext: page,
                                                    }) => {
        await page.waitForURL('/login');
        // await page.click('//span[contains(text(),"Dashboard")]')
        await dashboard.selectSection('sectionDashboard')
        await dashboard.selectProduct('repaymentCalculator');
        expect((await dashboard.getHeaderText()) === 'Repayment calculator');
        await repaymentCalculator.populateCalendar()
        expect((await repaymentCalculator.returnPayment()) === '$40.00');
        expect((await repaymentCalculator.returnEstablishmentFee()) === '$40.00')
        expect((await repaymentCalculator.returnAccountFee()) === '$0.00')


    });
});
