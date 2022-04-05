const data = require('../constants/zipCreditData')


const UserManagement = (page) => {
    const elements = {
        header: 'h3',
        searchInput: '#search',
        saveButton: '#save-button',
        cancelButton: '#cancel-button',
        userList: '#list-container',
        containerHeader: '//b[contains(text(), "UserProfiles")]',
    };

    async function searchUsers(query= data.zipCredit.EmailAlternative){
        const { searchInput, containerHeader } = elements;
        await page.$$(containerHeader)
        await page.waitForTimeout(2000)
        await page.click(searchInput)
        await page.fill(searchInput, query)
    }

    async function cancelDetails(){
        const { cancelButton } = elements;
        await page.click(cancelButton)
    }

    async function returnTableElements(){
        const { userList } = elements;
        const raw_rows = await page.$$eval(`css=${userList}`, (nodes) =>
            // get text content of each td and trim out all line breaks
            nodes.map(n => n.textContent.trim())
        );
        return raw_rows;
    }

    async function clickDetailsButton(index='1'){
        await page.click(`//div[@id="list-container"]//div[@class="list-group"]//a[@class="list-group-item"][${index}]`)
    }

    async function returnUserRoles(){
        const raw_rows = await page.$$eval(`css=.selected-tag`, (nodes) =>
            // get text content of each td and trim out all line breaks
            nodes.map(n => n.textContent.trim())
        );
        return raw_rows;
    }

    return {
        clickDetailsButton,
        cancelDetails,
        returnTableElements,
        searchUsers,
        returnUserRoles
    };
};

module.exports = UserManagement;
