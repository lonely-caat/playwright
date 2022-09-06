import { Frame, Page } from 'playwright';
import createPages from '../pages/index';

const Authenticate = (p: Page, f: Frame | null = null) => {
  const { signIn, signInMFA } = createPages(p, f);

  async function signInNo2fa() {
    return Promise.all([signIn.performSignIn(), signIn.waitForTokenResponse(204)]);
  }

  async function signInAnd2fa() {
    await Promise.all([
      signIn.performSignIn(),
      signIn.waitForTokenResponse(),
    ]);
    await signInMFA.selectSendCode();
    await Promise.all([signInMFA.enterActivationCode(), signIn.waitForTokenResponse(200)]);
  }

  return {
    signInNo2fa,
    signInAnd2fa,
  };
};

export default Authenticate;
