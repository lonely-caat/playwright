// eslint-disable-next-line no-undef
import dotenv from 'dotenv-safe';
import createOrderPage from '../pages/create-order-page';
import completeOrderPage from '../pages/complete-order-page';
import dashboardHomePage from '../pages/dashboard-home-page';
import partnerSignInPage from '../pages/partner-signin-page';
import * as constants from '../data/constants';
import mailHelper from '../../ui-e2e-tests/utils/mail-helper.js';

dotenv.config();

fixture('Partners can create auth & capture order through dashboard').page(
  process.env.ZIPPARTNER_SIGNIN,
);

test.skip('Verify auth & capture order :heavy_exclamation_mark: https://zipmoney.atlassian.net/browse/BP-311',
  async (t) => {
    const userEmail = constants.partnerDashboard.merchant2.adminEmail;
    await partnerSignInPage.signIn(userEmail);
    await dashboardHomePage.createOrderTile();
    await createOrderPage.createcOrder(constants.orderEmail);

    await createOrderPage.submitOrder();
    await t.expect(completeOrderPage.receiptNumber.innerText).contains('Receipt Number');

    const mailContent = await mailHelper.getEmailDetails(
      constants.orderEmail.split('@')[0],
      `Thank you for shopping with Zip`,
      30000,
    );

    await t.expect(mailContent).contains(`$${constants.price}.00`);
    await t
      .expect(mailContent)
      .contains(
        `If you have any questions about your order please contact ${constants.partnerDashboard.merchant2.merchantName} directly.`,
      );
  },
);
