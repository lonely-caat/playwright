import { expect } from '@playwright/test';
import { BasePage } from './base.page';

export class ExplorePage extends BasePage {
    private locators = {
        tableRows: '//div[contains(@class, "p-datatable-scrollable")]//tr[@class="table-row ng-star-inserted"]',
        clearSearch: '//button[@label="Clear search"]',
        gQLInput: '//input[@placeholder="Find everything using GQL"]',
        gQLSuggestion: (label: string) => `//gv-gql-query-suggestion//span[text()="${label}"]`,
        modeCheckbox: '//span[@class="p-inputswitch-slider"]',
        searchButton: '//button[@icon="pi pi-search"]',
        basicFilterMulti: (placeholder: string) => `//gv-shared-ui-multi-select-filter[@placeholder="${placeholder}"]`,
        basicFilterMultiOption: (option: string) => `//p-multiselectitem//span[text()="${option}"]`,
        basicFilterMultiOptions: `//p-multiselectitem//span`,
        basicFilter: (placeholder: string) => `//gv-shared-ui-select-filter[@placeholder="${placeholder}"]`,
        basicFilterOption: (option: string) => `//div[@class="p-dropdown-items-wrapper"]//li[@aria-label="${option}"]`,
        firstColumnExpander: '//i[text()="more_vert"]',
        columnAvailabilityDropDown: (name: string) => `//div[@class="p-menuitem-content"]//span[text()="${name}"]`,
        columnByName: (name: string) => `//thead//th//span[text()="${name}"]`,
        edit: '//button[@ptooltip="Manually verify ML classification"]',
        permissions: '//span[@ptooltip="Open the permission management for this file"]',
        saveModal: '//button[@label="Save"]',
        checkbox: '//div[@class="p-checkbox-box"]',
        apply: '//button[@label="Apply"]',
        star: '//i[@ptooltip="Save GQL search"]',
    };

    async checkColumnNameDisplayed(name: string) {
        await expect(this.page.locator(this.locators.columnByName(name))).toBeAttached();
    }

    async editFirstClassification() {
        await this.page.locator(this.locators.edit).first().click();
        await expect(this.page.locator('//div[@role="dialog"]//div[text()=" Category "]')).toBeVisible();
        await expect(this.page.locator('//div[@role="dialog"]//div[text()=" Subcategory "]')).toBeVisible();
        await expect(this.page.locator('//div[@role="dialog"]//div[text()=" Compliance "]')).toBeVisible();
        await expect(this.page.locator('//div[@role="dialog"]//div[text()=" Classification "]')).toBeVisible();
        await this.page.locator(this.locators.saveModal).click();
        // TODO: remove this timeout after fixing the issue with SDM ENG-1498
        await expect(this.page.getByText('File was successfully classified')).toBeVisible({ timeout: 20000 });
    }

    async returnTablePaths() {
        const pathsList = [];
        const pathsCount = await this.page.locator('//gv-results-table//td//gv-file-path-cell').count();
        for (let i = 0; i < pathsCount; i++) {
            pathsList.push(await this.page.locator('//gv-results-table//td//gv-file-path-cell').nth(i).textContent());
        }
        return pathsList;

        // return await this.page.locator('//gv-results-table//td//gv-file-path-cell').first().textContent()
    }

    async viewFirstPermissions() {
        await this.page.locator(this.locators.permissions).first().click();
        await expect(this.page.locator('//thead//span[text()="SID"]')).toBeVisible();
        await expect(this.page.locator('//thead//span[text()="Display name"]')).toBeVisible();
        await expect(this.page.locator('//thead//span[text()="Externally shared"]')).toBeVisible();
        await expect(this.page.locator('//thead//span[text()="Permissions"]')).toBeVisible();
    }

    async changeColumnVisibility(name: string) {
        await this.page.locator(this.locators.firstColumnExpander).first().click();
        // somehow rows need to be pressed twice, bug or feature?
        await this.page.locator(this.locators.columnAvailabilityDropDown(name)).click();
        await this.page.locator(this.locators.columnAvailabilityDropDown(name)).click();

        await this.checkColumnNameDisplayed(name);
    }

    async basicFilterMultiSelect(placeholder: string, option: string[]) {
        await this.page.locator(this.locators.basicFilterMulti(placeholder)).click();
        for (let element of option) {
            await this.page.locator(this.locators.basicFilterMultiOption(element)).click();
        }
    }

    async basicFilterSelectFirstOption(placeholder: string) {
        await this.page.locator(this.locators.basicFilterMulti(placeholder)).click();
        await this.page.locator(this.locators.basicFilterMultiOptions).first().click();
    }

    async basicFilterSelect(placeholder: string, option: string) {
        await this.page.locator(this.locators.basicFilter(placeholder)).click();
        await this.page.locator(this.locators.basicFilterOption(option)).click();
    }

    async isGQLSuggestionDisplayed(label: string): Promise<void> {
        const suggestionLocator = this.locators.gQLSuggestion(label);
        await expect(this.page.locator(suggestionLocator)).toBeVisible({ timeout: 5000 });
    }

    async search() {
        await this.page.locator(this.locators.searchButton).click();
    }

    async assertQueryHelperMessage(message: string): Promise<void> {
        await expect(this.page.locator(`text=${message}`)).toBeVisible();
    }

    async clickGQLSuggestion(label: string): Promise<void> {
        await this.isGQLSuggestionDisplayed(label);
        const suggestionLocator = this.locators.gQLSuggestion(label);
        await this.page.locator(suggestionLocator).click();
    }

    async inputGQL(query: string) {
        await this.page.locator(this.locators.gQLInput).click()
        if (query) { await this.page.locator(this.locators.gQLInput).fill(query) }
    }

    async hasAtLeastOneRow(): Promise<boolean> {
        return (await this.page.locator(this.locators.tableRows).count()) > 0;
    }

    async appendTextToGQLInput(text: string) {
        await this.page.focus(this.locators.gQLInput);
        await this.page.type(this.locators.gQLInput, text, { delay: 100 })
    }

    async clearSearch() {
        await this.page.locator(this.locators.clearSearch).click();
    }

    async verifyGQLEmpty() {
        const innerText = await this.page.locator(this.locators.gQLInput).innerText();
        expect(innerText).toBe('');
    }

    async checkModeCheckbox() {
        await this.page.waitForSelector(this.locators.modeCheckbox, { state: 'visible' });
        await this.page.locator(this.locators.modeCheckbox).click({ delay: 100 });
    }

    async checkFirstCheckbox() {
        await this.page.locator(this.locators.checkbox).nth(0).click();
    }

    async verifySendToClassification() {
        await this.checkFirstCheckbox();
        await this.page.getByText('Send to classification pipeline').isVisible();
        await this.page.locator(this.locators.apply).click();
        await this.page.getByText('Files were sent for classification').isVisible();
    }

    async addGQLFilterToFavorite(filterName: string) {
        await this.page.locator(this.locators.star).click();
        await this.page.getByPlaceholder('Enter GQL query description').fill(filterName);
        await this.page.locator(this.commonLocators.acceptButton).click();
        await this.page.getByText('Query has been saved').isVisible();
    }
}
