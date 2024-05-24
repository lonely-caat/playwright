import { BasePage } from './base.page';
import { expect } from '@playwright/test';

export class WebhooksPage extends BasePage {
  private locators = {
    createWebhook: '//button[@label="Create Webhook"]',
    callbackURL: '//section[@aria-label="webhook-url"]//input',
    webhookName: '//input[@formcontrolname="name"]',
    checkConnection: '//button[@ptooltip="Check connection"]',
    resultsCount: '//small[@aria-label="gql-query-message"]',
    save: '//button[@type="submit"]',
    gqlFilter: '//input[@placeholder="Find everything using GQL"]',
    gqlResultsFound: '//small[@aria-label="gql-query-message"]',
    enabledTumblerModal: '//section[@aria-label="webhook-status"]//div[@class="p-inputswitch p-component"]',
    editByName: (name: string) => `//span[text()="${name}"]/ancestor::tr//button[@ptooltip="Edit Webhook"]`,
    deleteByName: (name: string) => `//span[text()="${name}"]/ancestor::tr//button[@ptooltip="Delete Webhook"]`,
    acceptButton: '//button[@label="Accept"]',
  };

  async clickCreateWebhook() {
    await this.page.locator(this.locators.createWebhook).click();
  }

  async editWebhookByName(name: string) {
    await this.page.locator(this.locators.editByName(name)).click();
    await this.fillName(name + ' edited');
    await this.enableWebhookInModal();
    await this.page.pause();

    await this.saveAndcheckWebhookActive();
    return;
  }

  async deleteWebhookByName(name: string) {
    await this.page.locator(this.locators.deleteByName(name)).click();
    await this.page.locator(this.locators.acceptButton).click();
  }

  async assertResultsMoreThanCount(count: number) {
    const resultsNumber = await this.page.locator(this.locators.resultsCount).textContent();
    expect(parseInt(resultsNumber)).toBeGreaterThan(count);
  }

  async gqlFilterClick() {
    await this.page.locator(this.locators.gqlFilter).click();
  }

  async fillName(name: string) {
    await this.page.locator(this.locators.webhookName).fill(name);
  }

  async saveAndcheckWebhookActive() {
    await this.page.locator(this.locators.save).click();
    const response = await this.page.waitForResponse((response) => response.url().includes('webhook-manager/webhooks'));
    expect(response.status()).toBe(200);

    const responseBody = await response.json();
    expect(responseBody).not.toBeNull();
    expect(responseBody.active).toBe(true);
  }

  async waitAndCheckWebhookActive(webhookName: string) {
    await this.page.waitForTimeout(5000);
    await this.page.reload();

    const response = await this.page.waitForResponse((response) => response.url().includes('webhook-manager/webhooks'));
    const responseBody = await response.json();

    const webhook = responseBody.webhooks.find((webhook) => webhook.name === webhookName);
    expect(webhook).not.toBeNull();
    expect(webhook.active).toBe(true);
  }

  async testCallbackURL(callbackURL: string) {
    await this.page.locator(this.locators.callbackURL).fill(callbackURL);
    await this.page.locator(this.locators.checkConnection).click();
    await this.page.waitForTimeout(500);
    await expect(this.page.getByText('URL is not valid or remote server is not reachable')).toHaveCount(0);
  }

  async enableWebhookInModal() {
    const isDisabled = await this.page.$(this.locators.enabledTumblerModal);

    if (isDisabled) {
      await this.page.click(this.locators.enabledTumblerModal);
    }
  }
}
