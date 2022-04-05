import { Selector, t } from 'testcafe';
import { getByText } from '@testing-library/testcafe';
import dotenv from 'dotenv-safe';

dotenv.config();

class CompleteOrderPage {
  constructor() {
    this.mainHeader = Selector('zip-content-heading');
    this.receiptNumber = Selector('#order-confirmed-receipt-number');
    this.orderComplete = Selector('#order-confirmed-receipt-number+ p');
  }

  async customerReceivedItem() {
    await t.click(getByText('Customer received item'));
  }
}

export default new CompleteOrderPage();
