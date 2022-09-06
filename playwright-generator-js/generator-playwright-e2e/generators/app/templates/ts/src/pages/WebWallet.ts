import { Page } from 'playwright';

const WebWallet = (page: Page) => {
  const elements = {
    header: '//zip-profile-overview',
  };

  async function getHeaderText() {
    const { header } = elements;
    const headerText = await page.innerText(header);
    return headerText;
  }

  return {
    getHeaderText,
  };
};

export default WebWallet;
