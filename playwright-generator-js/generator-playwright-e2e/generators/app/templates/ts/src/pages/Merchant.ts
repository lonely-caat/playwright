import { Page } from 'playwright';
import actions from '../helpers/actions';
import { byTagAndText } from '../helpers/selectors';

const Merchant = (page: Page) => {
  const { waitForResponse } = actions(page);

  async function waitForShippingPage() {
    return waitForResponse('/estimate-shipping-methods', {
      status: 200,
      message: 'Waited for shipping costs to load before continuing Luma checkout',
    });
  }

  async function addToCart() {
    await page.click('//*[@option-id="169"]');
    await page.click('//*[@option-label="Orange"]');
    await Promise.all([
      page.click('#product-addtocart-button'),
      waitForResponse('http://10.41.10.23/checkout/cart/add/', { status: 200 }),
    ]);
  }

  async function goToCheckoutPage() {
    const selector = byTagAndText('a', 'shopping cart');
    const goToCardElement = await page.waitForSelector(selector);
    await page.waitForTimeout(600);
    await Promise.all([
      goToCardElement.click(),
      waitForResponse('/checkout/shipping_method/price', {
        message: 'Waited for transaction total to settle before continuing Luma checkout',
      }),
    ]);
    await page.waitForTimeout(600);
    await page.click('//button[@data-role="proceed-to-checkout"]');
  }

  async function completeCheckoutForm() {
    await waitForShippingPage();
    await page.waitForTimeout(600);
    await page.type('#customer-email', 'QA@zip.co', { delay: 200 });
    await page.type('[name="firstname"]', 'QA');
    await page.type('[name="lastname"]', 'fake@gmail.com');
    await page.type('[name="street[0]"]', '10 Spring Street');
    await page.type('[name="city"]', 'Sydney');
    await page.selectOption('[name="region_id"]', { value: '570' });
    await page.type('[name="postcode"]', '2126');
    await page.type('[name="telephone"]', '0400000000');
    await page.waitForResponse('http://10.41.10.23/rest/au/V1/guest-carts/*/estimate-shipping-methods');
    await page.click('//*[@id="shipping-method-buttons-container"]//button');
    await page.click('#zippayment');
    await Promise.all([
      await page.click('//button[@title="Continue to ZipMoney"]'),
      waitForResponse('http://10.41.10.23/zippayment/standard/', { status: 200 }),
    ]);
  }

  async function performCheckout() {
    await addToCart();
    await goToCheckoutPage();
    await completeCheckoutForm();
  }

  async function getPageTitle() {
    return page.innerText('[data-ui-id="page-title-wrapper"]');
  }

  async function waitForPurchaseSuccess() {
    return waitForResponse('http://10.41.10.23/zippayment/complete/?result', {
      message: 'Waited for Luma checkout to succeed',
    });
  }

  return {
    performCheckout,
    getPageTitle,
    waitForPurchaseSuccess,

  };
};

export default Merchant;
