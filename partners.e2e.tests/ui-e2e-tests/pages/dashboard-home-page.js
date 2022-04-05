/* eslint-disable class-methods-use-this */
import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';

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

class DashboardHomePage {
  constructor() {
    this.merchantName = getElementsByXPath('//span[@class="merchant-name"]')
    this.oldDashboardHeader = Selector("[data-e2e-userprofile='']");
    this.oldDashboardLogout = Selector("[track-label='Log Out']");
  }

  async createOrderTile() {
    const create_order = getElementsByXPath('//p[contains(text(), "Create Order")]')
    await t.click(create_order);
  }

  async createInviteTile() {
    const invite = getElementsByXPath('//p[contains(text(), "Start an application")]')
    await t.click(invite);
  }

  async orderSearchTile() {
    const order_search = getElementsByXPath('//p[contains(text(), "Order Search")]')
    await t.click(order_search);
  }

  async customerSearchTile() {
    const customer_search = getElementsByXPath('//p[contains(text(), "Customer Search")]')
    await t.click(customer_search);
  }

  async adminTile() {
    const admin = getElementsByXPath('//p[contains(text(), "Admin")]')
    await t.click(admin);
  }

  async remoteOrderTile() {
    const remote_order = getElementsByXPath('//p[contains(text(), "Remote Order")]')
    await t.click(remote_order);
  }

  async repaymentCalculator() {
    const repayment_calculator = getElementsByXPath('//p[contains(text(), "Repayment Calculator")]')
    await t.click(repayment_calculator);
  }

  async refundTile() {
    const refund = getElementsByXPath('//p[contains(text(), "Refund")]')
    await t.click(refund);
  }

  async dashboardLogout() {
    await t.click(this.oldDashboardHeader).click(this.oldDashboardLogout);
  }

  async logOut() {
    await t.click(Selector('button.for-desktop'))
    await t.click(getElementsByXPath('//button[contains(text(), "Log Out")]'))
  }
}

export default new DashboardHomePage();
