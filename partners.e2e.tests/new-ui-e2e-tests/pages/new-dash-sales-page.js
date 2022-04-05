/* eslint-disable class-methods-use-this */
import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';
import helper from "../utils/api-helper";
import * as constants from "../data/constants";

dotenv.config();

const getElementsByXPath = Selector(xpath => {
    const iterator = document.evaluate(xpath, document, null, XPathResult.UNORDERED_NODE_ITERATOR_TYPE, null);
    const items = [];

    let item = iterator.iterateNext();

    while (item) {
        items.push(item);
        item = iterator.iterateNext();
    }

    return items;
});

class NewDashSalesPage {
    constructor() {
        this.orderDate = Selector("h4")
            .withText('Order date')
            .sibling('p');
        this.branch = Selector("h4")
            .withText('Branch')
            .sibling('p');
        this.salesRep = Selector("h4")
            .withText('Sales rep')
            .sibling('p');
        this.amountOutstanding = Selector("h4")
            .withText('Amount outstanding')
            .sibling('p');
        this.amountPaid = Selector("h4")
            .withText('Amount paid')
            .sibling('p');
        this.amountRefunded = Selector("h4")
            .withText('Amount refunded')
            .sibling('p');
        this.editReferenceNumber = Selector('h4')
            .withText('Order reference')
            .sibling('button');
        // needs ID baaadly
        this.referenceInput = Selector('fieldset')
            .nth(-1);
        this.referenceUpdate = Selector('span')
            .withText('Update')
            .parent('button')

        this.referenceValueFirst = Selector('h4')
            .withText('Order reference')
            .parent('div')
        this.referenceValue = this.referenceValueFirst
            .sibling('p')

        this.order_success = getElementsByXPath('//div[contains(text(), "Order processed successfully")]')
        this.refund_overfill = getElementsByXPath('//div[contains(text(), "An error occurred processing the request: Refund total greater than capture total")]')
        this.refund_success = getElementsByXPath('//div[contains(text(), "Refund was processed successfully")]')
        this.table_elements = getElementsByXPath('//table//tr')
    }

    async clickTableElementByIndex(index) {
        const row = Selector('table tr')
            .nth(index);
        await t.click(row)
        await t.wait(3000)
    }

    async getDetailsDataByField(field) {
        const headers = Selector('h4')
        for (element of headers){element === field ?  text = result: text = "not found"}
        return text
    }

    async salesSubTab() {
        const sales_tab = getElementsByXPath('//span[contains(text(), "All sales")]')
        await t.click(sales_tab);
    }
    async authorizedSubTab() {
        const authorized_sub_tab = getElementsByXPath('//span[contains(text(), "Authorised")]')
        await t.click(authorized_sub_tab);
        await t.wait(3000)
    }
    async cancelledSubTab() {
        const cancelled_sub_tab = getElementsByXPath('//span[contains(text(), "Canceled")]')
        await t.click(cancelled_sub_tab);
        await t.wait(3000)
    }
    async completedSubTab() {
        const completed_sub_tab = getElementsByXPath('//span[contains(text(), "Completed")]')
        await t.click(completed_sub_tab);
        await t.wait(3000)
    }
    async contractPendingSubTab() {
        const contract_pending_sub_tab = getElementsByXPath('//span[contains(text(), "Contract pending")]')
        await t.click(contract_pending_sub_tab);
        await t.wait(3000)
    }
    async declinedSubTab() {
        const declined_sub_tab = getElementsByXPath('//span[contains(text(), "Declined")]')
        await t.click(declined_sub_tab);
        await t.wait(3000)
    }
    async depositRequiredSubTab() {
        const deposit_required_sub_tab = getElementsByXPath('//span[contains(text(), "Deposit required")]')
        await t.click(deposit_required_sub_tab);
        await t.wait(3000)
    }
    async inProgressSubTab() {
        const in_progress_sub_tab = getElementsByXPath('//span[contains(text(), "In progress")]')
        await t.click(in_progress_sub_tab);
        await t.wait(3000)
    }
    async partiallyCapturedSubTab() {
        const partially_captured_sub_tab = getElementsByXPath('//span[contains(text(), "Partially captured")]')
        await t.click(partially_captured_sub_tab);
        await t.wait(3000)
    }
    async refundedSubTab() {
        const refunded_sub_tab = getElementsByXPath('//span[contains(text(), "Refunded")]')
        await t.click(refunded_sub_tab);
        await t.wait(3000)
    }
    async underReviewSubTab() {
        const under_review_sub_tab = getElementsByXPath('//span[contains(text(), "Under review")]')
        await t.click(under_review_sub_tab);
        await t.wait(3000)
    }

