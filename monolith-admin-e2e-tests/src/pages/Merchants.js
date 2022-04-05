const data = require('../constants/zipCreditData')


const Merchants = (page) => {
    const elements = {
        header: 'h3',
        searchInput: '#merchant-index-search-input',
        searchButton: '#search',
        countryDropDown: '#s2id_merchant-index-country-id',

        countryDropDownCompanies: '#s2id_company-index-country-id',
        SearchInputCompanies: '#company-index-search-input',


        merchantsSelector: '//a[text()="Merchants"]',
        companiesSelector: '//a[text()="Companies"]',
        promotionsSelector: '//a[text()="Promotions"]',
        rolesSelector: '//a[text()="Roles"]',
    };

    async function selectMerchantsSubTabs(tab='merchants') {
        const { merchantsSelector, companiesSelector, promotionsSelector, rolesSelector }  = elements;
        switch (tab) {
            case "merchants":
                await page.click(merchantsSelector);
                break;
            case "companies":
                await page.click(companiesSelector);
                break
            case "promotions":
                await page.click(promotionsSelector);
                break;
            case "roles":
                await page.click(rolesSelector);
                break;
            default:
                console.error('Invalid tab!')
                break;

        };
    };

    async function searchMerchants(country='AU', query=data.zipCredit.merchant){
        const { countryDropDown, searchInput, searchButton } = elements;
        await page.click(countryDropDown)
        'AU, NZ'.includes(country) ? await page.click(`//div[text()="${country}"]`) : console.log('Invalid country!');
        await page.fill(searchInput, query)
        await page.click(searchButton)
    };

    async function searchCompanies(country='AU', query=data.zipCredit.company){
        const { countryDropDownCompanies, SearchInputCompanies, searchButton } = elements;
        await page.click(countryDropDownCompanies)
        'AU, NZ'.includes(country) ? await page.click(`//div[text()="${country}"]`) : console.log('Invalid country!');
        await page.fill(SearchInputCompanies, query)
        await page.click(searchButton)
    };

    async function returnTableElements(){
        const raw_rows = await page.$$eval('css=#applicationList >> td', (nodes) =>
            // get text content of each td and trim out all line breaks
            nodes.map(n => n.textContent.trim())
        );
        return raw_rows;
    };

    async function clickDetailsButton(index='1'){
        await page.click(`//tr[@class="odd"][${index}]//a[contains(text(), "Details")]`)
    }

    async function returnMerchantsDetailTablesElements(){
        await page.waitForTimeout(1000);
        await page.$$('//li')
        let raw_rows = await page.$$eval('xpath=//li', (nodes) =>
            nodes.map(n => n.textContent.trim()))
        // console.dir(raw_rows, {'maxArrayLength': null})
        raw_rows = raw_rows.join().replace( /\s\s+|\n/g, ' ' );
        return raw_rows
    };



    return {
        clickDetailsButton,
        returnTableElements,
        returnMerchantsDetailTablesElements,
        searchMerchants,
        selectMerchantsSubTabs,
        searchCompanies

    };
};

module.exports = Merchants;
