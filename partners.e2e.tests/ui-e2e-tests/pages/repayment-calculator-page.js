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

class RepaymentCalculatorPage {
  constructor() {
    this.repaymentsTitle = getElementsByXPath('//zip-content-heading');
    this.priceInput = getElementsByXPath('//input[@placeholder="Enter price"]');
    this.monthlyRepayment = getElementsByXPath('//zip-input-label[contains(text(), "Minimum Monthly Repayment")]/following-sibling::p[1]')
    this.establishmentFee = getElementsByXPath('//zip-input-label[contains(text(), "Establishment fee")]/following-sibling::p[1]')
    this.monthlyAccountFee = getElementsByXPath('//zip-input-label[contains(text(), "Monthly Account Fee")]/following-sibling::p[1]')
    this.sendOrderBtn = getElementsByXPath('//div[contains(text(), "Send Order")]');
    this.returnBtn = getElementsByXPath('//a[contains(text(), "Return")]');
    this.maxAmountErr = getElementsByXPath('//zip-input-notice[contains(text(), "Maximum amount you can invite with is $3000")]');
    this.accountLimitErr = getElementsByXPath('//div[contains(@class, "invalidError")]');
  }
}

export default new RepaymentCalculatorPage();