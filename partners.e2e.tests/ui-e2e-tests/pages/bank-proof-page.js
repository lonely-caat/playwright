/* eslint-disable class-methods-use-this */
import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';
import { getByText } from '@testing-library/testcafe';

dotenv.config();

class BankProofPage {
  async bankFileUpload() {
    const uploadFile = Selector('input[type=file]');
    await t
      .setFilesToUpload(uploadFile, ['../data/fileupload/Automation_Upload.png'])
      .click(Selector('#mat-checkbox-2 > label > div'))
      .click(Selector('#mat-checkbox-3 > label > div'))
      .click(getByText('Save'));
  }
}

export default new BankProofPage();
