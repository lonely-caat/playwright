import supertest from 'supertest';
import { isDev } from '../config';
import { createRandomEmail } from '../helpers/generateData';

const qaUtilsURL = 'http://qa-utils-1866972975.ap-southeast-2.elb.amazonaws.com';

export enum CreateUserProducts {
  zipPay = 'zipPay',
  zipMoney = 'zipCredit',
}

type CreateUserPayload = {
  email?: string,
  product?: CreateUserProducts,
  env?: 'sandbox' | 'dev',
  token?: string,
  charge?: boolean,
  purchaseAmount?: number,
};

export default async function createUser({
  email = createRandomEmail(),
  product = CreateUserProducts.zipPay,
  env = isDev ? 'dev' : 'sandbox',
  token = 'AutomationTest',
  charge = false,
  purchaseAmount = 0,
}: CreateUserPayload = {}) {
  const payload = {
    product,
    env,
    token,
    charge,
    purchaseAmount,
    email,
  };
  return supertest(qaUtilsURL)
    .post('/createuser')
    .set({ 'Content-Type': 'application/json' })
    .send(payload)
    .then(({ status, text }) => {
      if (status !== 200) {
        throw new Error(`Status code ${status}`);
      }
      return ({ email: text });
    })
    .catch((error) => {
      throw new Error(`Test failed due to /createuser script returning error: ${error}`);
    });
}
