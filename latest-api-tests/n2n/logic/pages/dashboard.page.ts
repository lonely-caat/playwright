import { Page } from '@playwright/test';
import { BasePage } from './base.page';

export class DashboardPage extends BasePage {
  private locators = {
    inputSwitchSpan: 'p-inputswitch span',
    pageContentLabel: 'page-content',
    searchControlsLabel: 'search-controls',
  };

  async toggleInputSwitch() {
    await this.page.locator(this.locators.inputSwitchSpan).click();
    await this.page.locator(this.locators.inputSwitchSpan).click();
  }

  async clickOnPageContent() {
    await this.page.getByLabel(this.locators.pageContentLabel).click();
  }

  async navigateToAndClickLink(action: string) {
    await this.navigateTo('/dashboard/companyoverview');
    await this.page.getByRole('link', { name: action }).click();
  }

  async toggleSearchControls() {
    await this.page
      .getByLabel(this.locators.searchControlsLabel)
      .locator('span')
      .click();
    await this.page
      .getByLabel(this.locators.searchControlsLabel)
      .locator('span')
      .click();
  }

  async clickExportButton(buttonName: string) {
    await this.page.getByRole('button', { name: buttonName }).click();
  }
}
