import { Frame, Page } from 'playwright';

import transactionMFA from './TransactionMFA';
import ConfirmOrder from './ConfirmOrder';
import AccountSelector from './AccountSelector';
import Merchant from './Merchant';
import SignIn from './SignIn';
import SignInMFA from './SignInMFA';
import VerifyEmail from './VerifyEmail';
import WebWallet from './WebWallet';

export default (page: Page, frame: Frame | null = null) => ({
  accountSelector: AccountSelector(page),
  confirmOrder: ConfirmOrder(page, frame),
  merchantLuma: Merchant(page),
  signIn: SignIn(page, frame),
  transactionMFA: transactionMFA(page, frame),
  signInMFA: SignInMFA(page, frame),
  verifyEmail: VerifyEmail(page),
  webWallet: WebWallet(page),
});
