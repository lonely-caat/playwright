const {byTagAndText} = require("../helpers/selectors");
const { expect } = require('@playwright/test');



const RepaymentCalculatorPage = (page) => {
    const elements = {
        header: 'h1',
        price: '#price',
        period: '#interestFreePeriodSelect',
        details: 'text=More details',
        monthlyPayment: 'button[role="tab"]:has-text("Monthly")',
        weeklyPayment: 'button[role="tab"]:has-text("Weekly")',
        repaymentsTitle: '//zip-content-heading',
        priceInput: '//input[@placeholder="Enter price"]',
        monthlyRepayment: '//h4[contains(text(), "Equal")]/following-sibling::p[1]',
        establishmentFee: '//h4[contains(text(), "Establishment fee")]/following-sibling::p[1]',
        monthlyAccountFee: '//h4[contains(text(), "Monthly account fee")]/following-sibling::p[1]',
        sendOrderButton: 'button:has-text("Send order")',
        cancel: 'text=Cancel',
        // interestFreePayments : '//h4[contains(text(), "interest-free repayments")]/following-sibling::p[1]',


    };


    async function getHeaderText() {
        const { header } = elements;
        return page.innerText(header);
    }

    async function returnPayment() {
        const { monthlyRepayment } = elements;
        return page.innerText(monthlyRepayment);
    }
    async function returnEstablishmentFee() {
        const { establishmentFee } = elements;
        return page.innerText(establishmentFee);
    }
    async function returnAccountFee() {
        const { monthlyAccountFee } = elements;
        return page.innerText(monthlyAccountFee);
    }

    async function populateCalendar(item_price = "10", period = "12") {
        await page.fill(elements['price'], item_price)
        await page.waitForTimeout(2000)
        await page.click(elements['period'])
        await page.waitForTimeout(1000)
        await page.click(byTagAndText('li', '12 Months Interest Free A'))
    }


    return {
        getHeaderText,
        populateCalendar,
        returnPayment,
        returnEstablishmentFee,
        returnAccountFee

    };
};

module.exports = RepaymentCalculatorPage;

