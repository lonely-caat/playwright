import { Frame, Page } from 'playwright';
import actions from '../helpers/actions';
import { buttonByText } from '../helpers/selectors';

const ConfirmOrder = (p: Page, f: Frame | null = null) => {
  const page = f || p;
  const { clickFrameElement } = actions(p, f);

  async function selectContinue() {
    await page.waitForSelector('//h1[contains(text(),"Confirm order")]');
    if (f) {
      await clickFrameElement(buttonByText('Continue'));
    } else {
      await page.click(buttonByText('Continue'));
    }
  }

  return {
    selectContinue,
  };
};

export default ConfirmOrder;
