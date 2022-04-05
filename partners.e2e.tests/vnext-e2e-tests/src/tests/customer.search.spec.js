const { expect } = require('@playwright/test');
const test = require('../fixtures/fixtures');
const createPin = require('../utils/createPin')


const { describe } = test;

//TODO add email client verification
describe('Invite customer tests', () => {
    test('Invite customer', async ({
                                       pageObjects: { dashboard, customerSearch },
                                                  }) => {
        // await page.waitForURL('/login');
        await dashboard.selectSection('sectionDashboard')
        await dashboard.selectProduct('customerSearch');
        expect((await dashboard.getHeaderText()) === 'Customers');
        await customerSearch.searchCustomer()
        await customerSearch.clickSearchResult()
        await customerSearch.navigateRefundedOrder()
        await page.waitForURL('/orders');
    });
});
