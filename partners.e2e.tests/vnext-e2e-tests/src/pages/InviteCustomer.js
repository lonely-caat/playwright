const InviteCustomerPage = (page) => {
    const elements = {
        header: 'h1',
        branchSelector: '#mui-component-select-branch',
        sydneyDropDownOption: 'text=SydneyTick',
        firstName: 'input[name="firstName"]',
        lastName: 'input[name="lastName"]',
        email: 'input[name="emailAddress"]',
        phone: 'input[name="mobilePhone"]',
        submitButton: 'button:has-text("Send invite")',
        successPopUp: 'text=Invite sent successfully',
        failurePopUp: 'text=Error sending invite',

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

    async function inviteCustomer(fname='test name Jr.', lname="Smith",
                                  email_address='bastesting@zipteam222898.testinator.com', mobile='0400000000') {
        const { branchSelector, sydneyDropDownOption, firstName, lastName, email, phone, submitButton } = elements;

        await page.click(branchSelector);
        await page.click(sydneyDropDownOption);
        await page.fill(firstName, fname);
        await page.fill(lastName, lname);
        await page.fill(email, email_address);
        await page.fill(phone, mobile)
        await page.click(submitButton)


    }


    return {
        getHeaderText,
        getSuccessMessage,
        inviteCustomer,
    };
};

module.exports = InviteCustomerPage;

