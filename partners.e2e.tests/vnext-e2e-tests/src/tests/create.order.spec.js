const { expect } = require('@playwright/test');
const test = require('../fixtures/fixtures');
const createPin = require('../utils/createPin')


const { describe } = test;


describe('Create order tests', () => {
    test('Create order with instore code', async ({
                                                              pageObjects: { dashboard, createOrder },
                                                              signInVnext: page,
                                                          }) => {
        await page.waitForURL('/login');
        await dashboard.selectSection('sectionDashboard')
        await dashboard.selectProduct('createOrder');
        expect((await dashboard.getHeaderText()) === 'New order');
        const pincode = createPin.CreatePinZipMoney()
        await createOrder.createOrderWPin(pincode)
        await createOrder.getSuccessMessage()
    });

    test('Create order without instore code', async ({
                                                      pageObjects: { dashboard, createOrder },
                                                      signInVnext: page,
                                                  }) => {
        await page.waitForURL('/login');
        await dashboard.selectSection('sectionDashboard')
        await dashboard.selectProduct('createOrder');
        expect((await dashboard.getHeaderText()) === 'New order');
        await createOrder.createOrderWOPin()
        await createOrder.getSuccessMessage()
    });
});
