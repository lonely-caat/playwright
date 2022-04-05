const CustomerSearchPage = (page) => {
    const elements = {
        header: 'h1',
        searchInput: '#searchCustomerInput',
        searchButton: '#submitSearchCustomer',
        refundedOrder: '#customerInfo >> text=Refunded',
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

    async function clickSearchResult(customer_name='test APPROVETEST') {
        await page.click(`text=${customer_name}`)

    }

    async function searchCustomer(customer_name='test APPROVETEST') {
        const { searchInput,  searchButton } = elements;
        await page.fill(searchInput, customer_name)
        await page.click(searchButton);
    }

    async function navigateRefundedOrder() {
        const { refundedOrder } = elements;
        await page.click(refundedOrder)
    }

    return {
        getHeaderText,
        getSuccessMessage,
        clickSearchResult,
        searchCustomer,
        navigateRefundedOrder,
    };
};

module.exports = CustomerSearchPage;

