/* eslint-disable class-methods-use-this */
import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';
import { getByText, getByPlaceholderText } from '@testing-library/testcafe';
import helper from '../utils/helper';
import * as constants from '../data/constants';

dotenv.config();

class LocationDetailsPage {
  constructor() {
    this.storeLocation = Selector('.mat-select-placeholder');
    this.dropDown = Selector('.mat-option');
    this.addressLookup = getByPlaceholderText('e.g. 123 Main Street Sydney');
    this.pickAddress = Selector('.pac-matched');
  }

  async locationDetails(contactNo = constants.genericContactNo) {
    await t.click(this.addressLookup);
    // await helper.populateAddress(this.addressLookup, this.pickAddress);
    await helper.populateAddressManual()
    await t.click(this.storeLocation);
    await t
      .click(this.dropDown.withText('2 - 5'))
      .typeText(getByPlaceholderText('Main business number'), contactNo)
      .click(getByText('Submit'));
  }
}

export default new LocationDetailsPage();
