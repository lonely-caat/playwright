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

class NewDashHomePage {
  constructor() {
    this.merchantName = getElementsByXPath('//span[@class="merchant-name"]')
    this.oldDashboardHeader = Selector("[data-e2e-userprofile='']");
    this.oldDashboardLogout = Selector("[track-label='Log Out']");

    this.repaymentCalculatorSubmit = Selector('span').withText('Send order');
    this.repaymentCalculatorOverLimit = getElementsByXPath('//div[@class="MuiAlert-message"]')
    this.repaymentCalculatorPriceError = Selector('#price-helper-text')

    this.repaymentCalculatorEqualRepayment = Selector('h4').withText('Equal monthly interest-free repayments')
    this.repaymentCalculatorEqualRepaymentValue = Selector(this.repaymentCalculatorEqualRepayment).sibling('p')
    this.repaymentCalculatorMonthlyMinimum = Selector('h4').withText('Minimum monthly repayment')
    this.repaymentCalculatorMonthlyMinimumValue = Selector(this.repaymentCalculatorMonthlyMinimum).nextSibling('p')
    this.repaymentCalculatorEstablishmentFee = Selector('h4').withText('Establishment fee')
    this.repaymentCalculatorEstablishmentFeeValue = Selector(this.repaymentCalculatorEstablishmentFee).nextSibling('p')
    this.repaymentCalculatorMonthlyFee = Selector('h4').withText('Monthly account fee')
    this.repaymentCalculatorMonthlyFeeValue = Selector(this.repaymentCalculatorMonthlyFee).nextSibling('p')
  }

  async salesTab() {
    const sales_tab = getElementsByXPath('//span[contains(text(), "Sales")]')
    await t.click(sales_tab);
  }
  async reportsTab() {
    const reports_tab = getElementsByXPath('//span[contains(text(), "Reports")]')
    await t.click(reports_tab);
  }
  async customersTab() {
    const customers_tab = getElementsByXPath('//span[contains(text(), "Customers")]')
    await t.click(customers_tab);
  }
  async profileTab() {
    const profile_tab = getElementsByXPath('//span[contains(text(), "Profile")]')
    await t.click(profile_tab);
  }
  async settingsTab() {
    const settings_tab = getElementsByXPath('//span[contains(text(), "Settings")]')
    await t.click(settings_tab);
  }
  async dashboardTab() {
    const dashboard_tab = getElementsByXPath('//span[contains(text(), "Dashboard")]')
    await t.click(dashboard_tab);
  }
  async repaymentCalculatorTab() {
    const repayment_calculator_tab = Selector('h2').withText('Repayment calculator')
    await t.click(repayment_calculator_tab)
  }

  async fillRepayment(amount= "10", dropDownValue = "12 Months Interest Free A") {
    await t.typeText('#price', amount.toString())
    await t.click('#interestFreePeriodSelect')
    const dropDown = Selector('li').withText(dropDownValue)
    await t.click(dropDown)
  };




  async dashboardLogout() {
    await t.click(this.oldDashboardHeader).click(this.oldDashboardLogout);
  }

  async logOut() {
    await t.click(Selector('button.for-desktop'))
    await t.click(getElementsByXPath('//button[contains(text(), "Log Out")]'))
  }
}

export default new NewDashHomePage();
