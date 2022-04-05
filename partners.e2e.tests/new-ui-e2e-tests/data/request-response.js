module.exports = {
    requests: {
        'authorised': '/https:\/\/zip-api-merchantdashboard.sand.au.edge.zip.co\/api\/v1\/Order\/search\?Status=authorised&SortBy=OrderDate&OrderBy=Descending&Take=15&Skip=0'

    },
    responses: {
        'authorised': {
            "orders": [{
                "id": 1866829,
                "orderId": 489235,
                "accountId": 187636,
                "consumerId": 250206,
                "branchName": "branch1",
                "firstName": "max",
                "lastName": "test",
                "merchantId": 3006,
                "branchId": 5073,
                "merchantName": "ZipPay Automation Merchant",
                "merchantOrderId": "ZIBRANCH18313",
                "orderDate": "2021-06-08T10:48:47.6301218+00:00",
                "orderReference": "1",
                "orderTotal": 100.0,
                "status": "Authorised"
            }, ]
        }
    },
    headers: {
        requests: {
            "Api-supported-versions": "1.0",
            "Content-type": "application/json",
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Headers": "*"
        }
    }
};