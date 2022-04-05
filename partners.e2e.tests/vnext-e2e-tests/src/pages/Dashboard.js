const { byTagAndText } = require('../helpers/selectors')

const Dashboard = (page) => {
    const elements = {
        header: 'h1',
        createOrder: 'Create order',
        inviteCustomer: 'Invite customer',
        repaymentCalculator: 'Repayment calculator',
        orderSearch: 'Order search',
        customerSearch: 'Customer search',
        settings: 'Settings',
        notifications: '#notificationsTile',
        integration: '#integrationTile',
        userManagement: '#userManagementTile',
        disbursements: '#disbursementTile',
        posMaterials: '#posMaterialsTile',

    };

    const sections = {
        sectionDashboard: 'Dashboard',
        sectionOrders: 'Orders',
        sectionCustomers: 'Customers',
        sectionSettings: 'Settings',
        sectionProfile: 'Profile',
    }

    async function selectSection(section, tag= 'span') {
        if (Object.keys(sections).includes(section)) {
            await page.click(byTagAndText(tag, sections[section]))}

        else {console.log('Invalid Section!');
                return 'Invalid Section!'}
    }

    async function selectProduct(product) {
        await page.waitForTimeout(2000)
        if (Object.keys(elements).includes(product)) {
            await page.click(byTagAndText('h2', elements[product]))}
        // await page.waitForTimeout(5000)
        // await page.click(byTagAndText('h2', 'Create order'))}

        else {
            console.log('Invalid product!');
            return 'Invalid product!'}
    }

    async function getHeaderText() {
        const { header } = elements;
        return page.innerText(header);
    }

    return {
        selectProduct,
        getHeaderText,
        selectSection,
    };
};

module.exports = Dashboard;
