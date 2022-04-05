/* eslint-disable class-methods-use-this */
/* eslint-disable no-undef */
import { Selector, t, ClientFunction } from 'testcafe';
import dotenv from 'dotenv-safe';
import { getByPlaceholderText, getByText } from '@testing-library/testcafe';
import helper from '../utils/api-helper';
import * as constants from '../data/constants';

dotenv.config();

class CreateOrderPage {
  constructor() {
    this.mainHeader = Selector('zip-content-heading');
    this.succesCheckMark = Selector('#instore-check-mark img');
    this.receiptNumber = Selector('#order-confirmed-receipt-number');
    this.orderComplete = Selector('#order-confirmed-receipt-number+ p');
    this.branchOption = Selector('.ng-option');
    this.branch = Selector('.ng-input');
  }

  async createOrder(userEmail) {
    const code = await this.getInstorePin(userEmail);
    console.log(code)
    await t
      .click(this.branch)
      .click(this.branchOption.withText('Sydney'))
      .typeText(getByPlaceholderText('Enter code from Zip customer'), code)
      .click(getByPlaceholderText('Enter code from Zip customer'))
      .typeText(getByPlaceholderText('Enter price'), constants.price)
  }

  async getInstorePin(userEmail) {
    const response = await helper.createInstorePin(constants.product.ZipCredit, userEmail);
    return response.text;
  }

  async submitOrder() {
    await t.wait(1000)
    await t.click(getByText('Create Order'));
  }

  async getUrlReceiptId(path) {
    const getLocation = await ClientFunction(() => window.location.href)();
    const response = getLocation.split(`${path}/`);
    return response[1];
  }
}

export default new CreateOrderPage();
