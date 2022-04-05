import { Role } from 'testcafe';
import { getByPlaceholderText } from '@testing-library/testcafe';
import dotenv from 'dotenv-safe';
import * as constants from '../data/constants';

dotenv.config();

const dashboardAdminRole = Role(
  process.env.ZIPPARTNER_SIGNIN,
  async (t) => {
    await t
      .typeText(
        getByPlaceholderText('example@email.com'),
        constants.partnerDashboard.merchant1.adminEmail,
      )
      .typeText(getByPlaceholderText('Enter password'), constants.partnerPassword)
      .click('.auth0-lock-submit');
  },
  { preserveUrl: true },
);

export default dashboardAdminRole;
