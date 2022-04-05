// eslint-disable-next-line no-undef
import dotenv from 'dotenv-safe';
import newDashSalesPage from '../pages/new-dash-sales-page';
import newDashHomePage from '../pages/new-dash-home-page';
import loginPage from '../pages/new-dash-signin-page';
import * as constants from '../data/constants';
import {RequestMock} from "testcafe";
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
            await loginPage.signIn(constants.newDash.merchant2.merchantName, constants.newDash.merchant2.merchantPassword);
            await newDashHomePage.salesTab();
            await newDashSalesPage.authorizedSubTab();
            await newDashSalesPage.clickTableElementByIndex(1)
            await t.expect(newDashSalesPage.orderDate.innerText).eql('08 June 2021');
            await t.expect(newDashSalesPage.branch.innerText).eql('branch1');
            await t.expect(newDashSalesPage.salesRep.innerText).eql('');
            await t.expect(newDashSalesPage.amountOutstanding.innerText).eql('$0.00');
            await t.expect(newDashSalesPage.amountPaid.innerText).eql('$1.00');
            await t.expect(newDashSalesPage.amountRefunded.innerText).eql('$0.00');
    },
);
