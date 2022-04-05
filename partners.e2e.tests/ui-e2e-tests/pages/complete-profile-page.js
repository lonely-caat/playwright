/* eslint-disable class-methods-use-this */
import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';
import { getByPlaceholderText } from '@testing-library/testcafe';
import * as constants from '../data/constants';

dotenv.config();

class CompleteProfilePage {
  constructor() {
    this.mainHeader = Selector('h1');
    this.toastMessage = Selector('.success');
  }

  async signIn(emailId, password = constants.partnerPassword) {
    await t
      .typeText(getByPlaceholderText('example@email.com'), emailId)
      .typeText(getByPlaceholderText('Enter password'), password)
      .click('.auth0-lock-submit');
  }

  goToSection(sectionName) {
    return t.click(Selector('h3').withText(sectionName));
  }

  async submitApplication() {
    await t.click('.content');
  }
}

export default new CompleteProfilePage();
