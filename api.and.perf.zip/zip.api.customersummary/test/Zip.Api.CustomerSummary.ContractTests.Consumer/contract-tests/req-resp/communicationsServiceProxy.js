
module.exports = {
    requests: {
        postCloseAccount: {
            "firstName":"test",
            "product":"test",
            "accountNumber":"test",
            "email":"bastest@gmail.com"
        },
        postResetPassword: {
            "firstName":"test",
            "product":"test",
            "resetPasswordLink":"test",
            "email":"bastest@gmail.com"
        },
        postSendSms: {
            "message":"test",
            "PhoneNumber":"0400000000"
        },
        postSendSmsPaynowLink:
            {
                "Classification":"paynow",
                "FirstName":"test",
                "Message":"please pay",
                "PhoneNumber":"0400000000",
                "PayNowUrl":"https://google.com"
            },
        postSendEmailAccountPaidout:
            {
                "Email":"test@email.com",
                "Address":"test",
                "Product":"ZipPay",
                "FullName":"meow test",
                "DateOfClosure":"01-01-2021",
                "DateOfLetterGeneration":"01-01-2021"
            },

    },
    responses: {
        postCloseAccount: {
            "success":true,
            "message":"Delivery StatusCode: Accepted"
        },
        postResetPassword: {
            "success":true,
            "message":"Delivery StatusCode: Accepted"
        },
        getSmsContent: {
            "id":1,
            "name":"Expired Card",
            "content":"Just letting you know the card used for your {classification} repayments has now expired. Please go to {account-url} to update your details and make a payment",
            "timeStamp":"2016-03-10T16:06:18.52",
            "active":true
        },
        postSendSms: {
            "success":true,
            "message":"Skipping sending message to test number 0400000000"
        },
        postSendSmsPaynowLink: {
            "success":true,
            "message":"Skipping sending message to test number 0400000000"
        },
        postSendEmailAccountPaidout: {
            "success":true,
            "message":"Payload published successfully."
        }
    },
};
