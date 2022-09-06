const createPages = require('../pages/index');

const Authenticate = (page, frame = null) => {
  const { signIn, signInMFA } = createPages(page, frame);
  async function signInNo2fa() {
    return Promise.all([signIn.performSignIn(), signIn.waitForTokenResponse(204)]);
  }

  async function signInAnd2fa() {
    await Promise.all([signIn.performSignIn(), signIn.waitForTokenResponse()]);
    await signInMFA.selectSendCode();
    await Promise.all([signInMFA.enterActivationCode(), signIn.waitForTokenResponse(200)]);
  }

  return {
    signInNo2fa,
    signInAnd2fa,
  };
};
module.exports = Authenticate;
