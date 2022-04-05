import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';
import { getByPlaceholderText } from '@testing-library/testcafe';
import * as constants from '../data/constants';
import helper from '../utils/helper';
import * as data from '../data/app-data.js';

dotenv.config();

class RemoteOrderPage {
  constructor() {
    this.mainHeader = Selector('zip-content-heading');
    this.branchOption = Selector('.ng-option');
    this.branch = Selector('.ng-input');
    this.orderSent = Selector('.action-content p');
    this.accountHeader = Selector('h1 .ng-binding:nth-child(1)');
    this.submitButton = Selector('[type="submit"]');
  }

  async sendOrder(
    email,
    price = constants.price,
    lastName = constants.sendInviteCustomer.lastName,
    mobileNo = constants.genericContactNo,
  ) {
    const reference = await helper.getRandomString(5);
    await t
      .click(this.branch)
      .click(this.branchOption.withText('Sydney'))
      .typeText(getByPlaceholderText('Enter price'), price)
      .typeText(getByPlaceholderText('Order reference'), reference)
      .typeText(getByPlaceholderText("Enter customer's last name"), lastName)
      .typeText(getByPlaceholderText("Enter customer's email"), email)
      .typeText(getByPlaceholderText("Enter customer's mobile"), mobileNo)
      .click(this.submitButton);
  }

  async checkSendOrderEmail(mailHelper) {
    const mailcontent = await mailHelper.getEmailDetails(data.emailSubject.sendorder);
    const originationUrl = await mailHelper.getURL(
      mailcontent.mail_body,
      'Click to complete my order',
    );
    await t.navigateTo(originationUrl);
  }
}

export default new RemoteOrderPage();
