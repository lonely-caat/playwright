/* eslint-disable class-methods-use-this */
import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';

dotenv.config();

class DisbursementsPage {
  constructor() {
    this.generateReportHeader = Selector('zip-disbursements-exporter h3');
    this.downloadLink = Selector('#link-1');
    this.downloadText = Selector('#content .layout-row p');
    this.dailyRecurrenceCheckBox = Selector('md-checkbox[ng-model="vm.model.daily"]')
    this.weeklyRecurrenceCheckBox = Selector('md-checkbox[ng-model="vm.model.weekly"]')
    this.monthlyRecurrenceCheckBox = Selector('md-checkbox[ng-model="vm.model.monthly"]')

  }

  async createReport() {
    await t.click('#e2e-createReportButton');
  }
}

export default new DisbursementsPage();
