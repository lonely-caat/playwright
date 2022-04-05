/* eslint-disable class-methods-use-this */
import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';
import { getByPlaceholderText, getByText } from '@testing-library/testcafe';
import * as constants from '../data/constants';

dotenv.config();

class PrimaryContactPage {
  constructor() {
    this.mainHeader = Selector('h1');
  }

  async populatePrimaryDetails(
    emailId,
    fullName = constants.partnerDetails.directorFullname,
    contactNo = constants.genericContactNo,
  ) {
    await t
      .typeText(getByPlaceholderText('Full name'), fullName)
      .typeText(getByPlaceholderText('Primary contact email'), emailId)
      .typeText(getByPlaceholderText('Primary contact number'), contactNo)
      .click(getByText('Save'));
  }
}

export default new PrimaryContactPage();
