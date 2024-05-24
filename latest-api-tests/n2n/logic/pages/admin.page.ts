import { BasePage } from './base.page';
import { connectorSecrets } from "../../playwright.config";
import { expect, selectors } from "@playwright/test";

export class AdminPage extends BasePage {
  private locators = {
    inputSwitch: 'p-inputswitch span',
    multiSelectTrigger: '.p-multiselect-trigger',
    searchInput: '//input[@placeholder="Search connections by name"]',
    awsName: '//gv-shared-ui-form-control[@aria-label="name-input"]//input',
    awsKey: '//gv-shared-ui-form-control[@aria-label="access-key-input"]//input',
    awsSecret: '//gv-shared-ui-form-control[@aria-label="secret-access-key-input"]//input',
    awsPath: '//gv-shared-ui-form-control[@aria-label="path-input"]//input',

    connectionString: '//input[@formcontrolname="ConnectionString"]',

    userID: '//input[@formcontrolname="userID"]',
    userIDSnake: '//input[@formcontrolname="userId"]',
    enterpiseID: '//input[@formcontrolname="enterpriseID"]',
    clientID: '//input[@formcontrolname="clientID"]',
    clientIDSnake: '//input[@formcontrolname="clientId"]',
    clientSecret: '//input[@formcontrolname="clientSecret"]',
    publicKeyID: '//input[@formcontrolname="publicKeyID"]',
    privateKey: '//input[@formcontrolname="privateKey"]',
    passphrase: '//input[@formcontrolname="passphrase"]',

    username: '//input[@formcontrolname="username"]',
    user: '//span[text()="Select a user"]',
    userOptions: '//li[@role="option"]',
    usernameSnake: '//input[@formcontrolname="userName"]',
    password: '//input[@formcontrolname="password"]',
    domain: '//input[@formcontrolname="domain"]',
    host: '//input[@formcontrolname="host"]',
    port: '//input[@formcontrolname="port"]',
    path: '//input[@formcontrolname="path"]',

    apiToken: '//input[@formcontrolname="apiToken"]',

    clientEmail: '//input[@formcontrolname="clientEmail"]',
    adminEmail: '//input[@formcontrolname="administratorName"]',
    adminPassword: '//input[@formcontrolname="administratorPassword"]',

    alias: '//input[@formcontrolname="alias"]',

    adminUserID: '//input[@formcontrolname="tenantId"]',
    tenantID: '//input[@formcontrolname="tenantId"]',

    saveSnake: '//div[@role="dialog"]//span[text()="Save"]',
    accept: '//div[@role="dialog"]//span[text()="Accept"]',
    selectLanguage: '//label[@for="selectLanguage"]',
    //Needs proper attribute badly
    saveSettings: '//button[@data-pc-name="button"]',
  };
  private tableActionsLocators = {
    rowByName: (name: string) => `//table//tbody//tr//td[text()=" ${name} "]`,
    delete: 'i.material-icons:has-text("delete_outline")',
    edit: 'i.material-icons-outlined:has-text("edit")',
    radar: 'i.material-icons:has-text("radar")',
    label: 'i.material-icons-outlined:has-text("label")',
    scan: 'i.material-symbols-outlined:has-text("quick_reference_all")',
  };

  async clickInputSwitch() {
    await this.page.locator(this.locators.inputSwitch).click();
  }

  async testDefaultLanguage() {
    await this.page.locator('//label[text()=" Use default language (English) "]').click();
    await this.page.locator(this.locators.saveSettings).click();
    await expect(this.page).toHaveURL(/.*en-US/);
  }

  async testLanguages(language, languageCode) {
    await this.page.locator(this.locators.selectLanguage).click();
    await this.page.locator('//span[@role="combobox"]').click()
    await this.page.locator(`//li//span[text()="${language}"]`).click();
    await this.page.locator(this.locators.saveSettings).click();
    await expect(this.page).toHaveURL(new RegExp(`.*${languageCode}`));
  }

  async searchConnection(name: string) {
    await this.page.locator(this.locators.searchInput).fill(name);
  }

  async clickSaveSnake() {
    await this.page.locator(this.locators.saveSnake).click();
  }

  async clickAccept() {
    await this.page.locator(this.locators.accept).click();
  }

  async clickMultiSelectTrigger() {
    await this.page.locator(this.locators.multiSelectTrigger).click();
  }

  async rowWithNameExists(name: string): Promise<boolean> {
    const rowSelector = this.tableActionsLocators.rowByName(name);
    return (await this.page.locator(rowSelector).count()) > 0;
  }

  async checkRowExists(name: string) {
    await this.searchConnection(name);
    const selector = this.tableActionsLocators.rowByName(name);
    await expect(this.page.locator(selector)).toBeVisible();
  }

  async waitForRowToNotExist(connectorName) {
    let attempts = 0;
    const maxAttempts = 3; // Maximum number of attempts
    const interval = 1000;  // Wait 1 second between attempts

    while (attempts < maxAttempts) {
      const rowExists = await this.rowWithNameExists(connectorName);
      if (!rowExists) {
        return; // If the row does not exist, exit the function
      }
      await this.page.waitForTimeout(interval); // Wait for a bit before retrying
      attempts++;
    }
    throw new Error(`Row with name ${connectorName} still exists after ${maxAttempts} attempts`);
  }

  async clickButtonOnRowByName(name: string, buttonType: keyof typeof this.tableActionsLocators) {
    // Click a button on a row with matching name
    await this.searchConnection(name);
    const buttonSelector = `tr:has(td:has-text("${name}")) >> ${this.tableActionsLocators[buttonType]}`;
    await this.page.locator(buttonSelector).click();
  }

