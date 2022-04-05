/* eslint-disable class-methods-use-this */
import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';
import { getByText, getByPlaceholderText } from '@testing-library/testcafe';
import * as constants from '../data/constants';

dotenv.config();

class PersonalDetailsPage {
  constructor() {
    this.mainHeader = Selector('h2');
  }

  async personalDetails(
    emailId,
    firstName = constants.partnerDetails.firstName,
    surName = constants.partnerDetails.surName,
    contactNo = constants.genericContactNo,
  ) {
    await t
      .typeText(getByPlaceholderText('Your first name'), firstName)
      .typeText(getByPlaceholderText('Your surname'), surName)
      .typeText(getByPlaceholderText('name@mail.com'), emailId)
      .typeText(getByPlaceholderText('0400000000'), contactNo)
      .click(getByText('Continue'));
  }
}
export default new PersonalDetailsPage();
