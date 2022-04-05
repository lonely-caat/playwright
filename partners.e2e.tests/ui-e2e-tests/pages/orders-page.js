import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';
import * as constants from '../data/constants';

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

class OrdersPage {
  constructor() {
    this.ordersTitle = Selector('.section-title');
    this.searchDropDown = getElementsByXPath('//md-select[@name="searchType"]');
    this.searchSurnameDropDown = Selector('#select_option_6');
    this.searchTypeEmail = Selector('#select_option_7');
    this.searchTypeMobile = getElementsByXPath('//md-option[@value="4"]')
    this.searchTypeOrderNo = getElementsByXPath('//md-option[@value="1"]')
    this.searchTypeReference = getElementsByXPath('//div[contains(text(), "Reference")]')
    this.searchTypeStatus = getElementsByXPath('//md-option[@value="5"]')
    this.orderStatus = Selector('.md-row:nth-child(1) .e2e-order-status');
    this.orderReferenceRow = getElementsByXPath('//td[contains(@class, "e2e-order-reference")]')
    this.searchKeyword = Selector('[ng-model="vm.searchValue"]');
    this.searchStatusOptionsDropdown = Selector('.md-cell .icon-align-right');
    this.orderDetailsEmail = Selector('.e2e-orderDetails-email');
    this.orderDetailsMobile = Selector('.e2e-orderDetails-mobile_phone');
    this.orderDetailsOrderNo = Selector('.e2e-order-id');
    this.orderDetailsMore = getElementsByXPath('//button[@aria-label="Actions Menu"]')
    this.orderDetailsMoreRefund = getElementsByXPath('//button[@track-label="Refund" and @class="md-button md-focused"]')

    this.orderDetailsMoreResendInvitation = getElementsByXPath('//button[@track-label="Resend Invitation"]')
    this.orderDetailsMoreDelete = getElementsByXPath('//button[@track-label="Delete"]')
    this.orderDetailsMoreProcessPayment = getElementsByXPath('//button[@track-label="Process payment"]')
    this.orderDetailsMoreCancelAmountOutstanding = getElementsByXPath('//button[@track-label="Cancel amount outstanding"]')

    this.refundModalTitle = getElementsByXPath('//h2[contains(text(), "Refund Order")]')
    this.branchDrpDwn = getElementsByXPath('//md-select[@name="branch"]')
    // this.branchDrpDwn = Selector('#select_899')
    this.branchSydney = getElementsByXPath('//div[contains(text(), "Sydney")]')
    this.branchRockdale = getElementsByXPath('//div[contains(text(), "rockdale")]')
    this.branchDashboard = getElementsByXPath('//div[contains(text(), "dashboard")]')
    this.branchWebStore = getElementsByXPath('//div[contains(text(), "Web Store")]')

    this.refundAmount = getElementsByXPath('//input[@id="e2e-refundAmount"]')
    this.refundComment = getElementsByXPath('//textarea[@name="comment"]')
    this.refundSubmit = getElementsByXPath('//button[@track-label="Refund Order"]')
    this.refundConfirm = getElementsByXPath('//span[@aria-label="Confirm"]')
    this.refundBack = getElementsByXPath('//button[@track-label="Back"]')

    this.orderReferenceTitle = getElementsByXPath('//div[contains(text(), "Order Reference")]')
    // this.orderRows = getElementsByXPath('//tr[@track-section="Orders"]')
    this.orderRows = Selector('.e2e-orders-container')
    this.orderReferenceEdit = getElementsByXPath('//button[@zip-permission="EditOrderReference"]')
    this.orderRefenceInput = getElementsByXPath('//input[@name="orderReference"]')
    this.orderReferenceApply = getElementsByXPath('//button[@track-section="edit_order_reference"]')

    this.header2 = getElementsByXPath('//h2')

    this.takings = getElementsByXPath('//button[@aria-label="viewTakings"]')
    this.takingsToday = getElementsByXPath('//span[@ng-bind="::vm.content.today"]')
    this.takingsWeek = getElementsByXPath('//span[@ng-bind="::vm.content.currentWeek"]')
    this.takingsMonth = getElementsByXPath('//span[@ng-bind="::vm.content.currentMonth"]')

  }

  async searchFilter(filterType, statusValue) {
    await t.expect(this.searchDropDown.exists).ok();
    await t.click(this.searchDropDown);
    switch (filterType) {
      case 'email':
        await t
          .click(this.searchTypeEmail)
          .wait(1000)
          .click(this.searchKeyword)
          .typeText(this.searchKeyword, constants.customerEmail.zipPay.email, { replace: true });
        break;
      case 'reference':
        await t
          // .click(this.searchTypeReference)
          .click(Selector('#select_option_13'))
          .wait(1000)
          .typeText(this.searchKeyword, statusValue, { replace: true });
        break;
      case 'mobileNumber':
        await t
          .click(this.searchTypeMobile)
          .wait(1000)
          .click(this.searchKeyword)
          .typeText(this.searchKeyword, constants.genericContactNo, { replace: true });
        break;
      case 'orderNumber':
        await t
          .click(this.searchTypeOrderNo)
          .wait(1000)
          .click(this.searchKeyword)
          .typeText(this.searchKeyword, process.env.ORDERID, { replace: true });
        break;
      case 'status':
        await t
          .click(this.searchTypeStatus)
          .wait(1000)
          .click(this.searchKeyword)
          .click(`[ng-value='status.value'][value='${statusValue}']`);
        break;
      default:
        await t.click(this.searchTypeEmail).wait(1000).typeText(this.searchKeyword, filterType, { replace: true });
        break;
    }
    await t.click('#contentSearch').click(this.searchStatusOptionsDropdown);
  }

  async actionsDropDown(option) {
    await t.expect(this.orderDetailsMore.exists).ok();
    await t.click(this.orderDetailsMore);
    switch (option) {
      case 'refund':
        await t
          .click(this.orderDetailsMoreRefund)
        break;
      case 'resend_invitation':
        await t
          .click(this.orderDetailsMoreResendInvitation)
        break;
      case 'delete':
        await t
          .click(this.orderDetailsMoreDelete)
        break;
      case 'process_payment':
        await t
          .click(this.orderDetailsMoreProcessPayment)
        break;
      case 'cancel_outstanding':
        await t
          .click(this.orderDetailsMoreCancelAmountOutstanding)
        break;
    }
  }
  async branchDropDown(option) {
    await t.expect(this.branchDrpDwn.exists).ok();
    await t.click(this.branchDrpDwn);
    switch (option) {
      case 'sydney':
        await t
          .click(this.branchSydney)
        break;
      case 'dashboard':
        await t
          .click(this.branchDashboard)
        break;
      case 'rockdale':
        await t
          .click(this.branchRockdale)
        break;
      case 'web_store':
        await t
          .click(this.branchWebStore)
        break;
    }
  }
}

export default new OrdersPage();
