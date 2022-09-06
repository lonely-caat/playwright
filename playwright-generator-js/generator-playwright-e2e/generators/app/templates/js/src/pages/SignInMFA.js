const { DEBUG_SMS_CODE } = require('../constants/appData');
const actions = require('../helpers/actions');
const { byTagAndText } = require('../helpers/selectors');

const SignInMFA = (pageInstance, frameInstance = null) => {
  const page = frameInstance || pageInstance;
  const { waitForResponse, clickFrameElement } = actions(pageInstance, frameInstance);
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
    const clickAction = () =>
      frameInstance ? clickFrameElement(sendCodeButton) : page.click(sendCodeButton);
    await Promise.all([clickAction(), waitForResponse('/login/v2/mfa/challenge', { status: 200 })]);
  }

  async function enterActivationCode(code = DEBUG_SMS_CODE) {
    const { verificationCodeField, verifyButton } = elements;
    await page.type(verificationCodeField, code);
    frameInstance ? await clickFrameElement(verifyButton) : await page.click(verifyButton);
  }

  return {
    selectSendCode,
    enterActivationCode,
  };
};

module.exports = SignInMFA;
