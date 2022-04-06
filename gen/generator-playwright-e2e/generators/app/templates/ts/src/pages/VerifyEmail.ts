import { Page } from 'playwright';
import { linkByText } from '../helpers/selectors';

const VerifyEmail = (page: Page) => {
  const elements = {
    skipLink: linkByText('Skip for now'),
  };

  async function skipVerifyEmail() {
    const { skipLink } = elements;
    await page.click(skipLink);
  }

  return {
    skipVerifyEmail,
  };
};

export default VerifyEmail;