    async searchOrders(query) {
        const searchInput = getElementsByXPath('//input[@placeholder="Search..."]')
        await t.typeText(searchInput, query)
        await t.pressKey('enter')
    }

    async selectSearchType(search_type) {
        const searchBy = getElementsByXPath('//div[contains(text(), "No filter")]')
        await t.click(searchBy)
        search_type = search_type.toLowerCase()
        switch (search_type) {
            case 'orderid':
                t.click(getElementsByXPath(getElementsByXPath('//li[contains(text(), "Order Id")]')))
                break
            case 'reference':
                t.click(getElementsByXPath('//li[contains(text(), "Reference")]'))
                break
            case 'receipt':
                t.click(getElementsByXPath('//li[contains(text(), "Zip Receipt number")]'))
                break
            case 'full_name':
                t.click(getElementsByXPath('//li[contains(text(), "Full name")]'))
                break
            case 'first_name':
                t.click(getElementsByXPath('//li[contains(text(), "First name")]'))
                break
            case 'last_name':
                t.click(getElementsByXPath('//li[contains(text(), "Last name")]'))
                break
            case 'account':
                t.click(getElementsByXPath('//li[contains(text(), "Customer account #")]'))
        }
    }
    async newOrder(branch, instorePin, price="1", userEmail="zippay.email.login@mailinator.com") {
        const code = await this.getInstorePin(userEmail);
        await t.click(getElementsByXPath('//span[contains(text(), "Search sale")]'))
        await t.click(getElementsByXPath('//button[contains(text(), "New order")]'))
        // TODO add branch logic after an unique identifier is added to the field
        // if (branch){await t.click()}
        await t.typeText('div.MuiFormControl-root:nth-child(3) > div:nth-child(2) > input:nth-child(1)', code)
        await t.typeText('div.css-1ago99h:nth-child(2) > div:nth-child(2) > input:nth-child(1)', price)
        await t.click(getElementsByXPath('//span[contains(text(), "Create order")]'))
        await t.wait(3000)
    }
    async getInstorePin(userEmail) {
        const response = await helper.createInstorePin("ZipCredit", userEmail);
        return response.text;
    }
    async completeOrder(amount=1) {
        await t.click(getElementsByXPath('//span[contains(text(), "Action")]'))
        await t.click(getElementsByXPath('//li[contains(text(), "Complete")]'))
        await t.typeText(amount)
        await t.click(getElementsByXPath('//span[contains(text(), "Complete order")]'))
    }
    async refundOrder(amount) {
        await t.click(getElementsByXPath('//span[contains(text(), "Action")]'))
        await t.click(getElementsByXPath('//li[contains(text(), "Refund")]'))
        if (amount){await t.typeText(Selector("#refundAmount"),amount), { replace: true }}
        await t.click(getElementsByXPath('//span[contains(text(), "Complete order")]'))
    }
    async cancelOrder() {
        await t.click(getElementsByXPath('//span[contains(text(), "Action")]'))
        await t.click(getElementsByXPath('//li[contains(text(), "Cancel")]'))
        await t.click(getElementsByXPath('//span[contains(text(), "Cancel order")]'))
    }

    async editReference(newValue) {
        await t.click(this.editReferenceNumber)
        await t.typeText(this.referenceInput, newValue, {replace: true});
        await t.click(this.referenceUpdate)
    }
}

export default new NewDashSalesPage();
