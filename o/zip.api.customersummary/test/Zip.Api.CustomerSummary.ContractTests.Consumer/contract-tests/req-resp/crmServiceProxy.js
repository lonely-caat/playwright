
module.exports = {
    requests: {
        postComment: {
            "ReferenceId":12345678,
            "Category":1,
            "Type":1,
            "CommentBy":"basTest",
            "Detail":"testing bas products"
        },
        postCommentMissingParams: {
            "ReferenceId":"",
            "Category":"",
            "Type":"",
            "CommentBy":"",
            "Detail":""
        },
        postCommentInvalidData: {
            "ReferenceId":"test",
            "Category":"test",
            "Type":"test",
            "CommentBy":"1",
            "Detail":"1"
        },
    },
    responses: {
        postComment: {
            "id":62369,
            "type":1,
            "typeString":"Consumer",
            "category":1,
            "categoryString":"Support",
            "referenceId":12345678,
            "detail":"testing bas products",
            "timeStamp":"2021-04-08T21:04:53.123",
            "commentBy":"basTest"
        },
        postCommentMissingParams: {
            "errors":{
                "ReferenceId":[
                    "Error converting value \"\" to type 'System.Int64'. Path 'ReferenceId', line 2, position 28."
                ]
            },
            "type":"https://tools.ietf.org/html/rfc7231#section-6.5.1",
            "title":"One or more validation errors occurred.",
            "status":400,
            "traceId":"|de6ca570-4f5ca9e8b951b096."
        },
        postCommentInvalidData: {
            "errors":{
                "Type":[
                    "Error converting value \"test\" to type 'System.Nullable`1[Zip.Api.CRM.Api.Models.CommentType]'. Path 'Type', line 4, position 25."
                ],
                "Category":[
                    "Error converting value \"test\" to type 'System.Nullable`1[Zip.Api.CRM.Api.Models.CommentCategory]'. Path 'Category', line 3, position 29."
                ],
                "ReferenceId":[
                    "Error converting value \"test\" to type 'System.Int64'. Path 'ReferenceId', line 2, position 32."
                ]
            },
            "type":"https://tools.ietf.org/html/rfc7231#section-6.5.1",
            "title":"One or more validation errors occurred.",
            "status":400,
            "traceId":"|de6ca583-4f5ca9e8b951b096."
        },
        getComment: {
            "totalCount":4,
            "current":1,
            "pageSize":100,
            "totalPages":1,
            "items":[
                {
                    "id":201036,
                    "type":1,
                    "typeString":"Consumer",
                    "category":0,
                    "categoryString":"General",
                    "referenceId":123456,
                    "detail":"Comment: test",
                    "timeStamp":"2021-02-09T20:42:56.95",
                    "commentBy":"shan.ke@zip.co"
                },
                {
                    "id":196576,
                    "type":1,
                    "typeString":"Consumer",
                    "category":0,
                    "categoryString":"General",
                    "referenceId":123456,
                    "detail":"Comment: test",
                    "timeStamp":"2020-10-29T11:43:55.593",
                    "commentBy":"shan.ke@zip.co"
                },
                {
                    "id":196257,
                    "type":1,
                    "typeString":"Consumer",
                    "category":9,
                    "categoryString":"Risk",
                    "referenceId":123456,
                    "detail":"Comment: <NFD Lock> Auto lock by system. Rule: TransactionEmailExists. Reason: High velocity on new account (check range:90 days) over 3 orders within 2 days. Account review required or successful payment history demonstrated. Order timestamp history: 2017-02-25T16:23:03, 2017-02-25T17:40:54, 2017-02-25T18:25:06, 2017-02-27T16:10:49Attribute(s) in black list: Email:zip.developer1@mailinator.com(BadCredit),PhoneNumber:0401010101(BadCredit)",
                    "timeStamp":"2020-10-15T09:15:09.973",
                    "commentBy":"system"
                },
                {
                    "id":67623,
                    "type":1,
                    "typeString":"Consumer",
                    "category":9,
                    "categoryString":"Risk",
                    "referenceId":123456,
                    "detail":"Comment: test data",
                    "timeStamp":"2018-11-26T17:31:51.507",
                    "commentBy":"minh.nguyen@zip.co"
                }
            ]
        },
        getCommentUnexistingCustomerId:{
            "totalCount":0,
            "current":1,
            "pageSize":100,
            "totalPages":0,
            "items":[

            ]
        },
        getCommentInvalidCustomerId: {
            "errors":{
                "customerId":[
                    "The value 'test' is not valid."
                ]
            },
            "type":"https://tools.ietf.org/html/rfc7231#section-6.5.1",
            "title":"One or more validation errors occurred.",
            "status":400,
            "traceId":"|3815d843-4eec38002f1b4e6a."
        },
        getCommentInvalidParams: {
            "type":"https://tools.ietf.org/html/rfc7231#section-6.5.1",
            "title":"One or more validation errors occurred.",
            "status":400,
            "traceId":"|9710c9e6-4f19f3e6b256fb4a.",
            "errors":{
                "customerId":[
                    "The value 'test' is not valid."
                ]
            }
        }

    },
};
