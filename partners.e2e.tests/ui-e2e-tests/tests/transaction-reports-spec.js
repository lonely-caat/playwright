import { RequestMock } from 'testcafe';
import dotenv from 'dotenv-safe';
import transactionalReportsPage from '../pages/transactional-reports-page';
import partnerSignInPage from '../pages/partner-signin-page';
import * as constants from '../data/constants';


dotenv.config();
let conversionMock = RequestMock()
  // Transaction endpoint mock
  .onRequestTo(request =>
    request.url.match(/https:\/\/zip-api-merchantdashboard-reports.internal..*.au.edge.zip.co\/api\/v1\/transaction\/transactions\?startDate=.*&endDate=.*&frequency=.*/) &&
    request.method.toLowerCase() === 'options'
  )
  .respond({}, 200, { 'Access-Control-Allow-Origin': '*', 'content-type': 'application/json charset=utf-8'})
  .onRequestTo(request =>
    request.url.match(/https:\/\/zip-api-merchantdashboard-reports.internal..*.au.edge.zip.co\/api\/v1\/transaction\/transactions\?startDate=.*&endDate=.*&frequency=.*/) &&
    request.method.toLowerCase() === 'get'
  )
  .respond({"transactionReportSummaries":[{"periodStartDateTime":"2021-01-11T00:00:00","merchantTransactionSummaries":[{"merchantName":"zipBill","transactionCount":22}],"totalTransactionCount":22},{"periodStartDateTime":"2021-01-18T00:00:00","merchantTransactionSummaries":[{"merchantName":"zipBill","transactionCount":15},{"merchantName":"ZipCredit Automation Merchant","transactionCount":1}],"totalTransactionCount":16},{"periodStartDateTime":"2021-01-25T00:00:00","merchantTransactionSummaries":[{"merchantName":"zipBill","transactionCount":4},{"merchantName":"ZipCredit Automation Merchant","transactionCount":1}],"totalTransactionCount":5}]}, 200, { 'Access-Control-Allow-Origin': '*' })

  // New Customers endpoint mock
  .onRequestTo(request =>
    request.url.match(/https:\/\/zip-api-merchantdashboard-reports.internal..*.au.edge.zip.co\/api\/v1\/customer\/new-customers\?startDate=.*&endDate=.*&frequency=.*/) &&
    request.method.toLowerCase() === 'options'
  )
  .respond({}, 200, { 'Access-Control-Allow-Origin': '*', 'content-type': 'application/json charset=utf-8'})
  .onRequestTo(request =>
    request.url.match(/https:\/\/zip-api-merchantdashboard-reports.internal..*.au.edge.zip.co\/api\/v1\/customer\/new-customers\?startDate=.*&endDate=.*&frequency=.*/) &&
    request.method.toLowerCase() === 'get'
  )
  .respond({"customerReportSummaries":[{"periodStartDateTime":"2021-01-20T00:00:00","merchantCustomerSummaries":[{"merchantName":"RedBalloon","customerCount":3}],"totalCustomerCount":3}]}, 200, { 'Access-Control-Allow-Origin': '*' })

  // Total Customers endpoint mock
  .onRequestTo(request =>
    request.url.match(/https:\/\/zip-api-merchantdashboard-reports.internal..*.au.edge.zip.co\/api\/v1\/customer\/total-customers\?startDate=.*&endDate=.*&frequency=.*/) &&
    request.method.toLowerCase() === 'options'
  )
  .respond({}, 200, { 'Access-Control-Allow-Origin': '*', 'content-type': 'application/json charset=utf-8'})
  .onRequestTo(request =>
    request.url.match(/https:\/\/zip-api-merchantdashboard-reports.internal..*.au.edge.zip.co\/api\/v1\/customer\/total-customers\?startDate=.*&endDate=.*&frequency=.*/) &&
    request.method.toLowerCase() === 'get'
  )
  .respond({"customerReportSummaries":[{"periodStartDateTime":"2021-01-04T00:00:00","merchantCustomerSummaries":[{"merchantName":"ZipPay Automation Merchant","customerCount":1}],"totalCustomerCount":1},{"periodStartDateTime":"2021-01-05T00:00:00","merchantCustomerSummaries":[{"merchantName":"ZipPay Automation Merchant","customerCount":1}],"totalCustomerCount":1},{"periodStartDateTime":"2021-01-07T00:00:00","merchantCustomerSummaries":[{"merchantName":"ZipPay Automation Merchant","customerCount":1}],"totalCustomerCount":1},{"periodStartDateTime":"2021-01-08T00:00:00","merchantCustomerSummaries":[{"merchantName":"ZipPay Automation Merchant","customerCount":1}],"totalCustomerCount":1},{"periodStartDateTime":"2021-01-10T00:00:00","merchantCustomerSummaries":[{"merchantName":"ZipPay Automation Merchant","customerCount":2}],"totalCustomerCount":2},{"periodStartDateTime":"2021-01-11T00:00:00","merchantCustomerSummaries":[{"merchantName":"ZipPay Automation Merchant","customerCount":33}],"totalCustomerCount":33},{"periodStartDateTime":"2021-01-13T00:00:00","merchantCustomerSummaries":[{"merchantName":"ZipPay Automation Merchant","customerCount":1}],"totalCustomerCount":1},{"periodStartDateTime":"2021-01-14T00:00:00","merchantCustomerSummaries":[{"merchantName":"ZipPay Automation Merchant","customerCount":4}],"totalCustomerCount":4},{"periodStartDateTime":"2021-01-15T00:00:00","merchantCustomerSummaries":[{"merchantName":"ZipPay Automation Merchant","customerCount":28}],"totalCustomerCount":28},{"periodStartDateTime":"2021-01-17T00:00:00","merchantCustomerSummaries":[{"merchantName":"ZipPay Automation Merchant","customerCount":2}],"totalCustomerCount":2},{"periodStartDateTime":"2021-01-18T00:00:00","merchantCustomerSummaries":[{"merchantName":"ZipPay Automation Merchant","customerCount":18}],"totalCustomerCount":18},{"periodStartDateTime":"2021-01-19T00:00:00","merchantCustomerSummaries":[{"merchantName":"ZipPay Automation Merchant","customerCount":15}],"totalCustomerCount":15},{"periodStartDateTime":"2021-01-20T00:00:00","merchantCustomerSummaries":[{"merchantName":"ZipPay Automation Merchant","customerCount":2}],"totalCustomerCount":2},{"periodStartDateTime":"2021-01-21T00:00:00","merchantCustomerSummaries":[{"merchantName":"ZipPay Automation Merchant","customerCount":2}],"totalCustomerCount":2},{"periodStartDateTime":"2021-01-22T00:00:00","merchantCustomerSummaries":[{"merchantName":"ZipPay Automation Merchant","customerCount":3}],"totalCustomerCount":3},{"periodStartDateTime":"2021-01-24T00:00:00","merchantCustomerSummaries":[{"merchantName":"ZipPay Automation Merchant","customerCount":2}],"totalCustomerCount":2},{"periodStartDateTime":"2021-01-25T00:00:00","merchantCustomerSummaries":[{"merchantName":"ZipPay Automation Merchant","customerCount":16}],"totalCustomerCount":16},{"periodStartDateTime":"2021-01-26T00:00:00","merchantCustomerSummaries":[{"merchantName":"ZipPay Automation Merchant","customerCount":2}],"totalCustomerCount":2},{"periodStartDateTime":"2021-01-27T00:00:00","merchantCustomerSummaries":[{"merchantName":"ZipPay Automation Merchant","customerCount":41}],"totalCustomerCount":41}]}, 200, { 'Access-Control-Allow-Origin': '*' })

  // Transaction gross volume endpoint mock
  .onRequestTo(request =>
    request.url.match(/https:\/\/zip-api-merchantdashboard-reports.internal..*.au.edge.zip.co\/api\/v1\/transaction\/gross-volume\?startDate=.*&endDate=.*&frequency=.*/) &&
    request.method.toLowerCase() === 'options'
  )
  .respond({}, 200, { 'Access-Control-Allow-Origin': '*', 'content-type': 'application/json charset=utf-8'})
  .onRequestTo(request =>
    request.url.match(/https:\/\/zip-api-merchantdashboard-reports.internal..*.au.edge.zip.co\/api\/v1\/transaction\/gross-volume\?startDate=.*&endDate=.*&frequency=.*/) &&
    request.method.toLowerCase() === 'get'
  )
  .respond({"grossVolumeReportSummaries":[{"periodStartDateTime":"2021-01-04T00:00:00","totalVolume":100.000000,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":100.000000}]},{"periodStartDateTime":"2021-01-05T00:00:00","totalVolume":1.000000,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":1.000000}]},{"periodStartDateTime":"2021-01-07T00:00:00","totalVolume":1300.000000,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":1300.000000}]},{"periodStartDateTime":"2021-01-08T00:00:00","totalVolume":100.000000,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":100.000000}]},{"periodStartDateTime":"2021-01-10T00:00:00","totalVolume":300.000000,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":300.000000}]},{"periodStartDateTime":"2021-01-11T00:00:00","totalVolume":6874.000000,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":6874.000000}]},{"periodStartDateTime":"2021-01-13T00:00:00","totalVolume":500.000000,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":500.000000}]},{"periodStartDateTime":"2021-01-14T00:00:00","totalVolume":600.000000,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":600.000000}]},{"periodStartDateTime":"2021-01-15T00:00:00","totalVolume":5884.000000,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":5884.000000}]},{"periodStartDateTime":"2021-01-17T00:00:00","totalVolume":300.000000,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":300.000000}]},{"periodStartDateTime":"2021-01-18T00:00:00","totalVolume":4094.000000,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":4094.000000}]},{"periodStartDateTime":"2021-01-19T00:00:00","totalVolume":3631.000000,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":3631.000000}]},{"periodStartDateTime":"2021-01-20T00:00:00","totalVolume":210.000000,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":210.000000}]},{"periodStartDateTime":"2021-01-21T00:00:00","totalVolume":510.000000,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":510.000000}]},{"periodStartDateTime":"2021-01-22T00:00:00","totalVolume":810.000000,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":810.000000}]},{"periodStartDateTime":"2021-01-24T00:00:00","totalVolume":300.000000,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":300.000000}]},{"periodStartDateTime":"2021-01-25T00:00:00","totalVolume":2988.000000,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":2988.000000}]},{"periodStartDateTime":"2021-01-26T00:00:00","totalVolume":300.000000,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":300.000000}]},{"periodStartDateTime":"2021-01-27T00:00:00","totalVolume":10024.000000,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":10024.000000}]}]}, 200, { 'Access-Control-Allow-Origin': '*' })

  // Disbursement net volume endpoint mock
  .onRequestTo(request =>
    request.url.match(/https:\/\/zip-api-merchantdashboard-reports.internal..*.au.edge.zip.co\/api\/v1\/disbursement\/net-volume\?startDate=.*&endDate=.*&frequency=.*/) &&
    request.method.toLowerCase() === 'options'
  )
  .respond({}, 200, { 'Access-Control-Allow-Origin': '*', 'content-type': 'application/json charset=utf-8'})
  .onRequestTo(request =>
    request.url.match(/https:\/\/zip-api-merchantdashboard-reports.internal..*.au.edge.zip.co\/api\/v1\/disbursement\/net-volume\?startDate=.*&endDate=.*&frequency=.*/) &&
    request.method.toLowerCase() === 'get'
  )
  .respond({"netVolumeReportSummaries":[{"periodStartDateTime":"2021-01-11T00:00:00","totalVolume":7847.3900,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":7847.3900}]},{"periodStartDateTime":"2021-01-15T00:00:00","totalVolume":6085.2800,"merchantVolumeSummaries":[{"merchantName":"ZipPay Automation Merchant","volume":6085.2800}]}]}, 200, { 'Access-Control-Allow-Origin': '*' })


fixture('Partner transaction report E2E Test')
  .page(process.env.BASE_URL_REPORTS)
  .requestHooks(conversionMock)

  .beforeEach(async (t) => {
      await t.maximizeWindow();
      await partnerSignInPage.signIn(constants.partnerReports.merchant2.adminEmail, constants.partnerReports.merchant1.password);
  });
test.skip('Verify data', async (t) => {

    await t.expect(transactionalReportsPage.mainHeader.innerText).eql('Transactional Reports')
    await transactionalReportsPage.dateRangeDropDown('twelveMonths')

    await transactionalReportsPage.verifyDotLabelValue(0,'zipBill', '22')
    await transactionalReportsPage.verifyDotLabelValue(0,'zipBill', '4', false)

    await transactionalReportsPage.verifyDotLabelValue(1,'ZipCredit Automation Merchant', '1', true, false)
    await transactionalReportsPage.verifyDotLabelValue(1,'ZipCredit Automation Merchant', '1', false, false)

    await transactionalReportsPage.verifyDotLabelValue(2,'ZipPay Automation Merchant', 'A$7,847.39')
    await transactionalReportsPage.verifyDotLabelValue(2,'ZipPay Automation Merchant', 'A$6,085.28', false)

    await transactionalReportsPage.verifyDotLabelValue(3,'ZipPay Automation Merchant', 'A$100.00')
    await transactionalReportsPage.verifyDotLabelValue(3,'ZipPay Automation Merchant', 'A$10,024.00', false)

    await transactionalReportsPage.verifyDotLabelValue(4,'ZipPay Automation Merchant', '1')
    await transactionalReportsPage.verifyDotLabelValue(4,'ZipPay Automation Merchant', '41', false)

    await transactionalReportsPage.verifyDotLabelValue(5,'RedBalloon', '3')

    await t.expect(transactionalReportsPage.weeklyGranularity.visible).ok();
    await t.expect(transactionalReportsPage.monthlyGranularity.visible).ok();

});
