const AccountSelector = (page) => {
  const elements = {
    header: '//app-account-selector//h1[1]',
  };

  const productByName = (product) => `//*[@class="product" and contains(text(),"${product}")]`;

  async function selectProduct(product) {
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

module.exports = AccountSelector;
