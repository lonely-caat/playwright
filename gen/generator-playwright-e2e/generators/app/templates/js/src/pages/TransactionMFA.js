const { DEBUG_MOBILE, DEBUG_SMS_CODE } = require('../constants/appData');
const actions = require('../helpers/actions');
const { buttonByText } = require('../helpers/selectors');

const TransactionMFA = (pageInstance, frameInstance = null) => {
  const page = frameInstance || pageInstance;
  const elements = {
    mobileField: '#mobilePhone',
    sendCodeButton: buttonByText('Send code'),
    codeField: '//input[@id="verificationCode" and not(contains(@disabled,"disabled"))]',
    continueButton: buttonByText('Verify & continue'),
    completeOrderButtonm: buttonByText('Complete order'),
  };
  const { waitForResponse, clickFrameElement } = actions(pageInstance, frameInstance);

  function isCheckout() {
    return page.url().includes('/retransaction');
  }

  async function fetchVerificationCode() {
    const { mobileField, sendCodeButton } = elements;
    await page.type(mobileField, DEBUG_MOBILE);
    const clickSendCode = () =>
      frameInstance ? clickFrameElement(sendCodeButton) : page.click(sendCodeButton);
    await Promise.all([clickSendCode(), waitForResponse('/api/smsVerification', { status: 200 })]);
  }
  /**
   * This comment _supports_ [Markdown](https://marked.js.org/)
   */
  async function verifyMobileCode() {
    const { codeField, completeOrderButtonm, continueButton } = elements;
    await fetchVerificationCode();
    await page.waitForSelector(codeField);
    await page.type(codeField, DEBUG_SMS_CODE);
    const buttonSelector = isCheckout() ? completeOrderButtonm : continueButton;
    const clickCompleteOrder = () =>
      frameInstance ? clickFrameElement(buttonSelector) : page.click(buttonSelector);
    await Promise.all([
      clickCompleteOrder(),
      waitForResponse('/api/smsVerification/verifycode', { status: 200 }),
    ]);
  }

  return {
    verifyMobileCode,
  };
};

module.exports = TransactionMFA;
