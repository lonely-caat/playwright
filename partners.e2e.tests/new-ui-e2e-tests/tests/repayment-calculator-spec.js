import dotenv from 'dotenv-safe';
import newDashHomePage from '../pages/new-dash-home-page';
import loginPage from '../pages/new-dash-signin-page';
import * as constants from '../data/constants';


dotenv.config();


fixture('Partners can operate the repayment calculator')
    .page(process.env.NEW_DASH_PAGE)

test('verify below limit error is present',async (t) => {
        await loginPage.signIn(constants.newDash.merchant3.merchantName, constants.newDash.merchant3.merchantPassword);
        await newDashHomePage.dashboardTab();
        await newDashHomePage.repaymentCalculatorTab();
        await newDashHomePage.fillRepayment(1000)
        await t.expect(newDashHomePage.repaymentCalculatorOverLimit.textContent).contains("Your merchant account is " +
            "not able to offer an account with a high enough limit to facilitate this purchase amount.")
    },
);
test('verify above limit error is present',async (t) => {
        await loginPage.signIn(constants.newDash.merchant1.merchantName, constants.newDash.merchant1.merchantPassword);
        await newDashHomePage.dashboardTab();
        await newDashHomePage.repaymentCalculatorTab();
        await newDashHomePage.fillRepayment(1000000, 'Core Product')
        await t.expect(newDashHomePage.repaymentCalculatorPriceError().textContent).contains("Maximum price is $2,000.00")
},
);
test('verify valid data, below Min payment',async (t) => {
            await loginPage.signIn(constants.newDash.merchant4.merchantName, constants.newDash.merchant4.merchantPassword);
            await newDashHomePage.dashboardTab();
            await newDashHomePage.repaymentCalculatorTab();
            await newDashHomePage.fillRepayment(25, '12 Months Interest Free');
            await t.expect(newDashHomePage.repaymentCalculatorEqualRepaymentValue.textContent).contains("$60.00");
            await t.expect(newDashHomePage.repaymentCalculatorMonthlyMinimumValue.textContent).contains("$60.00");
            await t.expect(newDashHomePage.repaymentCalculatorEstablishmentFeeValue.textContent).contains("$0.00");
            await t.expect(newDashHomePage.repaymentCalculatorMonthlyFeeValue.textContent).contains("$4.95")
    },
);
test('verify valid data',async (t) => {
        await loginPage.signIn(constants.newDash.merchant4.merchantName, constants.newDash.merchant4.merchantPassword);
        await newDashHomePage.dashboardTab();
        await newDashHomePage.repaymentCalculatorTab();
        await newDashHomePage.fillRepayment(1500, '12 Months Interest Free');
        await t.expect(newDashHomePage.repaymentCalculatorEqualRepaymentValue.textContent).contains("$154.87");
        await t.expect(newDashHomePage.repaymentCalculatorMonthlyMinimumValue.textContent).contains("$60.00");
        await t.expect(newDashHomePage.repaymentCalculatorEstablishmentFeeValue.textContent).contains("$299.00");
        await t.expect(newDashHomePage.repaymentCalculatorMonthlyFeeValue.textContent).contains("$4.95")
    },
);
test('verify valid data, max decimal',async (t) => {
            await loginPage.signIn(constants.newDash.merchant4.merchantName, constants.newDash.merchant4.merchantPassword);
            await newDashHomePage.dashboardTab();
            await newDashHomePage.repaymentCalculatorTab();
            await newDashHomePage.fillRepayment("9999.99", '12 Months Interest Free');
            await t.expect(newDashHomePage.repaymentCalculatorEqualRepaymentValue.textContent).contains("$863.20");
            await t.expect(newDashHomePage.repaymentCalculatorMonthlyMinimumValue.textContent).contains("$308.97");
            await t.expect(newDashHomePage.repaymentCalculatorEstablishmentFeeValue.textContent).contains("$299.00");
            await t.expect(newDashHomePage.repaymentCalculatorMonthlyFeeValue.textContent).contains("$4.95")
    },
);
