import { Frame, Page } from 'playwright';
import { DEBUG_MOBILE, DEBUG_SMS_CODE } from '../constants/appData';
import actions from '../helpers/actions';
import { buttonByText } from '../helpers/selectors';

const TransactionMFA = (p: Page, f: Frame | null = null) => {
  const page = f || p;
  const elements = {
    mobileField: '#mobilePhone',
    sendCodeButton: buttonByText('Send code'),
    codeField: '//input[@id="verificationCode" and not(contains(@disabled,"disabled"))]',
    continueButton: buttonByText('Verify & continue'),
    completeOrderButtonm: buttonByText('Complete order'),
  };
  const { waitForResponse, clickFrameElement } = actions(p, f);

  function isCheckout() {
    return page.url().includes('/retransaction');
  }

  async function fetchVerificationCode() {
    const { mobileField, sendCodeButton } = elements;
    await page.type(mobileField, DEBUG_MOBILE);
    const clickSendCode = () => (f
      ? clickFrameElement(sendCodeButton)
      : page.click(sendCodeButton));
    await Promise.all([
      clickSendCode(),
      waitForResponse('/api/smsVerification', { status: 200 }),
    ]);
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
    const clickCompleteOrder = () => (f
      ? clickFrameElement(buttonSelector)
      : page.click(buttonSelector));
    await Promise.all([
      clickCompleteOrder(),
      waitForResponse('/api/smsVerification/verifycode', { status: 200 }),
    ]);
  }

  return {
    verifyMobileCode,
  };
};

export default TransactionMFA;
