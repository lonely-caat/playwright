import { Page } from 'playwright';
import { ZipProduct } from '../constants/picker';

const AccountSelector = (page: Page) => {
  const elements = {
    header: '//app-account-selector//h1[1]',
  };

  const productByName = (product: ZipProduct) => `//*[@class="product" and contains(text(),"${product}")]`;

  async function selectProduct(product: ZipProduct) {
    const el = productByName(product);
    await page.click(el);
  }

  async function getHeaderText() {
    const { header } = elements;
    return page.innerText(header);
  }

  return {
    selectProduct,
    getHeaderText,

  };
};

export default AccountSelector;
