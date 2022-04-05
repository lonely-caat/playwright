const {byTagAndText} = require("../helpers/selectors");
const { expect } = require('@playwright/test');



const Transactions = (page) => {
    const elements = {
        header: 'h3',
        searchInput: '#SearchValue',
        searchButton: '#search',
        searchDropDown: '#s2id_orderSearchTypeList',

        searchDropDownLegacy: '#s2id_transactionSearchTypeList',

        searchStateDropDown: '#s2id_SearchStateType',
        table: '#customerList',

        newTransactionsSelector: '//a[text()="New Transactions"]',
        oldTransactionsSelector: '//a[text()="Transactions"]',
        searchStartDate: '#SearchStartDate',
        searchEndDate: '#SearchEndDate',

        authorizedAmount: '//label[text()="Authorised Amount"]/following-sibling::span',
        capturedAmount: '//label[text()="Captured Amount"]/following-sibling::span',
        refundedAmount: '//label[text()="Refunded Amount"]/following-sibling::span',
        totalFeeAmount: '//label[text()="Total Merchant Fee Amount"]/following-sibling::span',
        disbursedAmount: '//label[text()="Disbursed Amount"]/following-sibling::span'
    };


    const filterOptions = {
        optionAccountNumber: 'AccountNumber',
        optionLastName: 'LastName',
        optionEmailAddress: 'EmailAddress',
        optionMobileNumber: 'MobileNumber',
        optionConsumerId: 'ConsumerId',
        optionNoFilter: 'NoFilter',

        optionCreditProfileState: 'CreditProfileState',

        optionTypesReferOne: 'Refer1',
        optionTypesActive: 'Active',
        optionTypesApproved: 'Approved',
        optionTypesDeclined: 'Declined',
        optionTypesInactive: 'InActive',
        optionTypesExpired: 'Expired',
        optionTypesCancelled: 'Cancelled',
        optionTypesVerify: 'Verify',
    }


    async function switchTransactionType(switchToOld=true) {
        // switcher between old and new search types
        const { newTransactionsSelector, oldTransactionsSelector }  = elements;
        switchToOld ? await page.click(oldTransactionsSelector) : await page.click(newTransactionsSelector)
    }

    async function setSearchDate(startDate='2021-11-18', endDate='2021-11-23') {
        const {searchStartDate, searchEndDate} = elements;
        await page.waitForSelector(searchStartDate)
        const startDateAttribute = await page.$(searchStartDate);
        await startDateAttribute.evaluate((node, startDate) => node.setAttribute('value', startDate), startDate)
        const endDateAttribute = await page.$(searchEndDate);
        await endDateAttribute.evaluate((node, endDate) => node.setAttribute('value', endDate), endDate)
    }

        async function selectFilterOption(filterOption, tag= 'div') {
        if (Object.keys(filterOptions).includes(filterOption)) {
            await page.click(byTagAndText(tag, filterOptions[filterOption]))}

        else {console.log('Invalid Section!');
            return 'Invalid Section!'}
    }


    async function searchTransactions(query, filter='optionNoFilter'){
        const { searchInput, searchButton, searchDropDown } = elements;

        await page.click(searchDropDown);
        await selectFilterOption(filter);
        await page.fill(searchInput, query);
        await page.click(searchButton);
    }

    async function searchTransactionsLegacy(query, filter='NoFilter' ) {
        const { searchButton, searchDropDownLegacy, searchInput } = elements;


        // filter ==='NoFilter' ? console.log('not touching that cursed dropdown') : await page.click(searchDropDownLegacy); await page.click(`//div[text()="${filter}"]`)
        await page.click(searchDropDownLegacy);
        await page.click(`//div[text()="${filter}"]`)
        await page.fill(searchInput, query);
        await page.click(searchButton);
        await page.waitForLoadState('domcontentloaded' );

    }

    async function returnTableElements(){
        const raw_rows = await page.$$eval('css=#transactionList >> td', (nodes) =>
            // get text content of each td and trim out all line breaks
            nodes.map(n => n.textContent.trim())
        );
        return raw_rows;
    }

    async function clickDetailsButton(index='1'){
        await page.click(`//tr[@class="odd"][${index}]//a[contains(text(), "Details")]`)
    }

    async function returnDetailTablesElements(){
        await page.waitForTimeout(1000);
        await page.$$('//td')
        let raw_rows = await page.$$eval('xpath=//td|//span', (nodes) =>
            nodes.map(n => n.textContent.trim()))
        raw_rows = raw_rows.join().replace( /\s\s+|\n/g, ' ' );
        return raw_rows
    }

    async function returnTotalFeeAmount(){
        const {  authorizedAmount,capturedAmount,refundedAmount,totalFeeAmount, disbursedAmount } = elements;
        return page.innerText(totalFeeAmount)
    }

    async function returnCapturedAmount(){
        const {  capturedAmount } = elements;
        return page.innerText(capturedAmount)
    }

    async function returnRefundedAmount(){
        const {  refundedAmount } = elements;
        return page.innerText(refundedAmount)
    }

    async function returnAuthorizedAmount(){
        const {  authorizedAmount } = elements;
        return page.innerText(authorizedAmount)
    }

    async function returnDisbursedAmount(){
        const {  disbursedAmount } = elements;
        return page.innerText(disbursedAmount)
    }


    return {
        clickDetailsButton,
        searchTransactions,
        returnTableElements,
        returnDetailTablesElements,
        switchTransactionType,
        setSearchDate,
        searchTransactionsLegacy,
        returnTotalFeeAmount,
        returnCapturedAmount,
        returnRefundedAmount,
        returnAuthorizedAmount,
        returnDisbursedAmount,
    };
};

module.exports = Transactions;
