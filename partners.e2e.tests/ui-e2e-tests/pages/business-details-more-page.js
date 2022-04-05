import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';
import { getByText } from '@testing-library/testcafe';

dotenv.config();

class BusinessDetailsMorePage {
  constructor() {
    this.dropDown = Selector('.mat-option');
    this.payment = Selector('#mat-select-0');
    // this.annualSales = Selector('mat-label').withText('Total online sales per year');
    this.annualSales = Selector('#mat-select-1');
    this.annualSales250k = Selector('span').withText('$0 - $250,000');
    this.annualSales500k = Selector('span').withText('$250,000 - $500,000');
    this.annualSales1kk = Selector('span').withText('$500,000 - $1 Million')
    this.annualSales5kk = Selector('span').withText('$1M - $5M')
    this.annualSales10kk = Selector('span').withText('$5M - $10M')


    this.onlineShippingTime = Selector('#mat-select-2');
    this.transactionValue = Selector('.mat-select-placeholder');
    this.averageTransaction = Selector('#mat-select-4');
  }

  async instoreOnlineSales(selector=this.annualSales1kk,
                           platform_selector = 'Shopify',
                           websiteUrl = process.env.WEBSITE_URL,
  ) {
    await t
      .click(this.payment)
      .click(this.dropDown.withText('Both (online & instore)'))
      .click(this.annualSales)
      .click(selector)
      .click(this.onlineShippingTime)
      .click(this.dropDown.withText('8-14 days'))
      .click(this.transactionValue)
      .click(this.dropDown.withText('$1000 - $3000'))
      .click(this.averageTransaction)
      .click(this.dropDown.withText(platform_selector))
      .typeText(Selector('#mat-input-7'), websiteUrl)
      .click(getByText('Continue'));
  }

  async onlineSales(selector=this.annualSales1kk,
                           platform_selector = 'Shopify',
                           websiteUrl = process.env.WEBSITE_URL,
  ) {
    await t
      .click(this.payment)
      .click(this.dropDown.withText('Online payments'))
      .click(this.annualSales)
      .click(selector)
      .click(this.onlineShippingTime)
      .click(this.dropDown.withText('15-30 days'))
      .click(this.transactionValue)
      .click(this.dropDown.withText('$3000+'))
      .click(this.averageTransaction)
      .click(this.dropDown.withText(platform_selector))
      .typeText(Selector('#mat-input-7'), websiteUrl)
      .click(getByText('Continue'));
  }

  async instoreSales(selector = this.annualSales5kk) {
    await t
      .click(this.payment)
      .click(this.dropDown.withText('Instore payments'))
      .click(this.annualSales)
      .click(selector)
      .click(this.transactionValue)
      .click(this.dropDown.withText('$0 - $1000'))
      .click(getByText('Continue'));
  }
}

export default new BusinessDetailsMorePage();
