import { BasePage } from './base.page';
import { ExplorePage } from './explore.page';
import { th } from "@faker-js/faker";

type Settings = 'Data settings' | 'Widget settings';
type Position = 'Top' | 'Bottom';
type Widget = 'Counter' | 'Chart' | 'Text' | 'Table';


export class AnalyticsPage extends BasePage {

  private locators = {
    searchBoards: '//input[@placeholder="Search"]',
    createNewBoard: '//span[text()="Create new Board"]',
    selectBoard: (boardName: string) => `//menu//gv-shared-ui-page-navigation-item//a[text()=" ${boardName} "]`,
    boardName: '//input[@placeholder="Board name"]',
    boardDescription: '//textarea[@placeholder="Board description"]',
    layouts: '//gv-shared-ui-choose-layout//gv-shared-ui-layout-icon',
    edit: '//span[text()="edit"]',
    export: '//span[text()="export"]',
    addWidget: '//span[text()="Add widget"]',
    widgetTypeDropdown: '//p-dropdown[@placeholder="Select widget type"]',
    widgetTypeValue: (value: string) => `//li[@aria-label="${value}"]`,
    datasetRadio: (dataset: string) => `//gv-shared-ui-form-label//label[text()=" ${dataset} "]`,
    aggregateTypeDropdown: '//p-dropdown[@formcontrolname="aggregate"]',
    aggregateTypeValue: (value: string) => `//p-dropdownitem//li//span[text()="${value}"]`,
    settingsType: (setting: string) => `//a[@role="tab"]//span[text()="${setting}"]`,
    title: '//input[@formcontrolname="label"]',
    positionDropDown: "//p-dropdown[@formcontrolname='position']",
    positionValue: (value: string) => `//p-dropdownitem//span[text()="${value}"]`,

    resultLimit: '//p-inputnumber[@formcontrolname="limit"]//input',
    thresholdCount: '//p-inputnumber[@formcontrolname="count"]//input',
    interval: '//p-inputnumber[@formcontrolname="interval"]',
    chartTitles: '//gv-widgets-shared-title-settings[@formcontrolname="title"]//input',
    marginLeft: '//p-inputnumber[@formcontrolname="left"]//input',
    marginRight: '//p-inputnumber[@formcontrolname="right"]//input',
    marginTop: '//p-inputnumber[@formcontrolname="top"]//input',
    marginBottom: '//p-inputnumber[@formcontrolname="bottom"]//input',
    flipCheckbox: '//label[@class="gv-form-label"][@for="flip"]',
    chartCheckbox: "//label[text()='Show grid']/../following-sibling::p-checkbox//div[contains(@class, 'p-checkbox-box')]",

    textBox: '//span[text()="Type something"]',

    viewInPage: '//span[text()="View in the page"]',

    exportLimit: '//p-inputnumber[@formcontrolname="exportLimit"]//input',
    groupBy: '//p-dropdown[@formcontrolname="field"]',
    groupByValue: (value: string) => `//div[@class="p-dropdown-items-wrapper"]//li[contains(., "${value}")]`,
    sortDropDown: '//p-dropdown[@formcontrolname="field"]',
    sortField: '//p-inputnumber[@formcontrolname="field"]',
    sortOptions: (value: string) => `//li[@role="option"]//span[text()="${value}"]`,
    visibleColumns: '//p-multiselect[@formcontrolname="columns"]',
    columns: (value: string) => `//div[@class="p-multiselect-items-wrapper"]//li//span[text()="${value}"]`,
  };

  async createCustomBoardStep1(boardName: string, layout: number, boardDescription?: string) {
    await this.page.locator(this.locators.createNewBoard).click();
    await this.page.locator(this.locators.boardName).fill(boardName);
    if (boardDescription) { await this.page.locator(this.locators.boardDescription).fill(boardDescription) }
    await this.page.locator(this.locators.layouts).nth(layout).click();
    await this.page.locator(this.commonLocators.acceptButton).click();
  }

