import {Page, expect, Locator} from '@playwright/test';

export class BasePage {
  page: Page;

  constructor(page: Page) {
    this.page = page;
  }

  commonLocators = {
    acceptButton: '//button[@label="Accept"]',
    saveButton: '//button[@label="Save"]',
  }

  async navigateTo(path: string) {
    await this.page.goto(`ui/en-US/${path}`);
    await this.page.waitForLoadState('networkidle', {timeout: 30000});
  }

  async clickOnCloseButton() {
    await this.page.getByRole('button', { name: 'close' }).click();
  }

  async clickOnAcceptButton() {
    await this.page.locator(this.commonLocators.acceptButton).click();
  }

  async clickButtonAndVerifyRequest(buttonSelector: Locator, expectedUrl: string, expectedResponseStatus: number) {
    await buttonSelector.click();
    const response = await this.page.waitForResponse(response => response.url().includes(expectedUrl));
    expect(await response.status()).toBe(expectedResponseStatus);
  }
}
