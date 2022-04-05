const test = require('../fixtures/fixtures');

const { describe } = test;


describe('refund tests', () => {
    test('debug refund happy path', async ({
                                                        pageObjects: { refund },
                                                        signInOneLogin: page,
                                                    }) => {

        await refund.createPayment()
        await refund.performRefundAny()
        await refund.verifyRefundSuccess()
    });
});
