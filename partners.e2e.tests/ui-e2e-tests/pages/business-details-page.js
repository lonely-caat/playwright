import { Selector, t } from 'testcafe';
import dotenv from 'dotenv-safe';
import { getByText, getByPlaceholderText } from '@testing-library/testcafe';
import helper from '../utils/helper';
import * as constants from '../data/constants';

dotenv.config();

class BusinessDetailsPage {
  constructor() {
    this.industryType = Selector('#industryInput');
    this.industryTypeOption = Selector('span').withText('Computers');
    this.IndustrytypeOptionTattoo = Selector('span').withText('Tattooists');
    this.IndustrytypeOptionPiercing = Selector('span').withText('Piercing');
    this.IndustrytypeOptionCruise = Selector('span').withText('Cruise');
    this.IndustrytypeOptionHairdressers = Selector('span').withText('Hairdressers');



  }

  async businessDetails(industry, abn = constants.partnerDetails.abn) {
    const populateTradingName = await this.tradingName(industry);
    await t
      .typeText(getByPlaceholderText('Your 11 digit ABN'), abn)
      .typeText(getByPlaceholderText('Your trading name'), populateTradingName);

    switch (industry){
      case 'computers':
        await t.click(this.industryType).click(this.industryTypeOption);
        break;
      case 'piercing':
        await t.click(this.industryType).click(this.IndustrytypeOptionPiercing());
        break;
      case 'tattoo':
        await t.click(this.industryType).click(this.IndustrytypeOptionTattoo());
        break;
      case 'travel':
        await t.click(this.industryType).click(this.IndustrytypeOptionCruise());
        break;
      case 'hairdressers':
        await t.click(this.industryType).click(this.IndustrytypeOptionHairdressers());
        break;
    }
    await t.click(getByText('Continue'));
  }

  async tradingName(status) {
    const tradingBusinessName = await helper.getRandomString(5);
    return `Automation${status}${tradingBusinessName}`;
  }
}

export default new BusinessDetailsPage();
