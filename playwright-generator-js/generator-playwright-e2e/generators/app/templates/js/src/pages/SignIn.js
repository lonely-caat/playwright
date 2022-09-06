const actions = require('../helpers/actions');
const createUser = require('../utils/createUser');
const { DEFAULT_PASSWORD } = require('../constants/appData');

const SignIn = (pageInstance, frameInstance = null) => {
  const page = frameInstance || pageInstance;
  const elements = {
    header: '//zip-view-sign-in//zip-panel-heading-title[1]',
    emailField: '#mat-input-0',
    passwordField: '#mat-input-1',
    submitButton: '//form//button[@type="submit"]',
  };
  const { waitForResponse, clickFrameElement } = actions(pageInstance, frameInstance);

  async function getHeaderText() {
    const { header } = elements;
    return page.innerText(header);
  }

  async function waitForTokenResponse(code = 0) {
    return waitForResponse('/login/v2/connect/token', {
      status: code,
      message: 'Zip login token',
    });
  }

  async function performSignIn(password = DEFAULT_PASSWORD) {
    const { email } = await createUser.createZPUser();
    const { emailField, passwordField, submitButton } = elements;
    await page.waitForSelector(emailField);
    await page.type(emailField, email);
    await page.type(passwordField, password);
    if (frameInstance) {
      await clickFrameElement(submitButton);
    } else {
      await page.click(submitButton);
    }
  }

  return {
    getHeaderText,
    performSignIn,
    waitForTokenResponse,
  };
};

module.exports = SignIn;
