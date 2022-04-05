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

class NotificationsSettingsPage {
  constructor() {
    this.notificationSettingsTitle = getElementsByXPath('//h1[@class="section-title"]');

    this.notificationCustomerAuthorised = getElementsByXPath('//md-checkbox[@aria-label="Authorised"]')
    this.notificationCustomerCancelled = getElementsByXPath('//md-checkbox[@aria-label="Cancelled"]')
    this.notificationCustomerCompleted = getElementsByXPath('//md-checkbox[@aria-label="Completed Order"]')
    this.notificationCustomerRefunded = getElementsByXPath('//md-checkbox[@aria-label="Refunded"]')
    this.notificationCustomerApproved = getElementsByXPath('//md-checkbox[@aria-label="Approved"]')
    this.notificationCustomerDeclined = getElementsByXPath('//md-checkbox[@aria-label="Declined"]')
    this.notificationCustomerUnderReview = getElementsByXPath('//md-checkbox[@aria-label="UnderReview"]')
    this.allCheckboxes = [this.notificationCustomerAuthorised, this.notificationCustomerCancelled,
      this.notificationCustomerCompleted, this.notificationCustomerRefunded, this.notificationCustomerApproved,
      this.notificationCustomerDeclined, this.notificationCustomerUnderReview]

    this.saveBtn = getElementsByXPath('//button[@aria-label="Save"]')

  }
}

export default new NotificationsSettingsPage();