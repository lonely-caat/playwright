const CreateOrderPage = (page) => {
    const elements = {
        header: 'h1',
        instoreCode: '#instoreCode',
        branchSelector: '#mui-component-select-branch',
        sydneyDropDownOption: 'text=SydneyTick',
        noCodeCheckBox: 'input[type="checkbox"]',
        lastName: '#lastName',
        email: '#email',
        mobile: '#mobile',
        price: '#price',
        reference: '#reference',
        submitButton: '#sendOrderButton',
        successPopUp: 'text=Order sent successfully',
        failurePopUp: 'text=Error sending order',

    };

    async function getHeaderText() {
        const { header } = elements;
        return page.innerText(header);
    }

    async function getSuccessMessage() {
        const { message } = elements;
        const success = await page.isVisible(message)
        return success
    }

    async function createOrderWPin(pin, itemPrice=10, referenceValue='test') {
        const { branchSelector, sydneyDropDownOption, instoreCode, price,
                reference, submitButton } = elements;

        await page.click(branchSelector);
        await page.click(sydneyDropDownOption);
        await page.fill(instoreCode, pin)
        await page.fill(price, itemPrice)
        await page.fill(reference, referenceValue)
        await page.click(submitButton)
    }

    async function createOrderWOPin(itemPrice=10, referenceValue='test',
        clientName='bastest', clientEmail='bastest@zipteam222898.testinator.com', num='0400000000' ) {
        const { branchSelector, sydneyDropDownOption, email, lastName, mobile, price,
            reference, submitButton } = elements;

        await page.click(branchSelector);
        await page.click(sydneyDropDownOption);
        await page.fill(lastName, clientName);
        await page.fill(email, clientEmail);
        await page.fill(mobile, num)
        await page.fill(price, itemPrice)
        await page.fill(reference, referenceValue)
        await page.click(submitButton)
    }


    return {
        getHeaderText,
        getSuccessMessage,
        createOrderWPin,
        createOrderWOPin,
    };
};

module.exports = CreateOrderPage;

