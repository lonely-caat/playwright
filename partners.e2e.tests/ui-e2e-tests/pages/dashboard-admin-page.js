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

class DashboardAdminPage {
  async reportTile() {
    const reports = getElementsByXPath('//p[contains(text(), "Reports")]')
    await t.click(reports);
  }

  async userManagementTile() {
    const user_management = getElementsByXPath('//p[contains(text(), "User Management")]')
    await t.click(user_management);
  }

  async posMaterialsTile() {
    const pos_materials = getElementsByXPath('//p[contains(text(), "POS Materials")]')
    await t.click(pos_materials);
  }

  async storeProfileTile() {
    const store_profile = getElementsByXPath('//p[contains(text(), "Store Profile")]')
    await t.click(store_profile);
  }

  async storeCategoriesTile() {
    const store_categories = getElementsByXPath('//p[contains(text(), "Store Categories")]')
    await t.click(store_categories);
  }

  async notificationsPageTile() {
    const store_categories = getElementsByXPath('//p[contains(text(), "Notification Settings")]')
    await t.click(store_categories);
  }
}

export default new DashboardAdminPage();
