import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';
import { getByPlaceholderText, getByText } from '@testing-library/testcafe';
import * as constants from '../data/constants';
import * as data from '../data/app-data.js';

dotenv.config();

class CreateInvitePage {
  constructor() {
    this.mainHeader = Selector('zip-content-heading');
    this.branchOption = Selector('.ng-option');
    this.branch = Selector('.ng-input');
    this.inviteSent = Selector('.action-content p');
    this.accountHeader = Selector('h1 .ng-binding:nth-child(1)');
  }

  async sendInvite(
    email,
    firstName = constants.sendInviteCustomer.firstName,
    lastName = constants.sendInviteCustomer.lastName,
    mobileNo = constants.genericContactNo,
  ) {
    await t
      .click(this.branch)
      .click(this.branchOption.withText('Sydney'))
      .typeText(getByPlaceholderText("Enter customer's first name"), firstName)
      .typeText(getByPlaceholderText("Enter customer's last name"), lastName)
      .typeText(getByPlaceholderText("Enter customer's email"), email)
      .typeText(getByPlaceholderText("Enter customer's mobile"), mobileNo)
      .click(getByText('Send Invite'));
  }

  async checkEmail(mailHelper) {
    const mailcontent = await mailHelper.getEmailDetails(data.emailSubject.invite);
    const originationUrl = await mailHelper.getURL(mailcontent.mail_body, 'Create Account');
    await t.navigateTo(originationUrl);
  }
}
export default new CreateInvitePage();