  async replaceName(name: string) {
    await this.page.locator(this.locators.awsName).fill(name);
  }

  async awsFillRequiredFields(name: string = 'test') {
    await this.page.locator(this.locators.awsName).fill(name);
    await this.page.locator(this.locators.awsKey).fill(connectorSecrets.awsS3.connectionDetails.accessKey);
    await this.page.locator(this.locators.awsSecret).fill(connectorSecrets.awsS3.connectionDetails.secretAccessKey);
  }

  async azureFillRequiredFields(name: string = 'test') {
    await this.page.locator(this.locators.awsName).fill(name);
    await this.page.locator(this.locators.connectionString).fill(connectorSecrets.azureBlobFiles.connectionString);
  }

  async boxFillrequiredFields(name: string = 'test') {
    await this.page.locator(this.locators.awsName).fill(name);
    await this.page.locator(this.locators.userID).fill(connectorSecrets.box.userId);
    await this.page.locator(this.locators.enterpiseID).fill(connectorSecrets.box.enterpriseID);
    await this.page.locator(this.locators.clientID).fill(connectorSecrets.box.boxAppSettings.clientID);
    await this.page.locator(this.locators.clientSecret).fill(connectorSecrets.box.boxAppSettings.clientSecret);
    await this.page.locator(this.locators.publicKeyID).fill(connectorSecrets.box.boxAppSettings.appAuth.publicKeyID);
    await this.page.locator(this.locators.privateKey).fill(connectorSecrets.box.boxAppSettings.appAuth.privateKey);
    await this.page.locator(this.locators.passphrase).fill(connectorSecrets.box.boxAppSettings.appAuth.passphrase);
  }

  async smbFillRequiredFields(name: string = 'test') {
    await this.page.locator(this.locators.awsName).fill(name);
    await this.page.locator(this.locators.usernameSnake).fill(connectorSecrets.cifs.userName);
    await this.page.locator(this.locators.password).fill(connectorSecrets.cifs.password);
    await this.page.locator(this.locators.domain).fill(connectorSecrets.cifs.domain);
    await this.page.locator(this.locators.host).fill(connectorSecrets.cifs.host);
    await this.page.locator(this.locators.port).fill(connectorSecrets.cifs.port);
    await this.page.locator(this.locators.path)
  }

  async confluenceCloudFillRequiredFields(name: string = 'test') {
    await this.page.locator(this.locators.awsName).fill(name);
    await this.page.locator(this.locators.username).fill(connectorSecrets.confluenceCloud.connectionDetails.username);
    await this.page.locator(this.locators.apiToken).fill(connectorSecrets.confluenceCloud.connectionDetails.apiToken);
    await this.page.locator(this.locators.domain).fill(connectorSecrets.confluenceCloud.connectionDetails.domain);
  }

  async googleDriveFillRequiredFields(name: string = 'test') {
    await this.page.locator(this.locators.awsName).fill(name);
    await this.page.locator(this.locators.userIDSnake).fill(connectorSecrets.googleDrive.userId);
    await this.page.locator(this.locators.clientEmail).fill(connectorSecrets.googleDrive.clientEmail);
    await this.page.locator(this.locators.domain).fill(connectorSecrets.googleDrive.domain);
    await this.page.locator(this.locators.privateKey).fill(connectorSecrets.googleDrive.privateKey);
    await this.page.locator(this.locators.user).click();
    await this.page.locator(this.locators.userOptions).first().click();
  }

  async ldapFillRequiredFields(name: string = 'test') {
    await this.page.locator(this.locators.alias).fill(name);
    await this.page.locator(this.locators.adminEmail).fill(connectorSecrets.ldap.administratorName);
    await this.page.locator(this.locators.adminPassword).fill(connectorSecrets.ldap.administratorPassword);
    await this.page.locator(this.locators.host).fill(connectorSecrets.ldap.host);
  }

  async oneDriveFillRequiredFields(name: string = 'test') {
    await this.page.locator(this.locators.alias).fill(name);
    await this.page.locator(this.locators.adminUserID).fill(connectorSecrets.oneDrive.adminUserId);
    await this.page.locator(this.locators.tenantID).fill(connectorSecrets.oneDrive.tenantId);
    await this.page.locator(this.locators.clientIDSnake).fill(connectorSecrets.oneDrive.clientId);
    await this.page.locator(this.locators.clientSecret).fill(connectorSecrets.oneDrive.clientSecret);
  }

  async sharepointOnPremiseFillRequiredFields(name: string = 'test') {
    await this.page.locator(this.locators.alias).fill(name);
    await this.page.locator(this.locators.domain).fill(connectorSecrets.sharepointOnPremise.domain);
    await this.page.locator(this.locators.username).fill(connectorSecrets.sharepointOnPremise.username);
    await this.page.locator(this.locators.password).fill(connectorSecrets.sharepointOnPremise.password);
  }

  async sharepointOnlineFillRequiredFields(name: string = 'test') {
    await this.page.locator(this.locators.alias).fill(name);
    await this.page.locator(this.locators.tenantID).fill(connectorSecrets.sharepointOnline.tenantId);
    await this.page.locator(this.locators.clientIDSnake).fill(connectorSecrets.sharepointOnline.clientId);
    await this.page.locator(this.locators.clientSecret).fill(connectorSecrets.sharepointOnline.clientSecret);
  }
}
