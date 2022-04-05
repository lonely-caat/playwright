import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';
import { getByPlaceholderText } from '@testing-library/testcafe';


dotenv.config();

class Helper {
  createUserEmail(application) {
    const timeStamp = new Date().getTime();
    return `${application}_${timeStamp}_automation@zipteam222898.testinator.com`;
  }

  async getRandomString(characterLength) {
    let randomText = '';
    const possible = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
    for (let i = 0; i < characterLength; i += 1) {
      randomText += possible.charAt(Math.floor(Math.random() * possible.length));
    }
    return randomText;
  }

  async populateAddressAutoComplete(addressLookup, pickAddress) {
    await t.typeText(addressLookup, '12 Black Street Brighton');
    await t.click(addressLookup);
    await t.wait(2000);
    await t.click(pickAddress.withText('Brighton'));
  }

  async populateAddressManual() {
    await t.click(Selector('#manualAddressTrigger'))
    await t.typeText(Selector('#addressLineOne'), '12 Black Street Brighton');
    await t.typeText(Selector('#suburb'), 'Meowtown');
    await t.click(Selector('label').withText('State'));
    await t.click(Selector('span').withText('NSW'));
    await t.typeText(Selector('#postcode'), '4321');
  }

  async populateDirectorAddressManual() {
    await t.click(Selector('#manualAddressTrigger'))
    await t.typeText(Selector('#streetNumber'), '12');
    await t.typeText(Selector('#streetName'), 'Black Street Brighton');
    await t.typeText(Selector('#suburb'), 'Meowtown');
    await t.click(Selector('#mat-select-2'));
    await t.click(Selector('span').withText('NSW'));
    await t.typeText(Selector('#postcode'), '4321');
  }

  async createEmail(product) {
  /**
  * Returns a mailinator email based on current timestamp
  */
    const timeStamp = new Date().getTime();
    return `${product}_${timeStamp}_automation@zipteam222898.testinator.com`;
  }
  /**
   * Returns a random number between min (inclusive) and max (exclusive)
   */
  async getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }
}

export default new Helper();
