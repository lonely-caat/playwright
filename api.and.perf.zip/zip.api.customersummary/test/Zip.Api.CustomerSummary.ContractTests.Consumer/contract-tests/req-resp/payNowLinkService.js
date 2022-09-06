
module.exports = {
    requests: {
        postPaynow: {
            "ConsumerId":577370,
            "Amount":0.01
        },
        postPaynowStringValues: {
            "ConsumerId":"577370",
            "Amount":"0.01"
        },
        postPaynowUnexistingConsumerId: {
            "ConsumerId":1,
            "Amount":0.01
        },


    },
    responses: {
        postPaynow: {
            "payNowUrl":"https://go.dev.au.edge.zip.co/a202311b",
            "reference":"paynow:zipPay:419598:1617946142"
        },
        postPaynowStringValues: {
            "type":"https://tools.ietf.org/html/rfc7231#section-6.5.1",
            "title":"One or more validation errors occurred.",
            "status":400,
            "traceId":"|2f176482-47ca3112073db711.",
            "errors":{
                "$.ConsumerId":[
                    "The JSON value could not be converted to System.Int64. Path: $.ConsumerId | LineNumber: 0 | BytePositionInLine: 22."
                ]
            }
        },
        postPaynowUnexistingConsumerId: {
            "statusCode":500,
            "statusDescription":"InternalServerError",
            "message":"consumer id 1 not found."
        },

    },
};
