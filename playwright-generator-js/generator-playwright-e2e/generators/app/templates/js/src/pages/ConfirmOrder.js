const actions = require('../helpers/actions');
const { buttonByText } = require('../helpers/selectors');

const ConfirmOrder = (pageInstance, frameInstance = null) => {
  const page = frameInstance || pageInstance;
  const { clickFrameElement } = actions(pageInstance, frameInstance);

  async function selectContinue() {
    await page.waitForSelector('//h1[contains(text(),"Confirm order")]');
    if (frameInstance) {
      await clickFrameElement(buttonByText('Continue'));
    } else {
      await page.click(buttonByText('Continue'));
    }
  }
  return {
    selectContinue,
  };
};

module.exports = ConfirmOrder;
