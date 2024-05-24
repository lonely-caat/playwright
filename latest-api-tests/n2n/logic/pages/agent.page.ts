import { BasePage } from './base.page';
import { expect } from '@playwright/test';

export class AgentPage extends BasePage {
  private locators = {
    inputSwitch: 'p-inputswitch span',
    exportButton: '//button[@label="Export"]',
    createExportButton: '//button[@label="Create"]',
    nameInput: '//input[@formcontrolname="filename"]',
    dropdown: 'p-dropdown[formcontrolname="fileType"]',
    dropdownLabel: '.p-dropdown-label',
    dropdownOption: (value: string) => `.p-dropdown-items .p-dropdown-item:has-text("${value}")`,
    tableRows: (name) => this.page.locator(`//table[@role="table"]//tr[td[1][text()="${name}"]]`),
    firstTableRow: (name) => this.page.locator(`//td[text()=" ${name} "]`),
    autoRefresh: '//gv-shared-ui-auto-refresh//label[text()="Auto refresh"]',
    refreshDropdown: '//gv-shared-ui-auto-refresh//span[@role="combobox"]',
    dropdownOptionLocator: (optionText) =>
      this.page.locator(
        `//gv-shared-ui-auto-refresh//p-dropdown//ul[@role="listbox"]//li[span[text()="${optionText}"]]`,
      ),
  };

  async exportData(export_type: string = 'jsonl') {
    await this.page.locator(this.locators.exportButton).click();
    const exportName = await this.page.locator(this.locators.nameInput).inputValue();
    await this.selectDropdownValue(export_type);
    await this.page.locator(this.locators.createExportButton).click();
    await expect(this.page.locator("text='Export job created successfully'")).toBeVisible({ timeout: 30000 });

    await this.checkExportJobCreated(exportName);
  }

  async returnAutoRefreshOptions() {
    await this.page.locator(this.locators.autoRefresh).click();
    const dropdownLocator = this.page.locator(this.locators.refreshDropdown);
    await dropdownLocator.click();
    await this.locators.dropdownOptionLocator('1 minute').waitFor({ state: 'visible' });
    return this.page.locator('//gv-shared-ui-auto-refresh//p-dropdown//ul[@role="listbox"]');
  }

  async checkExportJobCreated(name: string = 'test') {
    await this.page.locator(this.locators.exportButton).click();
    const rowWithName = this.locators.firstTableRow(name);
    await expect(rowWithName.first()).toContainText(name, { timeout: 60000 });
  }

  async selectDropdownValue(value: string) {
    await this.page.locator(this.locators.dropdown).click();
    await this.page.locator(this.locators.dropdownOption(value)).click();
  }
}
