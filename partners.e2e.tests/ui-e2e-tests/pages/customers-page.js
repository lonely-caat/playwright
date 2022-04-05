/* eslint-disable class-methods-use-this */
import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';
import * as constants from '../data/constants';

dotenv.config();

class CustomersPage {
  constructor() {
    this.customerTitle = Selector('.section-title');
    this.searchDropDown = Selector('.md-select-icon');
    this.searchTypeCustomerSurname = Selector('#select_option_6');
    this.searchTypeCustomerEmail = Selector('#select_option_7');
    this.searchTypeCustomerMobile = Selector('#select_option_8');
    this.searchTypeStatus = Selector('#select_option_9');
    this.searchKeyword = Selector('[ng-model="vm.searchValue"]');
    this.searchStatusOptionsDropdown = Selector('.md-cell .icon-align-right');
    this.customerDetailsSurname = Selector('.e2e-customer-sur_name')
    this.customerDetailsEmail = Selector('.e2e-customerDetails-email');
    this.customerDetailsMobile = Selector('.e2e-customer-mobile_phone');
    this.searchTypeStatusDropDown = Selector('#select_value_label_1 .md-select-icon');
    this.searchTypeStatusValueDropDown = Selector('.flex-25:nth-child(4)');
    this.customerStatus = Selector('.selected-row .e2e-customer-status');
  }

  async searchFilter(filterType, statusValue) {
    await t.expect(this.searchDropDown.exists).ok();
    switch (filterType) {
      case 'email':
        await t.click(this.searchDropDown);
        await t
          .click(this.searchTypeCustomerEmail)
          .wait(1000)
          .typeText(this.searchKeyword, constants.customerEmail.zipCredit.email);
        break;
      case 'mobileNumber':
        await t.click(this.searchDropDown);
        await t
          .click(this.searchTypeCustomerMobile)
          .wait(1000)
          .typeText(this.searchKeyword, constants.customerEmail.zipCredit.mobileNumber);
        break;
      case 'surname':
        await t.click(this.searchDropDown);
        await t
            .click(this.searchTypeCustomerSurname)
            .typeText(this.searchKeyword, constants.sendInviteCustomer.lastName);
        break;
      case 'status':
        await t.click(this.searchDropDown);
        await t
          .click(this.searchTypeStatus)
          .wait(1000)
          .click(this.searchTypeStatusValueDropDown)
          .click(`[ng-value='status.value'][value='${statusValue}']`);
        break;
      default:
        await t.click(this.searchTypeEmail).wait(1000).typeText(this.searchKeyword, filterType);
        break;
    }
    await t.click('#contentSearch').click(this.searchStatusOptionsDropdown);
  }
}

export default new CustomersPage();
