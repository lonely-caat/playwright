/* eslint-disable class-methods-use-this */
import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';
import { getByPlaceholderText } from '@testing-library/testcafe';
import * as constants from '../data/constants';

dotenv.config();

class PartnerSignInPage {
  constructor() {
    this.mainHeader = Selector('div[title="Sign in as Zip partner"]');

    this.forgotPassword = Selector('.auth0-lock-alternative-link')
    this.forgotEmail = Selector('#\\31 -email')
    this.sendLink = Selector('.auth0-label-submit')

    this.resetPasswordHeader = Selector('.auth0-lock-name')
    this.resetPasswordText = Selector('.auth0-lock-form > p:nth-child(1) > span:nth-child(1)')

    this.resetExpired = Selector('.content > h1:nth-child(3)')
  }

  async setPassword(password) {
    await t
      .typeText(getByPlaceholderText('New password'), password)
      .typeText(getByPlaceholderText('Passwords must match'), password)
      .click('.auth0-lock-submit');
  }

  async signIn(emailId, password = constants.partnerPassword) {
    await t
      .expect(this.mainHeader.textContent)
      .contains('Sign in as Zip partner')
      .typeText(getByPlaceholderText('example@email.com'), emailId)
      .typeText(getByPlaceholderText('Enter password'), password)
      .click('.auth0-lock-submit');
  }
}

export default new PartnerSignInPage();
