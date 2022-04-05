/* eslint-disable class-methods-use-this */
import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';
import { getByPlaceholderText } from '@testing-library/testcafe';
import * as data from '../data/app-data.js';

dotenv.config();

class PasswordSetPage {
  constructor() {
    this.mainHeader = Selector('.auth0-lock-name');
    this.passwordConfirmation = Selector('.auth0-lock-confirmation-content');
  }

  async setPassword(password) {
    await t
      .typeText(getByPlaceholderText('New password'), password)
      .typeText(getByPlaceholderText('Passwords must match'), password)
      .click('.auth0-lock-submit');
  }

  async checkPasswordEmail(mailHelper) {
    const mailcontent = await mailHelper.getEmailDetails(data.emailSubject.setpassword);
    const originationUrl = await mailHelper.getURL(mailcontent.mail_body, 'Set My Password');
    await t.navigateTo(originationUrl);
  }
}

export default new PasswordSetPage();
