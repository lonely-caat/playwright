import { expect } from '@playwright/test';
import { BasePage } from './base.page';

export class DetectorsPage extends BasePage {
  private locators = {
    create: '//button[@label="Create"]',
    refresh: '//i[text()="sync"]',
    testDetectors: '//button[@label="Test My Detectors"]',
    queryName: '//input[@placeholder="E.g. Email Matcher"]',
    containInputs: '//label[text()=" Contain "]/following::p-chips//input',
    caseSensitiveTumbler: '//p-inputswitch[@formcontrolname="caseSensitive"]',
    enabledTumbler: '//p-inputswitch[@formcontrolname="enabled"]',
    regexRadio: '//fieldset//label[text()="Regex"]',
    keywordsRadio: '//fieldset//label[text()="Keywords"]',
    regexInput: '//textarea[@placeholder="E.g. [a-zA-Z]+@[a-zA-Z]+.[a-zA-Z]+"]',
    testinput: '//textarea[@placeholder="E.g. JohnDoe@company.com"]',
    save: '//button[@label="Save"]',
    canvas: '//canvas',
    matchOnCanvas: (name) => `//span[text()="${name}"]`,
    deleteRowByName: (name: string) =>
      `//tr[.//td[normalize-space(.)='${name}']]//button[.//i[contains(@class, 'material-icons') and text()='delete_outline']]`,
    editRowByName: (name: string) =>
      `//tr[.//td[normalize-space(.)='${name}']]//button[.//i[contains(@class, 'material-icons') and text()='edit']]`,
    tumblersByName: (name: string) => `//tr[.//td[normalize-space(.)='${name}']]//span[@data-pc-section='slider']`,
  };

  async createDetector(
    query: string,
    caseSensitive: boolean,
    enabled: boolean,
    keywordsYes?: string,
    keywordsNo?: string,
  ) {
    await this.page.locator(this.locators.create).click();
    await this.page.locator(this.locators.queryName).fill(query);

    if (!caseSensitive) {
      await this.page.locator(this.locators.caseSensitiveTumbler).click();
    }
    if (!enabled) {
      await this.page.locator(this.locators.enabledTumbler).click();
    }

    if (keywordsYes) {
      await this.page.locator(this.locators.containInputs).first().fill(keywordsYes);
      await this.page.keyboard.press('Enter');
    }

    if (keywordsNo) {
      await this.page.locator(this.locators.containInputs).last().fill(keywordsNo);
      await this.page.keyboard.press('Enter');
    }

    await this.page.locator(this.locators.save).click();
  }

  async createRegexDetector(query: string, enabled: boolean, regex: string) {
    await this.page.locator(this.locators.create).click();
    await this.page.locator(this.locators.queryName).fill(query);
    await this.page.locator(this.locators.regexRadio).click();
    await this.page.locator(this.locators.regexInput).fill(regex);

    if (!enabled) {
      await this.page.locator(this.locators.enabledTumbler).click();
    }
    await this.page.pause();
    await this.page.locator(this.locators.save).click();
  }

  async deleteDetectorByName(name: string) {
    await this.page.locator(this.locators.deleteRowByName(name)).click();
    await this.page.locator(this.commonLocators.acceptButton).click();
  }

  async editDetectorByName(name: string, keywordsYes?: string, keywordsNo?: string) {
    await this.page.locator(this.locators.editRowByName(name)).click();
    if (keywordsYes) {
      await this.page.locator(this.locators.containInputs).first().fill(keywordsYes);
      await this.page.keyboard.press('Enter');
    }

    if (keywordsNo) {
      await this.page.locator(this.locators.containInputs).last().fill(keywordsNo);
      await this.page.keyboard.press('Enter');
    }
    await this.page.locator(this.locators.save).click();
  }

  async disableDetectorByName(name: string) {
    await this.page.locator(this.locators.tumblersByName(name)).last().click();
  }

  async changeDetectorTumbler(name: string) {
    await this.page.locator(this.locators.tumblersByName(name)).first().click();
  }

  async testDetector(name: string, expectedMatch: string) {
    await this.page.locator(this.locators.testinput).fill(name);
    await this.page.locator(this.locators.testDetectors).click();
    await expect(this.page.locator(this.locators.canvas)).toBeVisible();
    await expect(this.page.locator((this.locators.matchOnCanvas(expectedMatch)))).toBeVisible({ timeout: 5000 });
  }
}
