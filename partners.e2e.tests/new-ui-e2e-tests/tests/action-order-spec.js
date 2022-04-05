// eslint-disable-next-line no-undef
import dotenv from 'dotenv-safe';
import newDashSalesPage from '../pages/new-dash-sales-page';
import newDashHomePage from '../pages/new-dash-home-page';
import loginPage from '../pages/new-dash-signin-page';
import * as constants from '../data/constants';
import mailHelper from '../utils/mail-helper'

dotenv.config();


fixture('Partners can submit orders through the new dash')
    .page(process.env.NEW_DASH_PAGE)

test('verify refund order email is sent',async (t) => {
        await loginPage.signIn(constants.newDash.merchant2.merchantName, constants.newDash.merchant2.merchantPassword);
        await newDashHomePage.salesTab();
        await newDashSalesPage.completedSubTab();
        await newDashSalesPage.clickTableElementByIndex(1)
        await newDashSalesPage.refundOrder()

        const mailContent = await mailHelper.getEmailDetails(
            constants.newDash.merchant2.merchantName.split('@')[0],
            `The status of your zipPay order has been updated to Refunded.`,
            30000,
        );
        await t.expect(mailContent).contains('To view this order,  click here to log in to your Merchant Dashboard.');
    },
);
test('verify complete order email is sent',async (t) => {
            await loginPage.signIn(constants.newDash.merchant2.merchantName, constants.newDash.merchant2.merchantPassword);
            await newDashHomePage.salesTab();
            await newDashSalesPage.authorizedSubTab();
            await newDashSalesPage.clickTableElementByIndex(1)
            await newDashSalesPage.completeOrder()

            const mailContent = await mailHelper.getEmailDetails(
                constants.newDash.merchant2.merchantName.split('@')[0],
                `The status of your zipPay order has been updated to Completed.`,
                30000,
            );
            await t.expect(mailContent).contains('To view this order,  click here to log in to your Merchant Dashboard.');
    },
);
test('verify cancel order email is sent',async (t) => {
            await loginPage.signIn(constants.newDash.merchant2.merchantName, constants.newDash.merchant2.merchantPassword);
            await newDashHomePage.salesTab();
            await newDashSalesPage.authorizedSubTab();
            await newDashSalesPage.clickTableElementByIndex(1)
            await newDashSalesPage.cancelOrder()

            const mailContent = await mailHelper.getEmailDetails(
                constants.newDash.merchant2.merchantName.split('@')[0],
                `The status of your zipPay order has been updated to Cancel.`,
                30000,
            );
            await t.expect(mailContent).contains('To view this order,  click here to log in to your Merchant Dashboard.');
    },
);
