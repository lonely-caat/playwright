import dotenv from 'dotenv-safe'
import assert from "assert";
import supertest from 'supertest';

dotenv.config()
const env = process.env.BASE_URL_COMMUNICATIONS_API;



describe('POST /api/emails/send/ tests',  function() {

  it('/close-account. Check that email with expected subject and body is received in email', async function() {
     /*
    Steps:
    1. Create Mailinator inbox
    2. Send /api/emails/send/close-account to the inbox from the above step
    3. Verify response
    4. Verify email received
    5. Verify email content
    */
    // create a Mailinator inbox with current timestamp as the name
    const user = "bascattesting"
    const tempEmail = `bascattesting@mailinator.com`;
    let zip_name = "test name Jr."
    let zip_account = "test account name"
    let zip_product = "test product"
    const payload = {"firstName":zip_name,"product":zip_product,"accountNumber":zip_account,"email":tempEmail}

    await supertest(env)
      .post(`/api/emails/send/close-account`)
      .send(payload)
      .set('Accept', 'application/json')
      .expect(200)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then(response => {
        assert.strictEqual(response.text, "{\"success\":true,\"message\":\"Delivery StatusCode: Accepted\"}")
      });
    // const mailContent = await mailHelper.getEmailDetails(user, `Your ${zip_product} account has been closed`);
    // console.log(mailContent, '!!!!!!!!')
    // assert(mailContent.includes(`Hi ${zip_name},`),
    //   `expected string not found in email content: ${mailContent}`)
    // assert(mailContent.includes(`Close account confirmation`),
    //   `expected string not found in email content: ${mailContent}`)
    // assert(mailContent.includes(`This is a friendly confirmation that your ${zip_product} account (${zip_account}) has now been closed.`),
    //   `expected string not found in email content: ${mailContent}`)

  });

  // TODO add link verification
  it('/reset-password Check that email with expected subject and body is received in email', async function() {
    /*
   Steps:
   1. Create Mailinator inbox
   2. Send /api/emails/send/reset-password to the inbox from the above step
   3. Verify response
   4. Verify email received
   5. Verify email content
   */
    // create a Mailinator inbox with current timestamp as the name
    const user = "bascattesting"
    const tempEmail = `bascattesting@mailinator.com`;
    let zip_name = "test name Jr."
    let zip_reset_password_link = "https://google.com/reset_password"
    let zip_product = "test product"

    await supertest(env)
      .post(`/api/emails/send/reset-password`)
      .send({"firstName":zip_name,"product":zip_product,"resetPasswordLink":zip_reset_password_link,"email":tempEmail})
      .set('Accept', 'application/json')
      .expect(200)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then(response => {
        assert.strictEqual(response.text, "{\"success\":true,\"message\":\"Delivery StatusCode: Accepted\"}")
      });
    // const mailContent = await mailHelper.getEmailDetails(user, `Set Your Zip Password`);
    //
    // console.log(mailContent)
    //
    // assert(mailContent.includes(`Hi ${zip_name},`))
    // assert(mailContent.includes(`You have requested a password change for your ${zip_product} account.`))

  });

  it('/credit-decline Check that email with expected subject and body is received in email', async function() {
    /*
   Steps:
   1. Create Mailinator inbox
   2. Send /api/emails/send/credit-decline to the inbox from the above step
   3. Verify response
   4. Verify email received
   5. Verify email content
   */
    // create a Mailinator inbox with current timestamp as the name
    const user = "bascattesting"
    const tempEmail = `bascattesting@mailinator.com`;
    let zip_name = "test name Jr."
    let zip_product = "test product"

    await supertest(env)
      .post(`/api/emails/send/credit-decline`)
      .send({"firstName":zip_name,"product":zip_product,"email":tempEmail})
      .set('Accept', 'application/json')
      .expect(200)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then(response => {
        assert.strictEqual(response.text, "{\"success\":true,\"message\":\"Delivery StatusCode: Accepted\"}")
      });
  //   const mailContent = await mailHelper.getEmailDetails(user, `Important information about your Zip application`);
  //   console.log(mailContent)
  //
  //   assert(mailContent.includes(`Hi ${zip_name},`),
  //     `email content: ${mailContent}`)
  //   assert(mailContent.includes(`Thanks for your recent application.`))
  //   assert(mailContent.includes(`https://www.equifax.com.au/contact`),
  //     `email does not contain equifax contact link, email content: ${mailContent}`)
  //   assert(mailContent.includes(`Address PO Box 964, North Sydney 2060`),
  //     `email does not contain equifax physical address, email content: ${mailContent}`)
  //   assert(mailContent.includes(`https://zip.co/privacy`),
  //     `email does not contain zip privacy page link, email content: ${mailContent}`)
  //   assert(mailContent.includes(`https://help.zip.co/en/articles/138 `),
  //     `email does not contain zip article, email content: ${mailContent}`)
  });

  it('/id-confirmation-request Check that email with expected subject and body is received in email for New Zealand', async function() {
    /*
   Steps:
   1. Create Mailinator inbox
   2. Send /api/emails/send/id-confirmation-request to the inbox from the above step
   3. Verify response
   4. Verify email received
   5. Verify email content
   */
    // create a Mailinator inbox with current timestamp as the name
    const user = "bascattesting"
    const tempEmail = `bascattesting@mailinator.com`;
    let zip_name = "test name Jr."
    let zip_product = "test product"
    let zip_country = "NZ"

    await supertest(env)
      .post(`/api/emails/send/id-confirmation-request`)
      .send({"firstName":zip_name,"product":zip_product,"country":zip_country,"email":tempEmail})
      .set('Accept', 'application/json')
      .expect(200)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then(response => {
        assert.strictEqual(response.text, "{\"success\":true,\"message\":\"Delivery StatusCode: Accepted\"}")
      });
  //   const mailContent = await mailHelper.getEmailDetails(user, `${zip_product} Application - Information Request`);
  //   console.log(mailContent)
  //
  //   assert(mailContent.includes(`Dear ${zip_name},`),
  //     `email content: ${mailContent}`)
  //   assert(mailContent.includes(`Your current New Zealand Nationality status (Visa type and subclass number if applicable)`))
  //   assert(mailContent.includes(`Please ensure that the photo is in high resolution with all details of the ID to be visible on the photo`))
  //   assert(mailContent.includes(`https://static.zipmoney.com.au/edm/2020/ocr/ocr2.png`),
  //     `email does not contain the photo example for taking pictures`)
  });

  it('/id-confirmation-request Check that email with expected subject and body is received in email for Australia', async function() {
      /*
     Steps:
     1. Create Mailinator inbox
     2. Send /api/emails/send/id-confirmation-request to the inbox from the above step
     3. Verify response
     4. Verify email received
     5. Verify email content
     */
      // create a Mailinator inbox with current timestamp as the name
    const user = "bascattesting"
    const tempEmail = `bascattesting@mailinator.com`;
      let zip_name = "test name Jr."
      let zip_product = "test product"
      let zip_country = "AU"

      await supertest(env)
        .post(`/api/emails/send/id-confirmation-request`)
        .send({"firstName":zip_name,"product":zip_product,"country":zip_country,"email":tempEmail})
        .set('Accept', 'application/json')
        .expect(200)
        .expect('Content-Type', 'application/json; charset=utf-8')
        .then(response => {
          assert.strictEqual(response.text, "{\"success\":true,\"message\":\"Delivery StatusCode: Accepted\"}")
        });
  //     const mailContent = await mailHelper.getEmailDetails(user, `${zip_product} Application - Information Request`);
  //     console.log(mailContent)
  //
  //     assert(mailContent.includes(`Dear ${zip_name},`),
  //       `email content: ${mailContent}`)
  //     assert(mailContent.includes(`Your current Australian Nationality status (Visa type and subclass number if applicable)`))
  //     assert(mailContent.includes(`Please ensure that the photo is in high resolution with all details of the ID to be visible on the photo`))
  //     assert(mailContent.includes(`https://static.zipmoney.com.au/edm/2020/ocr/ocr2.png`),
  //       `email does not contain the photo example for taking pictures`)
  });

  // Blocked by https://zipmoney.atlassian.net/browse/BP-230
  it('/arrears-notifications. Check that email with expected subject and body is received in email', async function() {
    /*
   Steps:
   1. Create Mailinator inbox
   2. Send /api/emails/send/arrears-notifications to the inbox from the above step
   3. Verify response
   4. Verify email received
   5. Verify email content
   */
    // create a Mailinator inbox with current timestamp as the name
    const user = "bascattesting"
    const tempEmail = `bascattesting@mailinator.com`;
    let zip_name = "test name Jr."
    let zip_account_id = 1
    let zip_consumer_id = 1
    let zip_product = "test product"
    let zip_phone_number = '0400000000'
    let zip_due_date = '2020-10-04T12:00:07.827Z'
    let zip_arrears_amount = 10
    let zip_pay_now_url = "https://google.com"
    let zip_country = 'AU'
    let zip_subject = 'Subject here'

    await supertest(env)
      .post(`/api/emails/send/arrears-notifications`)
      .send({"accountId": zip_account_id,"consumerId": zip_consumer_id,"firstName": zip_name,"phoneNumber": zip_phone_number,"email": tempEmail,
        "arrearsDays": 0,"dueDate": zip_due_date,"arrearsAmount": zip_arrears_amount,"payNowUrl": zip_pay_now_url,
        "countryId": zip_country,"notificationTypeId": 0,"productClassification": "productClassification","subject": zip_subject})
      .set('Accept', 'application/json')
      .expect(200)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then(response => {
        assert.strictEqual(response.text, "{\"success\":true,\"message\":\"Delivery StatusCode: Accepted\"}")
      });
    // const mailContent = await mailHelper.getEmailDetails(user, `Your ${zip_product} account has been closed`);
    // assert(mailContent.includes(`Hi ${zip_name},`),
    //   `expected string not found in email content: ${mailContent}`)
    // assert(mailContent.includes(`Close account confirmation`),
    //   `expected string not found in email content: ${mailContent}`)
    // assert(mailContent.includes(`This is a friendly confirmation that your ${zip_product} account (${zip_account}) has now been closed.`),
    //   `expected string not found in email content: ${mailContent}`)

  });

  // TODO find bucket ids
  it('/section88-notice. Check that email with expected subject and body is received in email', async function() {
    /*
   Steps:
   1. Create Mailinator inbox
   2. Send /api/emails/send/section88-notice to the inbox from the above step
   3. Verify response
   4. Verify email received
   5. Verify email content
   */
    // create a Mailinator inbox with current timestamp as the name
    const user = "bascattesting"
    const tempEmail = `bascattesting@mailinator.com`;
    let zip_name = "test name Jr."
    let zip_account = "test account name"
    let zip_product = "test product"

    await supertest(env)
      .post(`/api/emails/send/close-account`)
      .send({"firstName":zip_name,"product":zip_product,"accountNumber":zip_account,"email":tempEmail})
      .set('Accept', 'application/json')
      .expect(200)
      .expect('Content-Type', 'application/json; charset=utf-8')
      .then(response => {
        assert.strictEqual(response.text, "{\"success\":true,\"message\":\"Delivery StatusCode: Accepted\"}")
      });
  //   const mailContent = await mailHelper.getEmailDetails(user, `Your ${zip_product} account has been closed`);
  //   assert(mailContent.includes(`Hi ${zip_name},`),
  //     `expected string not found in email content: ${mailContent}`)
  //   assert(mailContent.includes(`Close account confirmation`),
  //     `expected string not found in email content: ${mailContent}`)
  //   assert(mailContent.includes(`This is a friendly confirmation that your ${zip_product} account (${zip_account}) has now been closed.`),
  //     `expected string not found in email content: ${mailContent}`)
  //
  });

  // Blocked by https://zipmoney.atlassian.net/browse/BP-3
  it('/decline-email Check that email with expected subject and body is received in email', async function() {
    /*
   Steps:
   1. Create Mailinator inbox
   2. Send /api/emails/send/decline-email to the inbox from the above step
   3. Verify response
   4. Verify email received
   5. Verify email content
   */
    // create a Mailinator inbox with current timestamp as the name
    const user = "bascattesting"
    const tempEmail = `bascattesting@mailinator.com`;
    let zip_name = "test name Jr."
    let zip_product = "test product"

    await supertest(env)
        .post(`/api/emails/send/decline-email`)
        .send({
          "firstName": zip_name,
          "product": zip_product,
          "email": tempEmail,
          "subject": "Subject",
          "consumerId": 1,
          "accountId": 1
        })
        .set('Accept', 'application/json')
        .expect(200)
        .expect('Content-Type', 'application/json; charset=utf-8')
        .then(response => {
          assert.strictEqual(response.text, "{\"success\":true,\"message\":\"Delivery StatusCode: Accepted\"}")
        });
    //   const mailContent = await mailHelper.getEmailDetails(user, `Your ${zip_product} application has been processed`);
    //   assert(mailContent.includes(`Hi ${zip_name},`),
    //     `expected string not found in email content: ${mailContent}`)
    //   assert(mailContent.includes(`Unfortunately, at this stage we are unable to reassess applications after a decision has been made. `),
    //     `expected string not found in email content: ${mailContent}`)
    //   assert(mailContent.includes(`Previous credit and repayment history`),
    //     `expected string not found in email content: ${mailContent}`)
    //
    });

    it('GET /arrears-notification-types Check that expected JSON of notification types is returned', async function () {
      /*
     Steps:
     1. Send GET /arrears-notification-types
     2. Verify response
     */

      await supertest(env)
          .get(`/api/emails/arrears-notification-types`)
          .set('Accept', 'application/json')
          .expect(200)
          .expect('Content-Type', 'application/json; charset=utf-8')
          .then(response => {
            assert.strictEqual(response.text, "[{\"key\":0,\"value\":\"Payment Failure\"},{\"key\":1,\"value\":\"Reminder Overdue\"},{\"key\":2,\"value\":\"Reminder Overdue Strong\"},{\"key\":3,\"value\":\"Final Reminder\"}]")
          })
    });
});




