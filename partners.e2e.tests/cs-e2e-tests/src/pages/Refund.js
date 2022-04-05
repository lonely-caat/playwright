const { expect } = require('@playwright/test');



const RefundsPage = (page) => {
    const elements = {
        header: 'h1',
        payNow: 'button:has-text("Pay now")',
        enterPayment: 'text=$Confirm payment >> input[type="number"]',
        confirmPayment: 'button:has-text("Confirm payment")',
        anyRefund: 'button:has-text("Refund")',
        confirmRefund: 'mat-dialog-container[role="dialog"] button:has-text("Refund")',
        failedRefundBasicError: 'text=Failed to refund',
        successMessage: 'text=Payment successfully refunded'

    };


    async function getHeaderText() {
        const { header } = elements;
        return page.innerText(header);
    }

    async function createPayment(payment_amount='40') {
        const { payNow,  enterPayment, confirmPayment} = elements;
        await page.click(payNow);
        await page.waitForSelector(enterPayment);
        await page.fill(enterPayment, payment_amount)
        await page.click(confirmPayment);

    }

    async function performRefundAny() {
        const { anyRefund,  confirmRefund, failedRefundBasicError } = elements;

        await page.click(anyRefund);
        await page.click(confirmRefund);
        await page.waitForTimeout(2000);
        const error = await page.isVisible(failedRefundBasicError);
        expect(error).toBeFalsy();
    }

    async function verifyRefundSuccess() {
        const { successMessage } = elements;
        const success = await page.isVisible(successMessage);
        expect(success).toBeTruthy();
    }

    return {
        getHeaderText,
        createPayment,
        performRefundAny,
        verifyRefundSuccess,
    };
};

module.exports = RefundsPage;

