/* eslint-disable class-methods-use-this */
import dotenv from 'dotenv-safe';
import { Selector, t } from 'testcafe';
import * as constants from '../data/constants';

dotenv.config();

class WalletPage {
  constructor() {
    this.zipMoneySignInLink = Selector("[product-id='2']");
    this.emailAddress = Selector('input').withAttribute('placeholder', 'Email Address');
    this.password = Selector('input').withAttribute('placeholder', 'Password');
    this.signInButton = Selector('button');
    this.completeCustomerOrder = Selector('#completeOrder');
    this.customerOrderHeader = Selector('h1');
  }

  async populateLoginDetails(email) {
    await t.typeText(this.emailAddress, email);
    await t.typeText(this.password, constants.defaultPassword);
    await t.click(this.signInButton);
  }

  async zipMoneySignIn() {
    await t.click(this.zipMoneySignInLink);
  }

  async originationOrderConfirm() {
    await t.click(this.completeCustomerOrder);
  }

  async mobileVerification(code = constants.defaultSmsCode) {
    await t.click(Selector('.e2e-smsVerification_sendCode'));
    await t.typeText(Selector('#verificationCode'), code);
    await t.click(Selector('#smsVerification-submit'));
  }
}

export default new WalletPage();