  async searchForBoard(boardName: string) {
    await this.page.locator(this.locators.searchBoards).fill(boardName);
    await this.page.locator(this.locators.selectBoard(boardName)).first().click();
  }

  async editCustomBoard(boardName: string) {
    await this.page.locator(this.locators.edit).click();
    await this.page.locator(this.locators.addWidget).click();
  }

  async selectWidgetType(widgetType: Widget) {
    await this.page.locator(this.locators.widgetTypeDropdown).click();
    await this.page.locator(this.locators.widgetTypeValue(widgetType)).click();
  }

  private async selectAggregateType(aggregateType: string) {
    await this.page.locator(this.locators.aggregateTypeDropdown).click();
    await this.page.locator(this.locators.aggregateTypeValue(aggregateType)).click();
  }

  private async selectSettingsType(setting: Settings) {
    await this.page.locator(this.locators.settingsType(setting)).click();
  }

  async addCounterWidget(dataSet: string, aggregationType: string, title: string, position: Position) {
    await this.page.locator(this.locators.datasetRadio(dataSet)).click();
    await this.selectSettingsType('Widget settings');
    await this.page.locator(this.locators.title).fill(title);
    await this.selectPosition(position);
    await this.saveWidget();

  }

  async addChartWidget(dataSet: string, sortField: string, title: string, subtitle: string, position: Position) {
    await this.page.locator(this.locators.datasetRadio(dataSet)).click();
    await this.page.locator(this.locators.groupBy).click();
    await this.page.locator(this.locators.resultLimit).fill('8');
    await this.page.locator(this.locators.thresholdCount).fill('12');

    await this.selectSettingsType('Widget settings');
    await this.page.locator(this.locators.chartTitles).first().fill(title);
    await this.page.locator(this.locators.chartTitles).last().fill(subtitle);
    await this.selectPosition(position);

    await this.page.locator(this.locators.marginLeft).fill('28');
    await this.page.locator(this.locators.marginRight).fill('14');
    await this.page.locator(this.locators.marginTop).fill('20');
    await this.page.locator(this.locators.marginBottom).fill('50');

    await this.page.locator(this.locators.flipCheckbox).click();
    await this.page.locator(this.locators.chartCheckbox).click();
    await this.saveWidget();

  }

  async addTableWidget(dataSet: string, visibleColumns: string[], sortField: string, title: string, subtitle: string, position: Position) {
    await this.page.locator(this.locators.datasetRadio(dataSet)).isVisible()
    await this.page.waitForTimeout(500);
    await this.page.locator(this.locators.datasetRadio(dataSet)).click();
    await this.page.locator(this.locators.resultLimit).fill('8');
    await this.page.locator(this.locators.exportLimit).fill('1000');
    await this.page.locator(this.locators.sortDropDown).click();
    await this.page.locator(this.locators.sortOptions(sortField));

    await this.page.locator(this.locators.visibleColumns).click();
    for (let column of visibleColumns) { await this.page.locator(this.locators.columns(column)).click(); }

    await this.selectSettingsType('Widget settings');
    await this.page.locator(this.locators.chartTitles).first().fill(title);
    await this.page.locator(this.locators.chartTitles).last().fill(subtitle);
    await this.selectPosition(position);
    await this.saveWidget();

  }

  async addTextWidget(text: string) {
    await this.page.locator(this.locators.textBox).type(text);
    await this.page.keyboard.press('Enter');
    await this.page.waitForTimeout(500);
    await this.saveWidget();
  }

  private async selectPosition(position: Position) {
    await this.page.locator(this.locators.positionDropDown)
    await this.page.locator(this.locators.positionValue(position))
  }

  async viewInPage() {
    await this.page.locator(this.locators.viewInPage).click();
  }

  private async saveWidget() {
    await this.page.locator(this.commonLocators.saveButton).click();
  }
}

