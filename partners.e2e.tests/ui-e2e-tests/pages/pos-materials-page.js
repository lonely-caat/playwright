/* eslint-disable class-methods-use-this */
import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';

dotenv.config();

const getElementsByXPath = Selector(xpath => {
  const iterator = document.evaluate(xpath, document, null, XPathResult.UNORDERED_NODE_ITERATOR_TYPE, null);
  const items = [];

  let item = iterator.iterateNext();

  while (item) {
    items.push(item);
    item = iterator.iterateNext();
  }

  return items;
});

class PosMaterialsPage {
  constructor() {
    this.posMaterialsTitle = getElementsByXPath('//h1[@class="section-title"]');
    this.orderBtn = getElementsByXPath('//a[contains(text(), "Order")]');
    this.orderBtn2 = Selector('a.md-primary')

  }
}

export default new PosMaterialsPage();