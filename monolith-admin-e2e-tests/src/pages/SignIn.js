
const { USERS } = require('../constants/usersData');


const elements = {
  header: 'text=Sign in as Zip partner',
  emailField: "#username",
  passwordField: "#password",
  continueButton: 'button:has-text("Continue")',
  skipAddon: '[data-testid="skip-extension"]',
  productSearch: '#search-input',
  adminSandBoxSelector: '[aria-label="Launch Admin - Sandbox"] div',
};

const SignInOneLogin = (pageInstance) =>
{

  async function performSignIn(email = USERS.oneLoginUser.adminEmail,
                               password = USERS.oneLoginUser.password) {
    const page = pageInstance;
    const {emailField, passwordField, continueButton} = elements;
    await page.waitForSelector(emailField);
    await page.fill(emailField, email);
    await page.click(continueButton);

    await page.waitForSelector(passwordField);
    await page.fill(passwordField, password);
    await page.click(continueButton);
  }

  async function loginAdmin(){
    const page = pageInstance;
    const { skipAddon, productSearch, adminSandBoxSelector, } = elements;
    await page.click(skipAddon)

    await page.waitForSelector(productSearch)
    await page.fill(productSearch, "admin - sandbox")
    await page.click(adminSandBoxSelector);
    await page.context().storageState({ path: '../../outputstorageState.json' });

  }
  return { performSignIn, loginAdmin }
}



module.exports = SignInOneLogin;
