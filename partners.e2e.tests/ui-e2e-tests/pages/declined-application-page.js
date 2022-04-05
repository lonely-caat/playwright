/* eslint-disable class-methods-use-this */
import { Selector } from 'testcafe';
import dotenv from 'dotenv-safe';

dotenv.config();

class DeclinedApplicationPage {
  constructor() {
    this.mainHeader = Selector('h2');
    this.declineContent = Selector('p');
    this.homePageLink = Selector('.link');
  }
}

export default new DeclinedApplicationPage();
