const supertest = require('supertest');
const generateData = require('../helpers/generateData');
const { QA_UTILS_URL } = require('../constants/appData');

const createUserProducts = {
  zipPay: 'zipPay',
  zipMoney: 'zipCredit',
};
async function createUser(
  product = createUserProducts.zipPay,
  env = 'sandbox',
  token = 'AutomationTest',
  charge = false,
  purchaseAmount = 0,
  email = generateData.createRandomEmail(),
) {
  const payload = {
    product,
    env,
    token,
    charge,
    purchaseAmount,
    email,
  };
  return supertest(QA_UTILS_URL)
    .post('/createuser')
    .set({ 'Content-Type': 'application/json' })
    .send(payload)
    .then(({ status, text }) => {
      if (status !== 200) {
        throw new Error(`Status code ${status}`);
      }
      return { email: text };
    })
    .catch((error) => {
      throw new Error(`Test failed due to /createuser script returning error: ${error}`);
    });
}

module.exports = {
  createUser
};
