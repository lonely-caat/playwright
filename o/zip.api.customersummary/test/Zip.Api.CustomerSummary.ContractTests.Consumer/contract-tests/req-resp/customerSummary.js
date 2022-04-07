
module.exports = {
    responses: {
        products: [
            {
                "id": 1,
                "classification": "zipPay",
                "countryId": "AU",
                "status": 1
            },
            {
                "id": 2,
                "classification": "zipMoney",
                "countryId": "AU",
                "status": 1
            },
            {
                "id": 3,
                "classification": "zipPay",
                "countryId": "NZ",
                "status": 1
            },
            {
                "id": 4,
                "classification": "zipMoney",
                "countryId": "NZ",
                "status": 1
            },
            {
                "id": 5,
                "classification": "zipBiz",
                "countryId": "AU",
                "status": 1
            },
            {
                "id": 6,
                "classification": "zipBizBasic",
                "countryId": "AU",
                "status": 1
            }
        ],
        countries: [
            {
                "id": "AU",
                "name": "Australia"
            },
            {
                "id": "NZ",
                "name": "New Zealand"
            }
        ],
        checkoutTokenError: {
            message: 'Purchase.SmsVerificationNotCompleted',
        },
    }
};
