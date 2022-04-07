
module.exports = {
    requests: {
        statementsTrigger: {
            "Accounts": [
                "134"
            ],
            "Classification": "1",
            "StatementDate": "2021-03-01"
        },

        statementsUnexistingTrigger: {
            "Accounts": [
                "noexist"
            ],
            "Classification": "noexist",
            "StatementDate": "3029-63-21"
        },
    },

        responses: {
            TransactionsByNRID: [
                {
                    "id": "a142ac1f-f914-4741-b05e-166e5cb75162",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 5,
                    "currency": "AUD",
                    "chargeId": "ch_1075af31d2b84f8cafdfd8e3abe37682",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "889ee1dc-15a5-4ece-a8ef-c99d7b0b5859",
                    "accountId": "",
                    "customerId": "d98a93bf-9108-4edd-b131-e1fd714239e3",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "360215f2-9dd6-4e60-8152-6dfb4b50fe12",
                    "sourceRequestId": "aaajhgjghgaaaaaaj12asd3111test2345",
                    "sourceApi": "TRANSACTION_FEE"
                },
                {
                    "id": "e4306ecd-04b6-473a-b494-6cb3181e89c3",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 500,
                    "currency": "AUD",
                    "chargeId": "ch_c83bb7ea48024accb57ca3fbf7f5badd",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "889ee1dc-15a5-4ece-a8ef-c99d7b0b5859",
                    "accountId": "",
                    "customerId": "d98a93bf-9108-4edd-b131-e1fd714239e3",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "360215f2-9dd6-4e60-8152-6dfb4b50fe12",
                    "sourceRequestId": "aaajhgjghgaaaaaaj12asd3111",
                    "sourceApi": "MARQETA_FORCED_POST"
                },
                {
                    "id": "f89a5a90-9278-4d3c-a76c-b634f915b850",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 500,
                    "currency": "AUD",
                    "chargeId": "ch_4c4c1ba94ea540a9907405ba5ca3d4d6",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "889ee1dc-15a5-4ece-a8ef-c99d7b0b5859",
                    "accountId": "",
                    "customerId": "d98a93bf-9108-4edd-b131-e1fd714239e3",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "360215f2-9dd6-4e60-8152-6dfb4b50fe12",
                    "sourceRequestId": "aaajhgjghgaaaaaaj12asd3111test2345",
                    "sourceApi": "MARQETA_FORCED_POST"
                },
                {
                    "id": "15508bf5-2515-4bd2-bc69-8e861db4c109",
                    "createdDate": "2020-06-17T02:34:15Z",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 5,
                    "currency": "AUD",
                    "chargeId": "ch_0cdcb10275eb4fafb8123dbf988d1584",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "fe341b98-997d-4f9a-81e9-3a29123b918e",
                    "accountId": "",
                    "customerId": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "sourceRequestId": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGCANCEaaaaLaaaa",
                    "sourceApi": "TRANSACTION_FEE"
                },
                {
                    "id": "4eed9212-d9a2-43f4-82f3-d2332956936e",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 500,
                    "currency": "AUD",
                    "chargeId": "ch_0bcb48885c2a481f9c15021c739ab7a0",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "889ee1dc-15a5-4ece-a8ef-c99d7b0b5859",
                    "accountId": "",
                    "customerId": "d98a93bf-9108-4edd-b131-e1fd714239e3",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "360215f2-9dd6-4e60-8152-6dfb4b50fe12",
                    "sourceRequestId": "aaajhgjghgaaaaaaj123",
                    "sourceApi": "MARQETA_FORCED_POST"
                },
                {
                    "id": "00a52372-3b84-48d7-814a-ac88bb34a0f0",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 500,
                    "currency": "AUD",
                    "chargeId": "ch_a47fa13b24144ef3b34c03eb79241b45",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "c4568ba3-7265-4cc5-98a0-5d6595dcf9c2",
                    "accountId": "",
                    "customerId": "c7fdc64a-bfb1-442c-a32f-e9da72f74755",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "0c12c150-5be4-4d2c-942e-7d79d6dad650",
                    "sourceRequestId": "aaajhgjghgjtestretry123clearing",
                    "sourceApi": "MARQETA_FORCED_POST"
                },
                {
                    "id": "1502f9fd-6dec-4d3a-bdda-7d3d7c54e800",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 5,
                    "currency": "AUD",
                    "chargeId": "ch_56ba4bc89dfa4be88549c39c878f567d",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "889ee1dc-15a5-4ece-a8ef-c99d7b0b5859",
                    "accountId": "",
                    "customerId": "d98a93bf-9108-4edd-b131-e1fd714239e3",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "360215f2-9dd6-4e60-8152-6dfb4b50fe12",
                    "sourceRequestId": "aaajhgjghgaaaaaaj12asd3111",
                    "sourceApi": "TRANSACTION_FEE"
                },
                {
                    "id": "b46e1f7f-d4c4-42c8-8742-f952b9e0fbc0",
                    "createdDate": "2020-06-17T02:34:15Z",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 500,
                    "currency": "AUD",
                    "chargeId": "ch_c6c7f5c68ed64617aa8c1e04cca4b7b8",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "fe341b98-997d-4f9a-81e9-3a29123b918e",
                    "accountId": "",
                    "customerId": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "sourceRequestId": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGCANCEaaaaLaaaa",
                    "sourceApi": "MARQETA_FORCED_POST"
                },
                {
                    "id": "b5edb57a-537d-4f70-8990-f31b1871310d",
                    "createdDate": "2017-09-25T23:41:25Z",
                    "merchantCategory": "5411",
                    "acquirerId": "375321467",
                    "cardAcceptorId": "4445001899609",
                    "terminalId": "TR5000",
                    "cardAcceptorNameLocation": "NRID TEST",
                    "amount": 5000,
                    "currency": "AUD",
                    "chargeId": "ch_fb7545d4a3ed421f90055996a3d8ef7e",
                    "outcome": "approved",
                    "message": "authorised",
                    "processedAt": "2020-12-09T03:30:41.543293Z",
                    "cardId": "080794dc-8e73-4e94-a0b0-2d3905726c6f",
                    "customerId": "9b776525-312d-46f4-a3ed-36ad430940a1",
                    "merchantId": "5b6ff194-c85f-4645-a72a-03c017c1af81",
                    "data": {
                        "type": "authorization",
                        "state": "PENDING",
                        "cardToken": "42d7d26f-082c-4c57-835d-3177775b992e",
                        "userToken": "9b776525-312d-46f4-a3ed-36ad430940a1",
                        "amount": 50,
                        "currencyCode": "AUD",
                        "timestamp": "2017-09-25T23:41:25Z",
                        "gpaOrder": {
                            "jit_funding": {
                                "token": "0b199f94-2d5c-4fc4-9357-be998eaeebfd",
                                "method": "pgfs.authorization",
                                "user_token": "9b776525-312d-46f4-a3ed-36ad430940a1",
                                "amount": 50
                            }
                        },
                        "merchant": {
                            "mcc": "5411",
                            "mid": "4445001899609",
                            "name": "NRID TEST",
                            "city": "SYDNEY",
                            "state": "NJ",
                            "postal_code": "07020",
                            "country_code": "AUS",
                            "cardAcceptorNameLocation": "NRID TEST"
                        },
                        "acquirer": {
                            "institution_id_code": "375321467",
                            "institution_country": "840",
                            "retrieval_reference_number": "526051868288",
                            "system_trace_audit_number": "676127"
                        },
                        "cardSecurityCodeVerification": {
                            "type": "CVV1",
                            "institution_country": {}
                        },
                        "pos": {
                            "pan_entry_mode": "MAG_STRIPE",
                            "pin_entry_mode": "TRUE",
                            "terminal_id": "TR5000",
                            "terminal_attendance": "ATTENDED",
                            "card_holder_presence": "false",
                            "card_presence": "false",
                            "partial_approval_capable": "false",
                            "purchase_amount_only": "false"
                        },
                        "fraud": {
                            "network": {
                                "transaction_risk_score": "86",
                                "account_risk_score": "2"
                            }
                        },
                        "createdTime": "2017-09-25T23:41:25Z",
                        "network": "MASTERCARD",
                        "transactionMetadata": {
                            "payment_channel": "OTHER"
                        },
                        "networkReferenceId": "12345678"
                    },
                    "externalId": "42d7d26f-082c-4c57-835d-3177775b992e",
                    "sourceRequestId": "0b199f94-2d5c-4fc4-9357-be998eaeebfd",
                    "sourceApi": "MARQETA"
                },
                {
                    "id": "4d7936f7-8983-4c81-8cf2-e6987c16d272",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 5,
                    "currency": "AUD",
                    "chargeId": "ch_9c14729f8fb64bfbb90547e6b94b73bb",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "889ee1dc-15a5-4ece-a8ef-c99d7b0b5859",
                    "accountId": "",
                    "customerId": "d98a93bf-9108-4edd-b131-e1fd714239e3",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "360215f2-9dd6-4e60-8152-6dfb4b50fe12",
                    "sourceRequestId": "aaajhgjghgaaaaaaj123",
                    "sourceApi": "TRANSACTION_FEE"
                },
                {
                    "id": "ea30ce65-7b37-4123-a97d-86c8679307a9",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 5,
                    "currency": "AUD",
                    "chargeId": "ch_7e7f34ca8048483eaba801233aa992c3",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "7c54afe0-f4e4-4200-9b59-bde2f12359d0",
                    "accountId": "",
                    "customerId": "4ffb8266-2a59-4084-aaa2-6ea9e6cf66b8",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "039e762a-8b95-4e24-87f7-91833f449a85",
                    "sourceRequestId": "anotehrtoken",
                    "sourceApi": "TRANSACTION_FEE"
                },
                {
                    "id": "776b000a-54df-42c6-9d1e-e75300b86fdb",
                    "createdDate": "2017-09-25T23:41:25Z",
                    "merchantCategory": "5411",
                    "acquirerId": "375321467",
                    "cardAcceptorId": "4445001899609",
                    "terminalId": "TR5000",
                    "cardAcceptorNameLocation": "NRID TEST",
                    "amount": 5000,
                    "currency": "AUD",
                    "chargeId": "ch_e0029868276746eb8b3de7af27e4e91f",
                    "outcome": "approved",
                    "message": "authorised",
                    "processedAt": "2020-12-07T03:46:12.951809Z",
                    "cardId": "080794dc-8e73-4e94-a0b0-2d3905726c6f",
                    "customerId": "9b776525-312d-46f4-a3ed-36ad430940a1",
                    "merchantId": "5b6ff194-c85f-4645-a72a-03c017c1af81",
                    "data": {
                        "type": "authorization.clearing",
                        "state": "PENDING",
                        "cardToken": "42d7d26f-082c-4c57-835d-3177775b992e",
                        "userToken": "9b776525-312d-46f4-a3ed-36ad430940a1",
                        "amount": 50,
                        "currencyCode": "AUD",
                        "timestamp": "2017-09-25T23:41:25Z",
                        "gpaOrder": {
                            "jit_funding": {
                                "token": "0b199f94-2d5c-4fc4-9357-be998eaeebfd",
                                "method": "pgfs.authorization",
                                "user_token": "9b776525-312d-46f4-a3ed-36ad430940a1",
                                "amount": 50
                            }
                        },
                        "merchant": {
                            "mcc": "5411",
                            "mid": "4445001899609",
                            "name": "NRID TEST",
                            "city": "SYDNEY",
                            "state": "NJ",
                            "postal_code": "07020",
                            "country_code": "AUS",
                            "cardAcceptorNameLocation": "NRID TEST"
                        },
                        "acquirer": {
                            "institution_id_code": "375321467",
                            "institution_country": "840",
                            "retrieval_reference_number": "526051868288",
                            "system_trace_audit_number": "676127"
                        },
                        "cardSecurityCodeVerification": {
                            "type": "CVV1",
                            "institution_country": {}
                        },
                        "pos": {
                            "pan_entry_mode": "MAG_STRIPE",
                            "pin_entry_mode": "TRUE",
                            "terminal_id": "TR5000",
                            "terminal_attendance": "ATTENDED",
                            "card_holder_presence": "false",
                            "card_presence": "false",
                            "partial_approval_capable": "false",
                            "purchase_amount_only": "false"
                        },
                        "fraud": {
                            "network": {
                                "transaction_risk_score": "86",
                                "account_risk_score": "2"
                            }
                        },
                        "createdTime": "2017-09-25T23:41:25Z",
                        "network": "MASTERCARD",
                        "transactionMetadata": {
                            "payment_channel": "OTHER"
                        },
                        "networkReferenceId": "12345678"
                    },
                    "externalId": "42d7d26f-082c-4c57-835d-3177775b992e",
                    "sourceRequestId": "0b199f94-2d5c-4fc4-9357-be998eaeebfd",
                    "sourceApi": "MARQETA"
                },
                {
                    "id": "4c3084b1-993d-40d2-9223-eb3903bc9520",
                    "createdDate": "2017-09-25T23:41:25Z",
                    "merchantCategory": "5411",
                    "acquirerId": "375321467",
                    "cardAcceptorId": "4445001899609",
                    "terminalId": "TR5000",
                    "cardAcceptorNameLocation": "NRID TEST",
                    "amount": 5000,
                    "currency": "AUD",
                    "chargeId": "ch_a572286197c147ce9f998cc910faacce",
                    "outcome": "approved",
                    "message": "authorised",
                    "processedAt": "2020-12-09T02:47:26.798224Z",
                    "cardId": "080794dc-8e73-4e94-a0b0-2d3905726c6f",
                    "customerId": "9b776525-312d-46f4-a3ed-36ad430940a1",
                    "merchantId": "5b6ff194-c85f-4645-a72a-03c017c1af81",
                    "data": {
                        "type": "authorization",
                        "state": "PENDING",
                        "cardToken": "42d7d26f-082c-4c57-835d-3177775b992e",
                        "userToken": "9b776525-312d-46f4-a3ed-36ad430940a1",
                        "amount": 50,
                        "currencyCode": "AUD",
                        "timestamp": "2017-09-25T23:41:25Z",
                        "gpaOrder": {
                            "jit_funding": {
                                "token": "0b199f94-2d5c-4fc4-9357-be998eaeebfd",
                                "method": "pgfs.authorization",
                                "user_token": "9b776525-312d-46f4-a3ed-36ad430940a1",
                                "amount": 50
                            }
                        },
                        "merchant": {
                            "mcc": "5411",
                            "mid": "4445001899609",
                            "name": "NRID TEST",
                            "city": "SYDNEY",
                            "state": "NJ",
                            "postal_code": "07020",
                            "country_code": "AUS",
                            "cardAcceptorNameLocation": "NRID TEST"
                        },
                        "acquirer": {
                            "institution_id_code": "375321467",
                            "institution_country": "840",
                            "retrieval_reference_number": "526051868288",
                            "system_trace_audit_number": "676127"
                        },
                        "cardSecurityCodeVerification": {
                            "type": "CVV1",
                            "institution_country": {}
                        },
                        "pos": {
                            "pan_entry_mode": "MAG_STRIPE",
                            "pin_entry_mode": "TRUE",
                            "terminal_id": "TR5000",
                            "terminal_attendance": "ATTENDED",
                            "card_holder_presence": "false",
                            "card_presence": "false",
                            "partial_approval_capable": "false",
                            "purchase_amount_only": "false"
                        },
                        "fraud": {
                            "network": {
                                "transaction_risk_score": "86",
                                "account_risk_score": "2"
                            }
                        },
                        "createdTime": "2017-09-25T23:41:25Z",
                        "network": "MASTERCARD",
                        "transactionMetadata": {
                            "payment_channel": "OTHER"
                        },
                        "networkReferenceId": "12345678"
                    },
                    "externalId": "42d7d26f-082c-4c57-835d-3177775b992e",
                    "sourceRequestId": "0b199f94-2d5c-4fc4-9357-be998eaeebfd",
                    "sourceApi": "MARQETA"
                },
                {
                    "id": "d963985f-cbb4-4872-9385-2195a593683b",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 500,
                    "currency": "AUD",
                    "chargeId": "ch_0debff4d980f4bcaa36bc94a44a8edfc",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "7c54afe0-f4e4-4200-9b59-bde2f12359d0",
                    "accountId": "",
                    "customerId": "4ffb8266-2a59-4084-aaa2-6ea9e6cf66b8",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "039e762a-8b95-4e24-87f7-91833f449a85",
                    "sourceRequestId": "anotehrtoken1233",
                    "sourceApi": "MARQETA_FORCED_POST"
                },
                {
                    "id": "7b4a1339-af66-417b-b49f-293a3b645606",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 500,
                    "currency": "AUD",
                    "chargeId": "ch_5ff2c49dea7a45a182701466482b1f9c",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "dfa4381e-e28b-4b51-b8bd-ec541786014c",
                    "accountId": "",
                    "customerId": "8a9be6d8-7698-439c-adc7-387a47597378",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "458e66e7-1ac2-45bd-a61c-6de19653e896",
                    "sourceRequestId": "aaajhgjghgaaaaaaj12asd3111tes23",
                    "sourceApi": "MARQETA_FORCED_POST"
                },
                {
                    "id": "614184ff-a23e-47a0-b3d2-201e8cc25d84",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 5,
                    "currency": "AUD",
                    "chargeId": "ch_ce60a0e35627456e8551310b4a16d13e",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "c4568ba3-7265-4cc5-98a0-5d6595dcf9c2",
                    "accountId": "",
                    "customerId": "c7fdc64a-bfb1-442c-a32f-e9da72f74755",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "0c12c150-5be4-4d2c-942e-7d79d6dad650",
                    "sourceRequestId": "aaajhgjghgjtestretry123clearing",
                    "sourceApi": "TRANSACTION_FEE"
                },
                {
                    "id": "3b9139f0-1e41-4e4e-9cea-b35218be68aa",
                    "createdDate": "2017-09-25T23:41:25Z",
                    "merchantCategory": "5411",
                    "acquirerId": "375321467",
                    "cardAcceptorId": "4445001899609",
                    "terminalId": "TR5000",
                    "cardAcceptorNameLocation": "NRID TEST",
                    "amount": 5000,
                    "currency": "AUD",
                    "chargeId": "ch_ffa1ed8a5c224ff2aef7872ce415eb2d",
                    "outcome": "approved",
                    "message": "authorised",
                    "processedAt": "2020-12-07T03:45:53.631404Z",
                    "cardId": "080794dc-8e73-4e94-a0b0-2d3905726c6f",
                    "customerId": "9b776525-312d-46f4-a3ed-36ad430940a1",
                    "merchantId": "5b6ff194-c85f-4645-a72a-03c017c1af81",
                    "data": {
                        "type": "authorization",
                        "state": "PENDING",
                        "cardToken": "42d7d26f-082c-4c57-835d-3177775b992e",
                        "userToken": "9b776525-312d-46f4-a3ed-36ad430940a1",
                        "amount": 50,
                        "currencyCode": "AUD",
                        "timestamp": "2017-09-25T23:41:25Z",
                        "gpaOrder": {
                            "jit_funding": {
                                "token": "0b199f94-2d5c-4fc4-9357-be998eaeebfd",
                                "method": "pgfs.authorization",
                                "user_token": "9b776525-312d-46f4-a3ed-36ad430940a1",
                                "amount": 50
                            }
                        },
                        "merchant": {
                            "mcc": "5411",
                            "mid": "4445001899609",
                            "name": "NRID TEST",
                            "city": "SYDNEY",
                            "state": "NJ",
                            "postal_code": "07020",
                            "country_code": "AUS",
                            "cardAcceptorNameLocation": "NRID TEST"
                        },
                        "acquirer": {
                            "institution_id_code": "375321467",
                            "institution_country": "840",
                            "retrieval_reference_number": "526051868288",
                            "system_trace_audit_number": "676127"
                        },
                        "cardSecurityCodeVerification": {
                            "type": "CVV1",
                            "institution_country": {}
                        },
                        "pos": {
                            "pan_entry_mode": "MAG_STRIPE",
                            "pin_entry_mode": "TRUE",
                            "terminal_id": "TR5000",
                            "terminal_attendance": "ATTENDED",
                            "card_holder_presence": "false",
                            "card_presence": "false",
                            "partial_approval_capable": "false",
                            "purchase_amount_only": "false"
                        },
                        "fraud": {
                            "network": {
                                "transaction_risk_score": "86",
                                "account_risk_score": "2"
                            }
                        },
                        "createdTime": "2017-09-25T23:41:25Z",
                        "network": "MASTERCARD",
                        "transactionMetadata": {
                            "payment_channel": "OTHER"
                        },
                        "networkReferenceId": "12345678"
                    },
                    "externalId": "42d7d26f-082c-4c57-835d-3177775b992e",
                    "sourceRequestId": "0b199f94-2d5c-4fc4-9357-be998eaeebfd",
                    "sourceApi": "MARQETA"
                },
                {
                    "id": "0c457513-0de1-458b-82b4-32afc4852362",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 500,
                    "currency": "AUD",
                    "chargeId": "ch_7babc0ce03064f919959f13f668f40ca",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "fe341b98-997d-4f9a-81e9-3a29123b918e",
                    "accountId": "",
                    "customerId": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "sourceRequestId": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGCANCEaaaaLaasssdaaa",
                    "sourceApi": "TRANSACTION_FEE"
                },
                {
                    "id": "0c8d5a76-8ddb-424c-9069-9c05d9288ccb",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 500,
                    "currency": "AUD",
                    "chargeId": "ch_1de677a22b904a58aad31279cf021a9c",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "fe341b98-997d-4f9a-81e9-3a29123b918e",
                    "accountId": "",
                    "customerId": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "sourceRequestId": "dsafdsaf-dslf-dsfjhds",
                    "sourceApi": "TRANSACTION_FEE"
                },
                {
                    "id": "516ab3dc-476c-41fe-9ea6-4922087ee32f",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 5,
                    "currency": "AUD",
                    "chargeId": "ch_2b7a9092c0e8471799aae85f30c7509e",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "dfa4381e-e28b-4b51-b8bd-ec541786014c",
                    "accountId": "",
                    "customerId": "8a9be6d8-7698-439c-adc7-387a47597378",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "458e66e7-1ac2-45bd-a61c-6de19653e896",
                    "sourceRequestId": "aaajhgjghgaaaaaaj12asd3111tes23",
                    "sourceApi": "TRANSACTION_FEE"
                },
                {
                    "id": "a2f9a865-68a1-472b-b210-1f9db375d69f",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 5,
                    "currency": "AUD",
                    "chargeId": "ch_fde6c3fa176644fabd3e88d6d455dbff",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "7c54afe0-f4e4-4200-9b59-bde2f12359d0",
                    "accountId": "",
                    "customerId": "4ffb8266-2a59-4084-aaa2-6ea9e6cf66b8",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "039e762a-8b95-4e24-87f7-91833f449a85",
                    "sourceRequestId": "anotehrtoken1233",
                    "sourceApi": "TRANSACTION_FEE"
                },
                {
                    "id": "02b41628-9d2d-454c-935c-f9388054ee6d",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 5,
                    "currency": "AUD",
                    "chargeId": "ch_5278ab4bd96741158532a768598ac11d",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "fe341b98-997d-4f9a-81e9-3a29123b918e",
                    "accountId": "",
                    "customerId": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "sourceRequestId": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGCANCEaaaaLaasssdaaa",
                    "sourceApi": "TRANSACTION_FEE"
                },
                {
                    "id": "5a709fd8-603b-454e-9582-4c773bc39817",
                    "createdDate": "2017-09-25T23:41:25Z",
                    "merchantCategory": "5411",
                    "acquirerId": "375321467",
                    "cardAcceptorId": "4445001899609",
                    "terminalId": "TR100000",
                    "cardAcceptorNameLocation": "Microsoft*Store",
                    "amount": 3000,
                    "currency": "AUD",
                    "outcome": "declined",
                    "message": "The external account provided not found",
                    "processedAt": "2020-12-02T04:36:03.747300Z",
                    "merchantId": "d3cba82e-1a51-4a79-8271-8d310b992508",
                    "data": {
                        "type": "authorization",
                        "state": "PENDING",
                        "cardToken": "52ae6ea9-b0ff-4663-864a-199307080779",
                        "userToken": "19c22060-2000-4fa5-9556-1c25989e7cbb",
                        "amount": 30,
                        "currencyCode": "AUD",
                        "timestamp": "2017-09-25T23:41:25Z",
                        "gpaOrder": {
                            "jit_funding": {
                                "token": "0b199f94-2d5c-4fc4-9357-be998eaeebfd",
                                "method": "pgfs.authorization",
                                "user_token": "19c22060-2000-4fa5-9556-1c25989e7cbb",
                                "amount": 30
                            }
                        },
                        "merchant": {
                            "mcc": "5411",
                            "mid": "4445001899609",
                            "name": "Microsoft*Store",
                            "city": "SYDNEY",
                            "state": "NJ",
                            "postal_code": "07020",
                            "country_code": "AUS",
                            "cardAcceptorNameLocation": "Microsoft*Store"
                        },
                        "acquirer": {
                            "institution_id_code": "375321467",
                            "institution_country": "840",
                            "retrieval_reference_number": "526051868288",
                            "system_trace_audit_number": "676127"
                        },
                        "cardSecurityCodeVerification": {
                            "type": "CVV1",
                            "institution_country": {}
                        },
                        "pos": {
                            "pan_entry_mode": "MAG_STRIPE",
                            "pin_entry_mode": "TRUE",
                            "terminal_id": "TR100000",
                            "terminal_attendance": "ATTENDED",
                            "card_holder_presence": "false",
                            "card_presence": "false",
                            "partial_approval_capable": "false",
                            "purchase_amount_only": "false"
                        },
                        "fraud": {
                            "network": {
                                "transaction_risk_score": "86",
                                "account_risk_score": "2"
                            }
                        },
                        "createdTime": "2017-09-25T23:41:25Z",
                        "network": "MASTERCARD",
                        "transactionMetadata": {
                            "payment_channel": "OTHER"
                        },
                        "networkReferenceId": "12345678"
                    },
                    "externalId": "52ae6ea9-b0ff-4663-864a-199307080779",
                    "sourceRequestId": "0b199f94-2d5c-4fc4-9357-be998eaeebfd",
                    "sourceApi": "MARQETA"
                },
                {
                    "id": "252b7ec3-afaf-4ca4-83ec-a10f2d92f24d",
                    "createdDate": "2020-06-17T02:34:15Z",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 5,
                    "currency": "AUD",
                    "chargeId": "ch_831e3e58fa1946d5babbf644cdd084db",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "fe341b98-997d-4f9a-81e9-3a29123b918e",
                    "accountId": "",
                    "customerId": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "sourceRequestId": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGCANCEaaaaLaasdaaa",
                    "sourceApi": "TRANSACTION_FEE"
                },
                {
                    "id": "58334e4e-e61d-4855-bb04-f35450ede7f7",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 500,
                    "currency": "AUD",
                    "chargeId": "ch_c274f01b538f421ea938a70699756b8e",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "7c54afe0-f4e4-4200-9b59-bde2f12359d0",
                    "accountId": "",
                    "customerId": "4ffb8266-2a59-4084-aaa2-6ea9e6cf66b8",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "039e762a-8b95-4e24-87f7-91833f449a85",
                    "sourceRequestId": "anotehrtoken",
                    "sourceApi": "MARQETA_FORCED_POST"
                },
                {
                    "id": "cbee8cca-0ea6-435a-a967-4f9197b4d5e6",
                    "createdDate": "2020-06-17T02:34:15Z",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 500,
                    "currency": "AUD",
                    "chargeId": "ch_ee0d218805b94e7ba6e3dd094270db9e",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "fe341b98-997d-4f9a-81e9-3a29123b918e",
                    "accountId": "",
                    "customerId": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "sourceRequestId": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGCANCEaaaaLaasdaaa",
                    "sourceApi": "MARQETA_FORCED_POST"
                },
                {
                    "id": "6b32e2c2-0a06-4d60-8640-e922a0f003c0",
                    "merchantCategory": "6411",
                    "cardAcceptorId": "123456890",
                    "cardAcceptorNameLocation": "Chicken Tooth Music",
                    "amount": 5,
                    "currency": "AUD",
                    "chargeId": "ch_ae508941a64642c796a8dceb51ecb192",
                    "outcome": "approved",
                    "message": "captured",
                    "processedAt": "null",
                    "cardId": "fe341b98-997d-4f9a-81e9-3a29123b918e",
                    "accountId": "",
                    "customerId": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                    "merchantId": "d0f49cd9-9a3f-45bd-93de-e5ebdfe72015",
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "sourceRequestId": "dsafdsaf-dslf-dsfjhds",
                    "sourceApi": "TRANSACTION_FEE"
                },
                {
                    "id": "2dd139e2-64b2-4a32-b74a-dba6dc694696",
                    "createdDate": "2017-09-25T23:41:25Z",
                    "merchantCategory": "5411",
                    "acquirerId": "375321467",
                    "cardAcceptorId": "4445001899609",
                    "terminalId": "TR5000",
                    "cardAcceptorNameLocation": "NRID TEST",
                    "amount": 5000,
                    "currency": "AUD",
                    "chargeId": "ch_9ba4195a25ed49a2ac63763fd3f2b5d5",
                    "outcome": "approved",
                    "message": "authorised",
                    "processedAt": "2020-12-09T03:30:48.916567Z",
                    "cardId": "080794dc-8e73-4e94-a0b0-2d3905726c6f",
                    "customerId": "9b776525-312d-46f4-a3ed-36ad430940a1",
                    "merchantId": "5b6ff194-c85f-4645-a72a-03c017c1af81",
                    "data": {
                        "type": "authorization",
                        "state": "PENDING",
                        "cardToken": "42d7d26f-082c-4c57-835d-3177775b992e",
                        "userToken": "9b776525-312d-46f4-a3ed-36ad430940a1",
                        "amount": 50,
                        "currencyCode": "AUD",
                        "timestamp": "2017-09-25T23:41:25Z",
                        "gpaOrder": {
                            "jit_funding": {
                                "token": "0b199f94-2d5c-4fc4-9357-be998eaeebfd",
                                "method": "pgfs.authorization",
                                "user_token": "9b776525-312d-46f4-a3ed-36ad430940a1",
                                "amount": 50
                            }
                        },
                        "merchant": {
                            "mcc": "5411",
                            "mid": "4445001899609",
                            "name": "NRID TEST",
                            "city": "SYDNEY",
                            "state": "NJ",
                            "postal_code": "07020",
                            "country_code": "AUS",
                            "cardAcceptorNameLocation": "NRID TEST"
                        },
                        "acquirer": {
                            "institution_id_code": "375321467",
                            "institution_country": "840",
                            "retrieval_reference_number": "526051868288",
                            "system_trace_audit_number": "676127"
                        },
                        "cardSecurityCodeVerification": {
                            "type": "CVV1",
                            "institution_country": {}
                        },
                        "pos": {
                            "pan_entry_mode": "MAG_STRIPE",
                            "pin_entry_mode": "TRUE",
                            "terminal_id": "TR5000",
                            "terminal_attendance": "ATTENDED",
                            "card_holder_presence": "false",
                            "card_presence": "false",
                            "partial_approval_capable": "false",
                            "purchase_amount_only": "false"
                        },
                        "fraud": {
                            "network": {
                                "transaction_risk_score": "86",
                                "account_risk_score": "2"
                            }
                        },
                        "createdTime": "2017-09-25T23:41:25Z",
                        "network": "MASTERCARD",
                        "transactionMetadata": {
                            "payment_channel": "OTHER"
                        },
                        "networkReferenceId": "12345678"
                    },
                    "externalId": "42d7d26f-082c-4c57-835d-3177775b992e",
                    "sourceRequestId": "0b199f94-2d5c-4fc4-9357-be998eaeebfd",
                    "sourceApi": "MARQETA"
                },
                {
                    "id": "30911fbd-321b-4bf4-9fdf-31bd7ebc3adc",
                    "createdDate": "2017-09-25T23:41:25Z",
                    "merchantCategory": "5411",
                    "acquirerId": "375321467",
                    "cardAcceptorId": "4445001899609",
                    "terminalId": "TR5000",
                    "cardAcceptorNameLocation": "Microsoft*Store",
                    "amount": 5000,
                    "currency": "AUD",
                    "outcome": "declined",
                    "message": "Microsoft transaction is only allowed for Single-use cards",
                    "processedAt": "2020-12-07T03:45:35.310194Z",
                    "merchantId": "d3cba82e-1a51-4a79-8271-8d310b992508",
                    "data": {
                        "type": "authorization",
                        "state": "PENDING",
                        "cardToken": "42d7d26f-082c-4c57-835d-3177775b992e",
                        "userToken": "9b776525-312d-46f4-a3ed-36ad430940a1",
                        "amount": 50,
                        "currencyCode": "AUD",
                        "timestamp": "2017-09-25T23:41:25Z",
                        "gpaOrder": {
                            "jit_funding": {
                                "token": "0b199f94-2d5c-4fc4-9357-be998eaeebfd",
                                "method": "pgfs.authorization",
                                "user_token": "9b776525-312d-46f4-a3ed-36ad430940a1",
                                "amount": 50
                            }
                        },
                        "merchant": {
                            "mcc": "5411",
                            "mid": "4445001899609",
                            "name": "Microsoft*Store",
                            "city": "SYDNEY",
                            "state": "NJ",
                            "postal_code": "07020",
                            "country_code": "AUS",
                            "cardAcceptorNameLocation": "Microsoft*Store"
                        },
                        "acquirer": {
                            "institution_id_code": "375321467",
                            "institution_country": "840",
                            "retrieval_reference_number": "526051868288",
                            "system_trace_audit_number": "676127"
                        },
                        "cardSecurityCodeVerification": {
                            "type": "CVV1",
                            "institution_country": {}
                        },
                        "pos": {
                            "pan_entry_mode": "MAG_STRIPE",
                            "pin_entry_mode": "TRUE",
                            "terminal_id": "TR5000",
                            "terminal_attendance": "ATTENDED",
                            "card_holder_presence": "false",
                            "card_presence": "false",
                            "partial_approval_capable": "false",
                            "purchase_amount_only": "false"
                        },
                        "fraud": {
                            "network": {
                                "transaction_risk_score": "86",
                                "account_risk_score": "2"
                            }
                        },
                        "createdTime": "2017-09-25T23:41:25Z",
                        "network": "MASTERCARD",
                        "transactionMetadata": {
                            "payment_channel": "OTHER"
                        },
                        "networkReferenceId": "12345678"
                    },
                    "externalId": "42d7d26f-082c-4c57-835d-3177775b992e",
                    "sourceRequestId": "0b199f94-2d5c-4fc4-9357-be998eaeebfd",
                    "sourceApi": "MARQETA"
                },
                {
                    "id": "56b2672b-4d42-4a36-83db-9e23637eccab",
                    "createdDate": "2017-09-25T23:41:25Z",
                    "merchantCategory": "5411",
                    "acquirerId": "375321467",
                    "cardAcceptorId": "4445001899609",
                    "terminalId": "TR5000",
                    "cardAcceptorNameLocation": "NRID TEST",
                    "amount": 5000,
                    "currency": "AUD",
                    "chargeId": "ch_e9e8f1227bc946c7ad2aeb3e22025ccc",
                    "outcome": "approved",
                    "message": "authorised",
                    "processedAt": "2020-12-09T02:51:43.993556Z",
                    "cardId": "080794dc-8e73-4e94-a0b0-2d3905726c6f",
                    "customerId": "9b776525-312d-46f4-a3ed-36ad430940a1",
                    "merchantId": "5b6ff194-c85f-4645-a72a-03c017c1af81",
                    "data": {
                        "type": "authorization",
                        "state": "PENDING",
                        "cardToken": "42d7d26f-082c-4c57-835d-3177775b992e",
                        "userToken": "9b776525-312d-46f4-a3ed-36ad430940a1",
                        "amount": 50,
                        "currencyCode": "AUD",
                        "timestamp": "2017-09-25T23:41:25Z",
                        "gpaOrder": {
                            "jit_funding": {
                                "token": "0b199f94-2d5c-4fc4-9357-be998eaeebfd",
                                "method": "pgfs.authorization",
                                "user_token": "9b776525-312d-46f4-a3ed-36ad430940a1",
                                "amount": 50
                            }
                        },
                        "merchant": {
                            "mcc": "5411",
                            "mid": "4445001899609",
                            "name": "NRID TEST",
                            "city": "SYDNEY",
                            "state": "NJ",
                            "postal_code": "07020",
                            "country_code": "AUS",
                            "cardAcceptorNameLocation": "NRID TEST"
                        },
                        "acquirer": {
                            "institution_id_code": "375321467",
                            "institution_country": "840",
                            "retrieval_reference_number": "526051868288",
                            "system_trace_audit_number": "676127"
                        },
                        "cardSecurityCodeVerification": {
                            "type": "CVV1",
                            "institution_country": {}
                        },
                        "pos": {
                            "pan_entry_mode": "MAG_STRIPE",
                            "pin_entry_mode": "TRUE",
                            "terminal_id": "TR5000",
                            "terminal_attendance": "ATTENDED",
                            "card_holder_presence": "false",
                            "card_presence": "false",
                            "partial_approval_capable": "false",
                            "purchase_amount_only": "false"
                        },
                        "fraud": {
                            "network": {
                                "transaction_risk_score": "86",
                                "account_risk_score": "2"
                            }
                        },
                        "createdTime": "2017-09-25T23:41:25Z",
                        "network": "MASTERCARD",
                        "transactionMetadata": {
                            "payment_channel": "OTHER"
                        },
                        "networkReferenceId": "12345678"
                    },
                    "externalId": "42d7d26f-082c-4c57-835d-3177775b992e",
                    "sourceRequestId": "0b199f94-2d5c-4fc4-9357-be998eaeebfd",
                    "sourceApi": "MARQETA"
                },
                {
                    "id": "81069831-090b-4497-8331-b9cc1fe5d9bf",
                    "createdDate": "2017-09-25T23:41:25Z",
                    "merchantCategory": "5411",
                    "acquirerId": "375321467",
                    "cardAcceptorId": "4445001899609",
                    "terminalId": "TR5000",
                    "cardAcceptorNameLocation": "NRID TEST",
                    "amount": 5000,
                    "currency": "AUD",
                    "chargeId": "ch_1c14d295d06c46a09fbb8ebabdf40b8c",
                    "outcome": "approved",
                    "message": "authorised",
                    "processedAt": "2020-12-09T03:36:16.225373Z",
                    "cardId": "080794dc-8e73-4e94-a0b0-2d3905726c6f",
                    "customerId": "9b776525-312d-46f4-a3ed-36ad430940a1",
                    "merchantId": "5b6ff194-c85f-4645-a72a-03c017c1af81",
                    "data": {
                        "type": "authorization",
                        "state": "PENDING",
                        "cardToken": "42d7d26f-082c-4c57-835d-3177775b992e",
                        "userToken": "9b776525-312d-46f4-a3ed-36ad430940a1",
                        "amount": 50,
                        "currencyCode": "AUD",
                        "timestamp": "2017-09-25T23:41:25Z",
                        "gpaOrder": {
                            "jit_funding": {
                                "token": "0b199f94-2d5c-4fc4-9357-be998eaeebfd",
                                "method": "pgfs.authorization",
                                "user_token": "9b776525-312d-46f4-a3ed-36ad430940a1",
                                "amount": 50
                            }
                        },
                        "merchant": {
                            "mcc": "5411",
                            "mid": "4445001899609",
                            "name": "NRID TEST",
                            "city": "SYDNEY",
                            "state": "NJ",
                            "postal_code": "07020",
                            "country_code": "AUS",
                            "cardAcceptorNameLocation": "NRID TEST"
                        },
                        "acquirer": {
                            "institution_id_code": "375321467",
                            "institution_country": "840",
                            "retrieval_reference_number": "526051868288",
                            "system_trace_audit_number": "676127"
                        },
                        "cardSecurityCodeVerification": {
                            "type": "CVV1",
                            "institution_country": {}
                        },
                        "pos": {
                            "pan_entry_mode": "MAG_STRIPE",
                            "pin_entry_mode": "TRUE",
                            "terminal_id": "TR5000",
                            "terminal_attendance": "ATTENDED",
                            "card_holder_presence": "false",
                            "card_presence": "false",
                            "partial_approval_capable": "false",
                            "purchase_amount_only": "false"
                        },
                        "fraud": {
                            "network": {
                                "transaction_risk_score": "86",
                                "account_risk_score": "2"
                            }
                        },
                        "createdTime": "2017-09-25T23:41:25Z",
                        "network": "MASTERCARD",
                        "transactionMetadata": {
                            "payment_channel": "OTHER"
                        },
                        "networkReferenceId": "12345678"
                    },
                    "externalId": "42d7d26f-082c-4c57-835d-3177775b992e",
                    "sourceRequestId": "0b199f94-2d5c-4fc4-9357-be998eaeebfd",
                    "sourceApi": "MARQETA"
                }
            ],

    TransactionsByUnexistingNRID: []
        },
};
