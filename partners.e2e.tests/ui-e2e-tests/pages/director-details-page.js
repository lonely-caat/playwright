/* eslint-disable class-methods-use-this */
import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';
import { getByPlaceholderText, getByText } from '@testing-library/testcafe';
import helper from '../utils/helper';
import * as constants from '../data/constants';

dotenv.config();

class DirectorDetailsPage {
  constructor() {
    this.mainHeader = Selector('h1');
    this.addressLookup = getByPlaceholderText('e.g. 123 Main Street Sydney');
    this.pickAddress = Selector('.pac-matched');
    this.dropDown = Selector('.mat-select-value');

    this.driverLicense = getByText('Driver Licence')
    this.passport = getByText('Passport')

    this.submit = getByText('Submit')

  }

  async populateDirectorDetails(
    emailId,
    firstName = constants.partnerDetails.firstName,
    lastName = constants.partnerDetails.surName,
    midName = constants.partnerDetails.midName,
    contactNo = constants.genericContactNo,
  ) {
    await t
      .typeText(Selector('#mat-input-0'), firstName)
      .typeText(Selector('#mat-input-1'), midName)
      .typeText(Selector('#mat-input-2'), lastName)

    await helper.populateDirectorAddressManual();
    await t
      .typeText(getByPlaceholderText('name@email.com'), emailId)
      .typeText(getByPlaceholderText("0400000000"), contactNo)
  }

  async addLicenceDetails(
    licenceNo = constants.partnerDetails.licenceNumber,
    dob = constants.partnerDetails.dob,
  ) {
    await t
      .typeText(getByPlaceholderText('12345678910'), licenceNo)
      .typeText(getByPlaceholderText('DD/MM/YYYY'), dob)
      .click(Selector('.mat-select-placeholder'))
      .click(Selector('span').withText('QLD'))
  }

  async uploadLicenceFile() {
    const uploadFile = Selector('input[type=file]');
    await t.setFilesToUpload(uploadFile, ['../data/fileupload/Automation_Upload.png']);
    await t
      .expect(Selector('#submitButton .ng-star-inserted'))
      .ok()
      .click(Selector('#submitButton .ng-star-inserted'));
  }
}

export default new DirectorDetailsPage();
