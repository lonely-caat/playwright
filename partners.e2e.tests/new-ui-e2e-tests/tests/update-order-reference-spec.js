import dotenv from 'dotenv-safe';
import newDashSalesPage from '../pages/new-dash-sales-page';
import newDashHomePage from '../pages/new-dash-home-page';
import loginPage from '../pages/new-dash-signin-page';
import * as constants from '../data/constants';
import {RequestMock} from "testcafe";
import helper from '../utils/helper';
const requestData = require('../data/request-response');
dotenv.config();



const mock = RequestMock()
    // authorised endpoint mock
    .onRequestTo('https://zip-api-merchantdashboard.sand.au.edge.zip.co/api/v1/Order/search?Status=authorised&SortBy=OrderDate&OrderBy=Descending&Take=15&Skip=0')
    .respond(requestData.responses.authorised, 200, requestData.headers.requests)


fixture('Partners can submit orders through the new dash')
    .page(process.env.NEW_DASH_PAGE)
    .requestHooks(mock);

test('verify sales data',async (t) => {
        const randomInput = await helper.getRandomString(10)
        await loginPage.signIn(constants.newDash.merchant2.merchantName, constants.newDash.merchant2.merchantPassword);
        await newDashHomePage.salesTab();
        await newDashSalesPage.authorizedSubTab();
        await newDashSalesPage.clickTableElementByIndex(1)
        await newDashSalesPage.editReference(randomInput)
        await t.expect(newDashSalesPage.referenceValue.textContent).eql(randomInput)
    },
);
