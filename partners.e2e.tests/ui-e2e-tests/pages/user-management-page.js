import { Selector, t } from 'testcafe';
import { getByText } from '@testing-library/testcafe';
import dotenv from 'dotenv-safe';
import * as constants from '../data/constants';
import helper from '../utils/helper';

dotenv.config();

class UserManagementPage {
  constructor() {
    this.usersTitle = Selector('.section-title');
    this.firstName = Selector('#firstName');
    this.lastName = Selector('#lastName');
    this.emailId = Selector('#email');
    this.userRole = Selector('#role-select');
    this.userRoleOption = Selector('.e2e-merchant-role');
    this.branch = Selector('#branch');
    this.branchOption = Selector('#select_option_18');
    this.contactNo = Selector('#mobilePhone');
    this.staffRefCode = Selector('#staffRefCode');
    this.toastMessage = Selector('#e2e-toastMessage');
    this.searchUser = Selector('#searchUser');
    this.toggleUserDetails = Selector('#e2e-toggleUserDetails');
    this.userDetailsEmailId = Selector('#e2e-userListEmailId');
    this.searchIcon = Selector("[aria-label='Search'] .md-24");
    this.editUserDetails = Selector('#e2e-editUser');
    this.deleteUserDetails = Selector('#e2e-deleteUser');
    this.confirmDelete = Selector('#e2e-confirmLabel');
    this.mobileNo = Selector('#mobilePhone');
    this.empCode = Selector('#staffRefCode');
    this.userNotFound = Selector('.l-padding-top-medium');
  }

  async addUser(userEmail, dashboardUserRole) {
    const refCode = await helper.getRandomString(5);
    await t
      .typeText(this.firstName, constants.userManagement.firstName)
      .typeText(this.lastName, constants.userManagement.lastName)
      .typeText(this.emailId, userEmail)
      .click(this.userRole);
    switch (dashboardUserRole) {
      case 'manager': {
        await t
          .click(this.userRoleOption.withExactText('Manager'))
          .click(this.branch)
          .click(this.branchOption)
          .pressKey('tab');
        break;
      }
      case 'store_user': {
        await t
          .click(this.userRoleOption.withExactText('Store User'))
          .click(this.branch)
          .click(this.branchOption)
          .pressKey('tab');
        break;
      }
      case 'reporting_user': {
        await t
          .click(this.userRoleOption.withExactText('Reporting User'))
          .pressKey('tab');
        break;
      }
      case 'api_admin': {
        await t
          .click(this.userRoleOption.withExactText('ApiV3 Admin'))
          .pressKey('tab');
        break;
      }
      case 'marketing_user': {
        await t
          .click(this.userRoleOption.withExactText('Marketing User'))
          .click(this.branch)
          .click(this.branchOption)
          .pressKey('tab');
        break;
      }
      case 'web_manager': {
        await t
          .click(this.userRoleOption.withExactText('Web Manager'))
          .pressKey('tab');
        break;
      }
      default:
        await t
          .click(this.userRoleOption.withExactText('Web Manager'))
          .wait(1000)
          .typeText(this.mobileNo, constants.genericContactNo)
          .typeText(this.empCode, refCode);
        break;
    }
    await t.click(getByText('Save changes'));
  }

  async searchUserDetails(email) {
    await t.typeText(this.searchUser, email).click(this.searchIcon).click(this.toggleUserDetails);
  }

  async editUser() {
    await t
      .click(this.editUserDetails)
      .click(this.lastName)
      .typeText(this.lastName, constants.userManagement.updateLastName)
      .click(getByText('Save changes'));
  }

  async deleteUser() {
    await t.click(this.toggleUserDetails).click(this.deleteUserDetails).click(this.confirmDelete);
  }
}

export default new UserManagementPage();
