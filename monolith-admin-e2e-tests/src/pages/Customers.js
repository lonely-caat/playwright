const {byTagAndText} = require("../helpers/selectors");


const Customers = (page) => {
    const elements = {
        header: 'h3',
        searchInput: '#customer-index-search-input',
        searchButton: '#search',
        searchDropDown: '#s2id_SearchType',
        table: '#customerList',
    };


    const filterOptions = {
        optionAccountNumber: 'AccountNumber',
        optionLastName: 'LastName',
        optionEmailAddress: 'EmailAddress',
        optionMobileNumber: 'MobileNumber',
        optionConsumerId: 'ConsumerId',
        optionNoFilter: 'NoFilter',
    }

    async function selectFilterOption(filterOption, tag= 'div') {
        if (Object.keys(filterOptions).includes(filterOption)) {
            await page.click(byTagAndText(tag, filterOptions[filterOption]))}

        else {console.log('Invalid Section!');
            return 'Invalid Section!'}
    }

    async function searchCustomers(query, filter='optionNoFilter'){
        const { searchInput, searchButton, searchDropDown } = elements;
        await page.click(searchDropDown);
        await selectFilterOption(filter);
        // stupid pw does not work with 'fill' here
        await page.type(searchInput, query);
        await page.click(searchButton);
    }

    async function returnTableElements(){
        const raw_rows = await page.$$eval('css=#customerList >> td', (nodes) =>
            // get text content of each td and trim out all line breaks
            nodes.map(n => n.textContent.trim())
        );
        return raw_rows;
    };

    async function clickDetailsButton(index='1'){
        await page.click(`//tr[@class="odd"][${index}]//a[contains(text(), "Details")]`)
    };

    async function returnDetailTablesElements(){
        let raw_rows = await page.$$eval('xpath=//tr', (nodes) =>
            nodes.map(n => n.textContent.trim()))
        raw_rows = raw_rows.filter(row=>!row.includes('Due Date'))
        raw_rows = raw_rows.join().replace( /\s\s+|\n/g, ' ' );
        return raw_rows
    };

    return {
        clickDetailsButton,
        searchCustomers,
        returnTableElements,
        returnDetailTablesElements


    };
};

module.exports = Customers;
