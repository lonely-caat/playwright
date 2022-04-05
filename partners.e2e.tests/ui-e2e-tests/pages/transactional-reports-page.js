/* eslint-disable class-methods-use-this */
/* eslint-disable no-undef */
import { Selector, t, ClientFunction } from 'testcafe';
import dotenv from 'dotenv-safe';


dotenv.config();

class TransactionalReportsPage {
  constructor() {
    this.mainHeader = Selector('h1');
    this.dateRangePicker = Selector('.MuiSelect-root')

    this.weeklyGranularity = Selector('button.MuiButtonBase-root').withText('Weekly')
    this.monthlyGranularity = Selector('button.MuiButtonBase-root').withText('Monthly')

    this.successTransactions = Selector('div.MuiGrid-grid-sm-6:nth-child(1)')
    this.netVolume = Selector('div.MuiGrid-grid-sm-6:nth-child(2)')
    this.grossVolume = Selector('div.MuiGrid-grid-sm-6:nth-child(3)')
    this.totalCustomers = Selector('div.MuiGrid-grid-sm-6:nth-child(4)')
    this.newCustomers = Selector('div.MuiGrid-grid-sm-6:nth-child(5)')

    this.dotOnChart = Selector('g.recharts-layer.recharts-line-dots > circle:nth-child(1)')
    this.lastDotOnChart = Selector('g.recharts-layer.recharts-line-dots > circle:nth-last-child(1)')
    this.dataForDotOnChart = Selector('span.recharts-tooltip-item-value')
    this.labelForDotOnChart = Selector('span.recharts-tooltip-item-name')

    this.successTransactionsHeader = this.successTransactions.child()

  }

  async verifyDotLabelValue(index, label, value, first= true, first_label = true) {
    if(first_label) {
      first ? await t.hover(this.dotOnChart.nth(index)) : await t.hover(this.lastDotOnChart.nth(index));
      await t.expect(this.labelForDotOnChart.textContent).eql(label);
      await t.expect(this.dataForDotOnChart.textContent).eql(value);
    }
    else {
      first ? await t.hover(this.dotOnChart.nth(index)) : await t.hover(this.lastDotOnChart.nth(index));
      await t.expect(this.labelForDotOnChart.nth(1).textContent).eql(label);
      await t.expect(this.dataForDotOnChart.nth(1).textContent).eql(value);
    }
  }



  async dateRangeDropDown(option) {
    await t.click(this.dateRangePicker)
    // option=option.toLowerCase();
    switch (option) {
      case 'today':
        await t.click(Selector('li').withText('Today'))
        break;
      case 'sevenDays':
        await t.click(Selector('li').withText('Last 7 days'))
        break;
      case 'thirtyDays':
        await t.click(Selector('li').withText('Last 30 days'))
        break;
      case 'threeMonths':
        await t.click(Selector('li').withText('Last 3 months'))
        break;
      case 'twelveMonths':
        await t.click(Selector('li').withText('Last 12 months'))
        break;
      case 'monthDate':
        await t.click(Selector('li').withText('Month to date'))
        break;
      case 'quarterDate':
        await t.click(Selector('li').withText('Quarter to date'))
        break;
      case 'fyDate':
        await t.click(Selector('li').withText('FY to date'))
        break;
      case 'yearDate':
        await t.click(Selector('li').withText('Year to date'))
        break;



    }
  }
}

export default new TransactionalReportsPage();
