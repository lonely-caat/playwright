/* eslint-disable class-methods-use-this */
import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';
import { getByText } from '@testing-library/testcafe';
import * as constants from '../data/constants';
import directorDetailsPage from './director-details-page';


dotenv.config();

class SettlementPage {
  constructor() {
    this.mainHeader = Selector('h1');
    this.secondaryHeader = Selector('h2.title')
  }

  async populateSettlement(
    emailId,
    accountName = constants.partnerDetails.directorFullname,
    bsbNumber = constants.partnerDetails.bsbNo,
    accountNo = constants.partnerDetails.accountNumber,
  ) {
    await t
      .typeText(Selector('#accountName .mat-form-field-infix'), accountName)
      .typeText(Selector('#bsb .mat-form-field-infix'), bsbNumber)
      .typeText(Selector('#accountNumber .mat-form-field-infix'), accountNo)
      .typeText(Selector('#email .mat-form-field-infix'), emailId);

    await directorDetailsPage.uploadLicenceFile()

    await t
      .click(Selector('#mat-checkbox-1'))
      .click(Selector('#mat-checkbox-2'))
      .click(Selector('#submitButton'));

  }
}

export default new SettlementPage();
