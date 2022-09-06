import { Frame, Page } from 'playwright';
import { DEBUG_SMS_CODE } from '../constants/appData';
import actions from '../helpers/actions';
import { byTagAndText } from '../helpers/selectors';

const SignInMFA = (p: Page, f: Frame | null = null) => {
  const page = f || p;
  const { waitForResponse, clickFrameElement } = actions(p, f);
  const elements = {
    backLink: byTagAndText('a', 'Back'),
    updateDetailsLink: byTagAndText('a', 'Update your details here'),
    toastContainer: '//zip-popup-toast-outlet',
    sendCodeButton: byTagAndText('div', 'Send code'),
    resendLink: byTagAndText('a', 'Resend'),
    verificationCodeField: '#mat-input-2',
    verifyButton: byTagAndText('div', 'Verify'),
  };

  async function selectSendCode() {
    const { sendCodeButton } = elements;
    const clickAction = () => (f
      ? clickFrameElement(sendCodeButton)
      : page.click(sendCodeButton));
    await Promise.all([
      clickAction(),
      waitForResponse('/login/v2/mfa/challenge', { status: 200 }),
    ]);
  }

  async function enterActivationCode(code = DEBUG_SMS_CODE) {
    const { verificationCodeField, verifyButton } = elements;
    await page.type(verificationCodeField, code);
    f ? await clickFrameElement(verifyButton)
      : await page.click(verifyButton);
  }

  return {
    selectSendCode,
    enterActivationCode,
  };
};

export default SignInMFA;
