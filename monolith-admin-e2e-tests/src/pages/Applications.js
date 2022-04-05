const {byTagAndText} = require("../helpers/selectors");
const { expect } = require('@playwright/test');



const Applications = (page) => {
    const elements = {
        header: 'h3',
        searchInput: '#SearchValue',
        searchButton: '#search',
        searchDropDown: '#s2id_applicationSearchTypeList',
        searchStateDropDown: '#s2id_SearchStateType',
        table: '#customerList',

        newSearchSelector: '//a[text()="New Application Search"]',
        oldSearchSelector: '//a[text()="Application Search"]',
        searchStartDate: '#SearchStartDate',
        searchEndDate: '#SearchEndDate',
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


    async function switchApplicationType(switchToOld=true) {
        // switcher between old and new search types
        const { newSearchSelector, oldSearchSelector }  = elements;
        switchToOld ? await page.click(oldSearchSelector) : await page.click(newSearchSelector)
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


    async function searchApplications(query, filter='optionNoFilter'){
        const { searchInput, searchButton, searchDropDown } = elements;

        await page.click(searchDropDown);
        await selectFilterOption(filter);
        await page.fill(searchInput, query);
        await page.click(searchButton);
    }

    async function searchApplicationsLegacy(filter='optionCreditProfileState', stateFilter='optionTypesReferOne' ) {
        const { searchButton, searchDropDown, searchStateDropDown  } = elements;

        await page.click(searchDropDown);
        await selectFilterOption(filter);
        await page.click(searchStateDropDown);
        await selectFilterOption(stateFilter);
        await page.click(searchButton);
    }

    async function returnTableElements(){
        const raw_rows = await page.$$eval('css=#applicationList >> td', (nodes) =>
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
        let raw_rows = await page.$$eval('xpath=//td', (nodes) =>
            nodes.map(n => n.textContent.trim()))
        raw_rows = raw_rows.join().replace( /\s\s+|\n/g, ' ' );
        return raw_rows
    }



    return {
        clickDetailsButton,
        searchApplications,
        returnTableElements,
        returnDetailTablesElements,
        switchApplicationType,
        setSearchDate,
        searchApplicationsLegacy

    };
};

module.exports = Applications;
