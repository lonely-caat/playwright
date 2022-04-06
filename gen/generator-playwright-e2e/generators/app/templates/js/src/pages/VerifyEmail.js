const { linkByText } = require('../helpers/selectors');

const VerifyEmail = (page) => {
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

module.exports = VerifyEmail;
