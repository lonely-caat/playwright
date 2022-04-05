const { expect } = require('@playwright/test');
const test = require('../fixtures/fixtures');
const createPin = require('../utils/createPin')


const { describe } = test;

//TODO add email client verification
describe('Invite customer tests', () => {
    test('Invite customer', async ({
                                                      pageObjects: { dashboard, inviteCustomer },
                                                      signInVnext: page,
                                                  }) => {
        await page.waitForURL('/login');
        await dashboard.selectSection('sectionDashboard')
        await dashboard.selectProduct('inviteCustomer');
        expect((await dashboard.getHeaderText()) === 'Invite customer');
        await inviteCustomer.inviteCustomer()
        await inviteCustomer.getSuccessMessage()
    });
});
