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

class StoreCategoriesPage {
  constructor() {
    this.storeCategoriesTitle = getElementsByXPath('//h1[@class="section-title"]');
    this.allCategoriesDrpDwns = Selector('li')
    this.allVisibleCategoriesDrpDwns = getElementsByXPath('//li[@ng-class="{open: childCategory.isOpen}"]')
    this.allCategoriesChoices = getElementsByXPath('//span[@ng-class="{selected: grandChildCategory.isSelected}"]')
    this.allSelectedCategoriesChoices = getElementsByXPath('//span[@class="selected"]')

    this.saveBtn = getElementsByXPath('//button[@ng-click="save()"]')
  }
}

export default new StoreCategoriesPage();