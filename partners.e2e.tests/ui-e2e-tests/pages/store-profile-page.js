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

class StoreProfilePage {
  constructor() {
    this.storeProfileTitle = getElementsByXPath('//h1[@class="section-title"]');
    this.displayNameInput = getElementsByXPath('//input[@id="displayName"]');
    this.companyUrl = getElementsByXPath('//input[@id="merchantUrl"]');
    this.companyShortDiscription = getElementsByXPath('//textarea[@id="input_1"]');
    this.companyLongDiscription = getElementsByXPath('//textarea[@id="input_2"]');
    this.storeOnlineCkbx = getElementsByXPath('//md-checkbox[@aria-label="Online"]')
    this.storeOfflineCkbx = getElementsByXPath('//md-checkbox[@aria-label="In-store"]')

    this.imageCancel = getElementsByXPath('//button[@track-label="Cancel"]')
    this.imageUploadBtn = getElementsByXPath('//button[@track-label="Select A File"]')
    this.imageContinueBtn = getElementsByXPath('//button[@track-label="Continue"]')

    this.imageInput = getElementsByXPath('//input[@type="file"]')

    this.imageEditTileBtn = getElementsByXPath('//button[@track-label="Edit Store Tile"]')
    this.imageNextBtn = getElementsByXPath('//button[@track-label="Next"]')
    this.imageDoneBtn = getElementsByXPath('//button[@track-label="Done"]')


    this.submitUpdateBtn = getElementsByXPath('//button[@id="updateProfileBtn"]')
    this.submitProfileBtn = getElementsByXPath('//button[@id="submitProfileBtn"]')



  }
}

export default new StoreProfilePage();