
module.exports = {
    requests: {

        },

        responses: {
            transactionsByNRID: [
                {
                    "id": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGCANCEaaaaLaasdaaa",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 1,
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "currency": "AUD",
                    "zipBatchId": 1609907061404,
                    "forcedReprocess": false,
                    "status": "PROCESSED",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "PENDING",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "PENDING",
                        "token": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGCANCEaaaaLaasdaaa",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "225e4ce0-d385-41d8-8767-6f9f45036cf3jit_funding",
                            "amount": 10,
                            "createdTime": "2018-07-10T18:13:26Z",
                            "lastModifiedTime": "2018-07-10T18:13:26Z",
                            "transactionToken": "0ca8d264-6f6b-46fa-a6f7-21ec6342a76e",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 10,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********4ebf",
                                    "active": true,
                                    "name": "PGFS for simulating transactions",
                                    "createdTime": "2018-07-10T18:13:25Z",
                                    "lastModifiedTime": "2018-07-10T18:13:25Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "3fce1245-6fc8-4da6-aab2-0fb43b8a5c85",
                                    "transactionId": "your-jit-funding-token",
                                    "message": "Approved or completed successfully",
                                    "duration": 244,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "your-jit-funding-token",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "your-jit-funding-user",
                                                "amount": 10,
                                                "originalJitFundingToken": "your-jit-funding-token",
                                                "memo": "123"
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********4ebf",
                            "jitFunding": {
                                "token": "b6086727-1513-419e-8b11-c550572274ce",
                                "method": "PGFS_FORCE_CAPTURE",
                                "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "actingUserToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "amount": 10
                            },
                            "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "aaajhgjghgjtestretry123",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 1,
                    "externalId": "0c12c150-5be4-4d2c-942e-7d79d6dad650",
                    "currency": "AUD",
                    "zipBatchId": 1613100087794,
                    "forcedReprocess": false,
                    "declineReason": "JIT response not sufficient funds.",
                    "status": "BATCH_PROCESSED",
                    "stan": "248746",
                    "valueMinor": 500,
                    "sourceRequestId": "9f4ec793-2dfc-4a41-aac2-dbdb348f335e",
                    "eventType": "authorization",
                    "source": "MARQETA",
                    "state": "DECLINED",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION",
                        "state": "DECLINED",
                        "token": "aaajhgjghgjtestretry123",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "0c12c150-5be4-4d2c-942e-7d79d6dad650",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "AUD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "AUD": {
                                    "currencyCode": "AUD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "225e4ce0-d385-41d8-8767-6f9f45036cf3jit_funding",
                            "amount": 10,
                            "createdTime": "2021-07-10T18:13:26Z",
                            "lastModifiedTime": "2021-07-10T18:13:26Z",
                            "transactionToken": "0ca8d264-6f6b-46fa-a6f7-21ec6342a76e",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 10,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********4ebf",
                                    "active": true,
                                    "name": "PGFS for simulating transactions",
                                    "createdTime": "2021-07-10T18:13:25Z",
                                    "lastModifiedTime": "2021-07-10T18:13:25Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "3fce1245-6fc8-4da6-aab2-0fb43b8a5c85",
                                    "transactionId": "your-jit-funding-token",
                                    "message": "Approved or completed successfully",
                                    "duration": 244,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "your-jit-funding-token",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "your-jit-funding-user",
                                                "amount": 10,
                                                "originalJitFundingToken": "your-jit-funding-token",
                                                "memo": "123"
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********4ebf",
                            "jitFunding": {
                                "token": "9f4ec793-2dfc-4a41-aac2-dbdb348f335e",
                                "method": "PGFS_FORCE_CAPTURE",
                                "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "actingUserToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "amount": 10
                            },
                            "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                            "currencyCode": "AUD"
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "aaajhgjghgaaaaaaj",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "declineReason": "JIT response not sufficient funds.",
                    "status": "PROCESSED",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization",
                    "source": "MARQETA",
                    "state": "DECLINED",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION",
                        "state": "DECLINED",
                        "token": "aaajhgjghgaaaaaaj",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "AUD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "AUD": {
                                    "currencyCode": "AUD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "zdshfkjdhasf-sdfhksdaf-lkjdhasf",
                    "timestamp": "2021-06-17T02:34:15Z",
                    "reprocessCount": 1,
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "status": "ERROR",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "PENDING",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "PENDING",
                        "token": "zdshfkjdhasf-sdfhksdaf-lkjdhasf",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2021-06-17T02:34:15Z",
                        "userTransactionTime": "2021-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2021-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "225e4ce0-d385-41d8-8767-6f9f45036cf3jit_funding",
                            "amount": 10,
                            "createdTime": "2021-07-10T18:13:26Z",
                            "lastModifiedTime": "2021-07-10T18:13:26Z",
                            "transactionToken": "0ca8d264-6f6b-46fa-a6f7-21ec6342a76e",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 10,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********4ebf",
                                    "active": true,
                                    "name": "PGFS for simulating transactions",
                                    "createdTime": "2021-07-10T18:13:25Z",
                                    "lastModifiedTime": "2021-07-10T18:13:25Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "3fce1245-6fc8-4da6-aab2-0fb43b8a5c85",
                                    "transactionId": "your-jit-funding-token",
                                    "message": "Approved or completed successfully",
                                    "duration": 244,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "your-jit-funding-token",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "your-jit-funding-user",
                                                "amount": 10,
                                                "originalJitFundingToken": "your-jit-funding-token",
                                                "memo": "123"
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********4ebf",
                            "jitFunding": {
                                "token": "b6086727-1513-419e-8b11-c550572274ce",
                                "method": "PGFS_AUTHORIZATION_CAPTURE",
                                "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "actingUserToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "amount": 10
                            },
                            "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "tttttsdfsdfsdfdddaaa",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 1,
                    "currency": "AUD",
                    "zipBatchId": 1612514322710,
                    "forcedReprocess": false,
                    "declineReason": "JIT response not sufficient funds.",
                    "status": "BATCH_PROCESSED",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization",
                    "source": "MARQETA",
                    "state": "DECLINED",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION",
                        "state": "DECLINED",
                        "token": "tttttsdfsdfsdfdddaaa",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "AUD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "AUD": {
                                    "currencyCode": "AUD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "ee4316a2-63fd-4b8f-9vickytest",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "status": "PROCESSED",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "refund",
                    "source": "MARQETA",
                    "state": "PENDING",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "REFUND",
                        "state": "PENDING",
                        "token": "ee4316a2-63fd-4b8f-9vickytest",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "AUD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "AUD": {
                                    "currencyCode": "AUD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "aaajhgjghgaaaaaaj12asd3111",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "360215f2-9dd6-4e60-8152-6dfb4b50fe12aa455",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "declineReason": "JIT response not sufficient funds.",
                    "status": "NEW",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "DECLINED",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "DECLINED",
                        "token": "aaajhgjghgaaaaaaj12asd3111",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "360215f2-9dd6-4e60-8152-6dfb4b50fe12aa455",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 117,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "impactedAmount": -1,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 117,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0,
                                    "impactedAmount": -1
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "3a1d7012-1742-4ff8-bbc4-405a56508132",
                            "amount": 0.5,
                            "createdTime": "2020-07-08T07:50:58Z",
                            "lastModifiedTime": "2020-07-08T07:50:58Z",
                            "transactionToken": "e973b6f6-6069-48be-9fd0-91a5ece661fd",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 1,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********634d",
                                    "active": true,
                                    "name": "Zip",
                                    "createdTime": "2020-06-16T05:56:22Z",
                                    "lastModifiedTime": "2020-06-16T05:56:22Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "bd33e496-b0df-4498-b005-70a86cbe11e0",
                                    "transactionId": "ffe6b451-f92e-4a48-bd30-43339d5633cf",
                                    "message": "Approved or completed successfully",
                                    "duration": 2245,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "ffe6b451-f92e-4a48-bd30-43339d5633cf",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                                "amount": 1
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********634d",
                            "jitFunding": {
                                "token": "a8ee8f86-34ba-4166-94e7-e77e2cc70344",
                                "method": "PGFS_FORCE_CAPTURE",
                                "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                "actingUserToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                "amount": 1
                            },
                            "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "aaajhgjghgjtestretry123clearing",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "0c12c150-5be4-4d2c-942e-7d79d6dad650",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "declineReason": "JIT response not sufficient funds.",
                    "status": "ERROR",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "DECLINED",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGsasdasdasd",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "DECLINED",
                        "token": "aaajhgjghgjtestretry123clearing",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "0c12c150-5be4-4d2c-942e-7d79d6dad650",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGsasdasdasd",
                        "gpa": {
                            "currencyCode": "AUD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "AUD": {
                                    "currencyCode": "AUD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "225e4ce0-d385-41d8-8767-6f9f45036cf3jit_funding",
                            "amount": 10,
                            "createdTime": "2021-07-10T18:13:26Z",
                            "lastModifiedTime": "2021-07-10T18:13:26Z",
                            "transactionToken": "0ca8d264-6f6b-46fa-a6f7-21ec6342a76e",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 10,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********4ebf",
                                    "active": true,
                                    "name": "PGFS for simulating transactions",
                                    "createdTime": "2021-07-10T18:13:25Z",
                                    "lastModifiedTime": "2021-07-10T18:13:25Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "3fce1245-6fc8-4da6-aab2-0fb43b8a5c85",
                                    "transactionId": "your-jit-funding-token",
                                    "message": "Approved or completed successfully",
                                    "duration": 244,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "your-jit-funding-token",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "your-jit-funding-user",
                                                "amount": 10,
                                                "originalJitFundingToken": "your-jit-funding-token",
                                                "memo": "123"
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********4ebf",
                            "jitFunding": {
                                "token": "9f4ec793-2dfc-4a41-aac2-dbdb348f335e",
                                "method": "PGFS_ADJUSTMENT_CREDIT",
                                "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "actingUserToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "amount": 10
                            },
                            "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                            "currencyCode": "AUD"
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "aaajhgjghgaaaaaaj12asd3111tes23",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "458e66e7-1ac2-45bd-a61c-6de19653e896",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "declineReason": "JIT response not sufficient funds.",
                    "status": "PROCESSED",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "DECLINED",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "DECLINED",
                        "token": "aaajhgjghgaaaaaaj12asd3111tes23",
                        "userToken": "1234",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "458e66e7-1ac2-45bd-a61c-6de19653e896",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 117,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "impactedAmount": -1,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 117,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0,
                                    "impactedAmount": -1
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "3a1d7012-1742-4ff8-bbc4-405a56508132",
                            "amount": 0.5,
                            "createdTime": "2020-07-08T07:50:58Z",
                            "lastModifiedTime": "2020-07-08T07:50:58Z",
                            "transactionToken": "e973b6f6-6069-48be-9fd0-91a5ece661fd",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 1,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********634d",
                                    "active": true,
                                    "name": "Zip",
                                    "createdTime": "2020-06-16T05:56:22Z",
                                    "lastModifiedTime": "2020-06-16T05:56:22Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "bd33e496-b0df-4498-b005-70a86cbe11e0",
                                    "transactionId": "ffe6b451-f92e-4a48-bd30-43339d5633cf",
                                    "message": "Approved or completed successfully",
                                    "duration": 2245,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "ffe6b451-f92e-4a48-bd30-43339d5633cf",
                                                "method": "PGFS_FORCE_CAPTURE",
                                                "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                                "amount": 1
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********634d",
                            "jitFunding": {
                                "token": "a8ee8f86-34ba-4166-94e7-e77e2cc70344",
                                "method": "PGFS_FORCE_CAPTURE",
                                "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                "actingUserToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                "amount": 1
                            },
                            "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "anotehrtoken1233",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "039e762a-8b95-4e24-87f7-91833f449a85",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "status": "PROCESSED",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "PENDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "PENDING",
                        "token": "anotehrtoken1233",
                        "userToken": "testest111v112345777",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "039e762a-8b95-4e24-87f7-91833f449a85",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 117,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "impactedAmount": -1,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 117,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0,
                                    "impactedAmount": -1
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "3a1d7012-1742-4ff8-bbc4-405a56508132",
                            "amount": 0.5,
                            "createdTime": "2020-07-08T07:50:58Z",
                            "lastModifiedTime": "2020-07-08T07:50:58Z",
                            "transactionToken": "e973b6f6-6069-48be-9fd0-91a5ece661fd",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 1,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********634d",
                                    "active": true,
                                    "name": "Zip",
                                    "createdTime": "2020-06-16T05:56:22Z",
                                    "lastModifiedTime": "2020-06-16T05:56:22Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "bd33e496-b0df-4498-b005-70a86cbe11e0",
                                    "transactionId": "ffe6b451-f92e-4a48-bd30-43339d5633cf",
                                    "message": "Approved or completed successfully",
                                    "duration": 2245,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "ffe6b451-f92e-4a48-bd30-43339d5633cf",
                                                "method": "PGFS_FORCE_CAPTURE",
                                                "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                                "amount": 1
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********634d",
                            "jitFunding": {
                                "token": "005252d4-32fc-4d86-b017-782b60c3010c",
                                "method": "PGFS_FORCE_CAPTURE",
                                "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                "actingUserToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                "amount": 1
                            },
                            "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "aaajhgjghgaaaaaaj12asd3111test2",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "360215f2-9dd6-4e60-8152-6dfb4b50fe12aa455",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "declineReason": "JIT response not sufficient funds.",
                    "status": "NEW",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "DECLINED",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "DECLINED",
                        "token": "aaajhgjghgaaaaaaj12asd3111test2",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "360215f2-9dd6-4e60-8152-6dfb4b50fe12aa455",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 117,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "impactedAmount": -1,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 117,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0,
                                    "impactedAmount": -1
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "3a1d7012-1742-4ff8-bbc4-405a56508132",
                            "amount": 0.5,
                            "createdTime": "2020-07-08T07:50:58Z",
                            "lastModifiedTime": "2020-07-08T07:50:58Z",
                            "transactionToken": "e973b6f6-6069-48be-9fd0-91a5ece661fd",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 1,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********634d",
                                    "active": true,
                                    "name": "Zip",
                                    "createdTime": "2020-06-16T05:56:22Z",
                                    "lastModifiedTime": "2020-06-16T05:56:22Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "bd33e496-b0df-4498-b005-70a86cbe11e0",
                                    "transactionId": "ffe6b451-f92e-4a48-bd30-43339d5633cf",
                                    "message": "Approved or completed successfully",
                                    "duration": 2245,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "ffe6b451-f92e-4a48-bd30-43339d5633cf",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                                "amount": 1
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********634d",
                            "jitFunding": {
                                "token": "a8ee8f86-34ba-4166-94e7-e77e2cc70344",
                                "method": "PGFS_FORCE_CAPTURE",
                                "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                "actingUserToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                "amount": 1
                            },
                            "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "aaajhgjghgj",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "declineReason": "JIT response not sufficient funds.",
                    "status": "PROCESSED",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization",
                    "source": "MARQETA",
                    "state": "DECLINED",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION",
                        "state": "DECLINED",
                        "token": "aaajhgjghgj",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "AUD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "AUD": {
                                    "currencyCode": "AUD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "aaaaafixtry",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "declineReason": "JIT response not sufficient funds.",
                    "status": "PROCESSED",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization",
                    "source": "MARQETA",
                    "state": "DECLINED",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION",
                        "state": "DECLINED",
                        "token": "aaaaafixtry",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "AUD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "AUD": {
                                    "currencyCode": "AUD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "aaajhgjghgaaaaaaj12asd3111test",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "360215f2-9dd6-4e60-8152-6dfb4b50fe12aa455",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "declineReason": "JIT response not sufficient funds.",
                    "status": "NEW",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "DECLINED",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "DECLINED",
                        "token": "aaajhgjghgaaaaaaj12asd3111test",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "360215f2-9dd6-4e60-8152-6dfb4b50fe12aa455",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 117,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "impactedAmount": -1,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 117,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0,
                                    "impactedAmount": -1
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "3a1d7012-1742-4ff8-bbc4-405a56508132",
                            "amount": 0.5,
                            "createdTime": "2020-07-08T07:50:58Z",
                            "lastModifiedTime": "2020-07-08T07:50:58Z",
                            "transactionToken": "e973b6f6-6069-48be-9fd0-91a5ece661fd",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 1,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********634d",
                                    "active": true,
                                    "name": "Zip",
                                    "createdTime": "2020-06-16T05:56:22Z",
                                    "lastModifiedTime": "2020-06-16T05:56:22Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "bd33e496-b0df-4498-b005-70a86cbe11e0",
                                    "transactionId": "ffe6b451-f92e-4a48-bd30-43339d5633cf",
                                    "message": "Approved or completed successfully",
                                    "duration": 2245,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "ffe6b451-f92e-4a48-bd30-43339d5633cf",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                                "amount": 1
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********634d",
                            "jitFunding": {
                                "token": "a8ee8f86-34ba-4166-94e7-e77e2cc70344",
                                "method": "PGFS_FORCE_CAPTURE",
                                "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                "actingUserToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                "amount": 1
                            },
                            "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "aaaaafixtryaaa",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 1,
                    "currency": "AUD",
                    "zipBatchId": 1612514322710,
                    "forcedReprocess": false,
                    "declineReason": "JIT response not sufficient funds.",
                    "status": "BATCH_PROCESSED",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization",
                    "source": "MARQETA",
                    "state": "DECLINED",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION",
                        "state": "DECLINED",
                        "token": "aaaaafixtryaaa",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "AUD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "AUD": {
                                    "currencyCode": "AUD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "aaajhgjghgaaaaaaj12asd3111test234",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "360215f2-9dd6-4e60-8152-6dfb4b50fe12aa455",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "declineReason": "JIT response not sufficient funds.",
                    "status": "NEW",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "DECLINED",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "DECLINED",
                        "token": "aaajhgjghgaaaaaaj12asd3111test234",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "360215f2-9dd6-4e60-8152-6dfb4b50fe12aa455",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 117,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "impactedAmount": -1,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 117,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0,
                                    "impactedAmount": -1
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "3a1d7012-1742-4ff8-bbc4-405a56508132",
                            "amount": 0.5,
                            "createdTime": "2020-07-08T07:50:58Z",
                            "lastModifiedTime": "2020-07-08T07:50:58Z",
                            "transactionToken": "e973b6f6-6069-48be-9fd0-91a5ece661fd",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 1,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********634d",
                                    "active": true,
                                    "name": "Zip",
                                    "createdTime": "2020-06-16T05:56:22Z",
                                    "lastModifiedTime": "2020-06-16T05:56:22Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "bd33e496-b0df-4498-b005-70a86cbe11e0",
                                    "transactionId": "ffe6b451-f92e-4a48-bd30-43339d5633cf",
                                    "message": "Approved or completed successfully",
                                    "duration": 2245,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "ffe6b451-f92e-4a48-bd30-43339d5633cf",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                                "amount": 1
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********634d",
                            "jitFunding": {
                                "token": "a8ee8f86-34ba-4166-94e7-e77e2cc70344",
                                "method": "PGFS_FORCE_CAPTURE",
                                "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                "actingUserToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                "amount": 1
                            },
                            "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "aaajhgjghgjtestretry",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 2,
                    "externalId": "0c12c150-5be4-4d2c-942e-7d79d6dad650",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "declineReason": "JIT response not sufficient funds.",
                    "status": "BATCH_PROCESSED_WITH_ISSUES",
                    "stan": "248746",
                    "valueMinor": 500,
                    "sourceRequestId": "9f4ec793-2dfc-4a41-aac2-dbdb348f335e",
                    "eventType": "authorization",
                    "source": "MARQETA",
                    "state": "DECLINED",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION",
                        "state": "DECLINED",
                        "token": "aaajhgjghgjtestretry",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "0c12c150-5be4-4d2c-942e-7d79d6dad650",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "AUD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "AUD": {
                                    "currencyCode": "AUD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "225e4ce0-d385-41d8-8767-6f9f45036cf3jit_funding",
                            "amount": 10,
                            "createdTime": "2021-07-10T18:13:26Z",
                            "lastModifiedTime": "2021-07-10T18:13:26Z",
                            "transactionToken": "0ca8d264-6f6b-46fa-a6f7-21ec6342a76e",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 10,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********4ebf",
                                    "active": true,
                                    "name": "PGFS for simulating transactions",
                                    "createdTime": "2021-07-10T18:13:25Z",
                                    "lastModifiedTime": "2021-07-10T18:13:25Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "3fce1245-6fc8-4da6-aab2-0fb43b8a5c85",
                                    "transactionId": "your-jit-funding-token",
                                    "message": "Approved or completed successfully",
                                    "duration": 244,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "your-jit-funding-token",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "your-jit-funding-user",
                                                "amount": 10,
                                                "originalJitFundingToken": "your-jit-funding-token",
                                                "memo": "123"
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********4ebf",
                            "jitFunding": {
                                "token": "9f4ec793-2dfc-4a41-aac2-dbdb348f335e",
                                "method": "PGFS_FORCE_CAPTURE",
                                "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "actingUserToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "amount": 10
                            },
                            "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                            "currencyCode": "AUD"
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "tttttsdfsdfsdfddd",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 1,
                    "currency": "AUD",
                    "zipBatchId": 1612514322710,
                    "forcedReprocess": false,
                    "declineReason": "JIT response not sufficient funds.",
                    "status": "BATCH_PROCESSED",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization",
                    "source": "MARQETA",
                    "state": "DECLINED",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION",
                        "state": "DECLINED",
                        "token": "tttttsdfsdfsdfddd",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "AUD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "AUD": {
                                    "currencyCode": "AUD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "aaajhgjghgaaaaaaj12asd3",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "declineReason": "JIT response not sufficient funds.",
                    "status": "PROCESSED",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization",
                    "source": "MARQETA",
                    "state": "DECLINED",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION",
                        "state": "DECLINED",
                        "token": "aaajhgjghgaaaaaaj12asd3",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "anotehrtoken",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "039e762a-8b95-4e24-87f7-91833f449a85",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "status": "PROCESSED",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "PENDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "PENDING",
                        "token": "anotehrtoken",
                        "userToken": "testest111v112345777",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "039e762a-8b95-4e24-87f7-91833f449a85",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 117,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "impactedAmount": -1,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 117,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0,
                                    "impactedAmount": -1
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "3a1d7012-1742-4ff8-bbc4-405a56508132",
                            "amount": 0.5,
                            "createdTime": "2020-07-08T07:50:58Z",
                            "lastModifiedTime": "2020-07-08T07:50:58Z",
                            "transactionToken": "e973b6f6-6069-48be-9fd0-91a5ece661fd",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 1,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********634d",
                                    "active": true,
                                    "name": "Zip",
                                    "createdTime": "2020-06-16T05:56:22Z",
                                    "lastModifiedTime": "2020-06-16T05:56:22Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "bd33e496-b0df-4498-b005-70a86cbe11e0",
                                    "transactionId": "ffe6b451-f92e-4a48-bd30-43339d5633cf",
                                    "message": "Approved or completed successfully",
                                    "duration": 2245,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "ffe6b451-f92e-4a48-bd30-43339d5633cf",
                                                "method": "PGFS_FORCE_CAPTURE",
                                                "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                                "amount": 1
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********634d",
                            "jitFunding": {
                                "token": "005252d4-32fc-4d86-b017-782b60c3010c",
                                "method": "PGFS_FORCE_CAPTURE",
                                "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                "actingUserToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                "amount": 1
                            },
                            "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGCANCEaaaaLaasssdaaa",
                    "timestamp": "2021-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "status": "ERROR",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "PENDING",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "PENDING",
                        "token": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGCANCEaaaaLaasssdaaa",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2021-06-17T02:34:15Z",
                        "userTransactionTime": "2021-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2021-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "225e4ce0-d385-41d8-8767-6f9f45036cf3jit_funding",
                            "amount": 10,
                            "createdTime": "2021-07-10T18:13:26Z",
                            "lastModifiedTime": "2021-07-10T18:13:26Z",
                            "transactionToken": "0ca8d264-6f6b-46fa-a6f7-21ec6342a76e",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 10,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********4ebf",
                                    "active": true,
                                    "name": "PGFS for simulating transactions",
                                    "createdTime": "2021-07-10T18:13:25Z",
                                    "lastModifiedTime": "2021-07-10T18:13:25Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "3fce1245-6fc8-4da6-aab2-0fb43b8a5c85",
                                    "transactionId": "your-jit-funding-token",
                                    "message": "Approved or completed successfully",
                                    "duration": 244,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "your-jit-funding-token",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "your-jit-funding-user",
                                                "amount": 10,
                                                "originalJitFundingToken": "your-jit-funding-token",
                                                "memo": "123"
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********4ebf",
                            "jitFunding": {
                                "token": "b6086727-1513-419e-8b11-c550572274ce",
                                "method": "PGFS_AUTHORIZATION_CAPTURE",
                                "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "actingUserToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "amount": 10
                            },
                            "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "aaajhgjghgaaaaaaj12asd3111test23",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "360215f2-9dd6-4e60-8152-6dfb4b50fe12aa455",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "declineReason": "JIT response not sufficient funds.",
                    "status": "NEW",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "DECLINED",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "DECLINED",
                        "token": "aaajhgjghgaaaaaaj12asd3111test23",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "360215f2-9dd6-4e60-8152-6dfb4b50fe12aa455",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 117,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "impactedAmount": -1,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 117,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0,
                                    "impactedAmount": -1
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "3a1d7012-1742-4ff8-bbc4-405a56508132",
                            "amount": 0.5,
                            "createdTime": "2020-07-08T07:50:58Z",
                            "lastModifiedTime": "2020-07-08T07:50:58Z",
                            "transactionToken": "e973b6f6-6069-48be-9fd0-91a5ece661fd",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 1,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********634d",
                                    "active": true,
                                    "name": "Zip",
                                    "createdTime": "2020-06-16T05:56:22Z",
                                    "lastModifiedTime": "2020-06-16T05:56:22Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "bd33e496-b0df-4498-b005-70a86cbe11e0",
                                    "transactionId": "ffe6b451-f92e-4a48-bd30-43339d5633cf",
                                    "message": "Approved or completed successfully",
                                    "duration": 2245,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "ffe6b451-f92e-4a48-bd30-43339d5633cf",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                                "amount": 1
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********634d",
                            "jitFunding": {
                                "token": "a8ee8f86-34ba-4166-94e7-e77e2cc70344",
                                "method": "PGFS_FORCE_CAPTURE",
                                "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                "actingUserToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                "amount": 1
                            },
                            "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "aaajhgjghgaaaaaaj123",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "declineReason": "JIT response not sufficient funds.",
                    "status": "PROCESSED",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization",
                    "source": "MARQETA",
                    "state": "DECLINED",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION",
                        "state": "DECLINED",
                        "token": "aaajhgjghgaaaaaaj123",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "aaajhgjghgaaaaaaj12asd3111test2345",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "458e66e7-1ac2-45bd-a61c-6de19653e896",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "declineReason": "JIT response not sufficient funds.",
                    "status": "PROCESSED",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "DECLINED",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "DECLINED",
                        "token": "aaajhgjghgaaaaaaj12asd3111test2345",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce111",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "458e66e7-1ac2-45bd-a61c-6de19653e896",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 117,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "impactedAmount": -1,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 117,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0,
                                    "impactedAmount": -1
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "3a1d7012-1742-4ff8-bbc4-405a56508132",
                            "amount": 0.5,
                            "createdTime": "2020-07-08T07:50:58Z",
                            "lastModifiedTime": "2020-07-08T07:50:58Z",
                            "transactionToken": "e973b6f6-6069-48be-9fd0-91a5ece661fd",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 1,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********634d",
                                    "active": true,
                                    "name": "Zip",
                                    "createdTime": "2020-06-16T05:56:22Z",
                                    "lastModifiedTime": "2020-06-16T05:56:22Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "bd33e496-b0df-4498-b005-70a86cbe11e0",
                                    "transactionId": "ffe6b451-f92e-4a48-bd30-43339d5633cf",
                                    "message": "Approved or completed successfully",
                                    "duration": 2245,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "ffe6b451-f92e-4a48-bd30-43339d5633cf",
                                                "method": "PGFS_FORCE_CAPTURE",
                                                "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                                "amount": 1
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********634d",
                            "jitFunding": {
                                "token": "a8ee8f86-34ba-4166-94e7-e77e2cc70344",
                                "method": "PGFS_FORCE_CAPTURE",
                                "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                "actingUserToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                                "amount": 1
                            },
                            "userToken": "5ea3c679-0722-48a4-8740-19b60c70b1b6",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGCANCEaaaaLaaaa",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 1,
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "currency": "AUD",
                    "zipBatchId": 1609907061404,
                    "forcedReprocess": false,
                    "status": "BATCH_PROCESSED",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "PENDING",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "PENDING",
                        "token": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGCANCEaaaaLaaaa",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "225e4ce0-d385-41d8-8767-6f9f45036cf3jit_funding",
                            "amount": 10,
                            "createdTime": "2018-07-10T18:13:26Z",
                            "lastModifiedTime": "2018-07-10T18:13:26Z",
                            "transactionToken": "0ca8d264-6f6b-46fa-a6f7-21ec6342a76e",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 10,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********4ebf",
                                    "active": true,
                                    "name": "PGFS for simulating transactions",
                                    "createdTime": "2018-07-10T18:13:25Z",
                                    "lastModifiedTime": "2018-07-10T18:13:25Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "3fce1245-6fc8-4da6-aab2-0fb43b8a5c85",
                                    "transactionId": "your-jit-funding-token",
                                    "message": "Approved or completed successfully",
                                    "duration": 244,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "your-jit-funding-token",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "your-jit-funding-user",
                                                "amount": 10,
                                                "originalJitFundingToken": "your-jit-funding-token",
                                                "memo": "123"
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********4ebf",
                            "jitFunding": {
                                "token": "b6086727-1513-419e-8b11-c550572274ce",
                                "method": "PGFS_FORCE_CAPTURE",
                                "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "actingUserToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "amount": 10
                            },
                            "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "12345678"
                }
            ],
            transactionsByUnexistingNRID: [],
            transactionsByExternalId: [
                {
                    "id": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGCANCEaaaaLaasdaaa",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 1,
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "currency": "AUD",
                    "zipBatchId": 1609907061404,
                    "forcedReprocess": false,
                    "status": "PROCESSED",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "PENDING",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "PENDING",
                        "token": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGCANCEaaaaLaasdaaa",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "225e4ce0-d385-41d8-8767-6f9f45036cf3jit_funding",
                            "amount": 10,
                            "createdTime": "2018-07-10T18:13:26Z",
                            "lastModifiedTime": "2018-07-10T18:13:26Z",
                            "transactionToken": "0ca8d264-6f6b-46fa-a6f7-21ec6342a76e",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 10,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********4ebf",
                                    "active": true,
                                    "name": "PGFS for simulating transactions",
                                    "createdTime": "2018-07-10T18:13:25Z",
                                    "lastModifiedTime": "2018-07-10T18:13:25Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "3fce1245-6fc8-4da6-aab2-0fb43b8a5c85",
                                    "transactionId": "your-jit-funding-token",
                                    "message": "Approved or completed successfully",
                                    "duration": 244,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "your-jit-funding-token",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "your-jit-funding-user",
                                                "amount": 10,
                                                "originalJitFundingToken": "your-jit-funding-token",
                                                "memo": "123"
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********4ebf",
                            "jitFunding": {
                                "token": "b6086727-1513-419e-8b11-c550572274ce",
                                "method": "PGFS_FORCE_CAPTURE",
                                "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "actingUserToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "amount": 10
                            },
                            "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "zdshfkjdhasf-sdfhksdaf-lkjdhasf",
                    "timestamp": "2021-06-17T02:34:15Z",
                    "reprocessCount": 1,
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "status": "ERROR",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "PENDING",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "PENDING",
                        "token": "zdshfkjdhasf-sdfhksdaf-lkjdhasf",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2021-06-17T02:34:15Z",
                        "userTransactionTime": "2021-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2021-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "225e4ce0-d385-41d8-8767-6f9f45036cf3jit_funding",
                            "amount": 10,
                            "createdTime": "2021-07-10T18:13:26Z",
                            "lastModifiedTime": "2021-07-10T18:13:26Z",
                            "transactionToken": "0ca8d264-6f6b-46fa-a6f7-21ec6342a76e",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 10,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********4ebf",
                                    "active": true,
                                    "name": "PGFS for simulating transactions",
                                    "createdTime": "2021-07-10T18:13:25Z",
                                    "lastModifiedTime": "2021-07-10T18:13:25Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "3fce1245-6fc8-4da6-aab2-0fb43b8a5c85",
                                    "transactionId": "your-jit-funding-token",
                                    "message": "Approved or completed successfully",
                                    "duration": 244,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "your-jit-funding-token",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "your-jit-funding-user",
                                                "amount": 10,
                                                "originalJitFundingToken": "your-jit-funding-token",
                                                "memo": "123"
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********4ebf",
                            "jitFunding": {
                                "token": "b6086727-1513-419e-8b11-c550572274ce",
                                "method": "PGFS_AUTHORIZATION_CAPTURE",
                                "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "actingUserToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "amount": 10
                            },
                            "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "dsafdsaf-dslf-dsfjhds-3244",
                    "timestamp": "2021-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "status": "PROCESSED",
                    "stan": "248746",
                    "valueMinor": 600,
                    "eventType": "refund",
                    "source": "MARQETA",
                    "state": "PENDING",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "REFUND",
                        "state": "PENDING",
                        "token": "dsafdsaf-dslf-dsfjhds-3244",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2021-06-17T02:34:15Z",
                        "userTransactionTime": "2021-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 6,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2021-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "anonenridjkj",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Test refund",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "225e4ce0-d385-41d8-8767-6f9f45036cf3jit_funding",
                            "amount": 10,
                            "createdTime": "2021-07-10T18:13:26Z",
                            "lastModifiedTime": "2021-07-10T18:13:26Z",
                            "transactionToken": "0ca8d264-6f6b-46fa-a6f7-21ec6342a76e",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 10,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********4ebf",
                                    "active": true,
                                    "name": "PGFS for simulating transactions",
                                    "createdTime": "2021-07-10T18:13:25Z",
                                    "lastModifiedTime": "2021-07-10T18:13:25Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "3fce1245-6fc8-4da6-aab2-0fb43b8a5c85",
                                    "transactionId": "your-jit-funding-token",
                                    "message": "Approved or completed successfully",
                                    "duration": 244,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "your-jit-funding-token",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "your-jit-funding-user",
                                                "amount": 10,
                                                "originalJitFundingToken": "your-jit-funding-token",
                                                "memo": "123"
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********4ebf",
                            "jitFunding": {
                                "token": "b6086727-1513-419e-8b11-c550572274ce",
                                "method": "PGFS_AUTHORIZATION_CAPTURE",
                                "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "actingUserToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "amount": 10
                            },
                            "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "anonenridjkj"
                },
                {
                    "id": "dsafdsaf-dslf-dsfjhds-1234",
                    "timestamp": "2021-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "status": "PROCESSED",
                    "stan": "248746",
                    "valueMinor": 600,
                    "eventType": "refund",
                    "source": "MARQETA",
                    "state": "PENDING",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "REFUND",
                        "state": "PENDING",
                        "token": "dsafdsaf-dslf-dsfjhds-1234",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2021-06-17T02:34:15Z",
                        "userTransactionTime": "2021-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 6,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2021-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "anonenridjkj",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Test refund",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "225e4ce0-d385-41d8-8767-6f9f45036cf3jit_funding",
                            "amount": 10,
                            "createdTime": "2021-07-10T18:13:26Z",
                            "lastModifiedTime": "2021-07-10T18:13:26Z",
                            "transactionToken": "0ca8d264-6f6b-46fa-a6f7-21ec6342a76e",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 10,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********4ebf",
                                    "active": true,
                                    "name": "PGFS for simulating transactions",
                                    "createdTime": "2021-07-10T18:13:25Z",
                                    "lastModifiedTime": "2021-07-10T18:13:25Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "3fce1245-6fc8-4da6-aab2-0fb43b8a5c85",
                                    "transactionId": "your-jit-funding-token",
                                    "message": "Approved or completed successfully",
                                    "duration": 244,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "your-jit-funding-token",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "your-jit-funding-user",
                                                "amount": 10,
                                                "originalJitFundingToken": "your-jit-funding-token",
                                                "memo": "123"
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********4ebf",
                            "jitFunding": {
                                "token": "b6086727-1513-419e-8b11-c550572274ce",
                                "method": "PGFS_AUTHORIZATION_CAPTURE",
                                "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "actingUserToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "amount": 10
                            },
                            "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "anonenridjkj"
                },
                {
                    "id": "dsafdsaf-dslf-dsfjhds-clearing2",
                    "timestamp": "2021-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "status": "ERROR",
                    "stan": "248746",
                    "valueMinor": 250,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "PENDING",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "PENDING",
                        "token": "dsafdsaf-dslf-dsfjhds-clearing2",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2021-06-17T02:34:15Z",
                        "userTransactionTime": "2021-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 2.5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2021-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "sadas",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "225e4ce0-d385-41d8-8767-6f9f45036cf3jit_funding",
                            "amount": 10,
                            "createdTime": "2021-07-10T18:13:26Z",
                            "lastModifiedTime": "2021-07-10T18:13:26Z",
                            "transactionToken": "0ca8d264-6f6b-46fa-a6f7-21ec6342a76e",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 10,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********4ebf",
                                    "active": true,
                                    "name": "PGFS for simulating transactions",
                                    "createdTime": "2021-07-10T18:13:25Z",
                                    "lastModifiedTime": "2021-07-10T18:13:25Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "3fce1245-6fc8-4da6-aab2-0fb43b8a5c85",
                                    "transactionId": "your-jit-funding-token",
                                    "message": "Approved or completed successfully",
                                    "duration": 244,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "your-jit-funding-token",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "your-jit-funding-user",
                                                "amount": 10,
                                                "originalJitFundingToken": "your-jit-funding-token",
                                                "memo": "123"
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********4ebf",
                            "jitFunding": {
                                "token": "b6086727-1513-419e-8b11-c550572274ce",
                                "method": "PGFS_AUTHORIZATION_CAPTURE",
                                "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "actingUserToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "amount": 10
                            },
                            "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "sadas"
                },
                {
                    "id": "dsafdsaf-dslf-dsfjhds",
                    "timestamp": "2021-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "status": "PROCESSED",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "refund",
                    "source": "MARQETA",
                    "state": "PENDING",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "REFUND",
                        "state": "PENDING",
                        "token": "dsafdsaf-dslf-dsfjhds",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2021-06-17T02:34:15Z",
                        "userTransactionTime": "2021-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2021-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "anonenridjkj",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "225e4ce0-d385-41d8-8767-6f9f45036cf3jit_funding",
                            "amount": 10,
                            "createdTime": "2021-07-10T18:13:26Z",
                            "lastModifiedTime": "2021-07-10T18:13:26Z",
                            "transactionToken": "0ca8d264-6f6b-46fa-a6f7-21ec6342a76e",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 10,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********4ebf",
                                    "active": true,
                                    "name": "PGFS for simulating transactions",
                                    "createdTime": "2021-07-10T18:13:25Z",
                                    "lastModifiedTime": "2021-07-10T18:13:25Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "3fce1245-6fc8-4da6-aab2-0fb43b8a5c85",
                                    "transactionId": "your-jit-funding-token",
                                    "message": "Approved or completed successfully",
                                    "duration": 244,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "your-jit-funding-token",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "your-jit-funding-user",
                                                "amount": 10,
                                                "originalJitFundingToken": "your-jit-funding-token",
                                                "memo": "123"
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********4ebf",
                            "jitFunding": {
                                "token": "b6086727-1513-419e-8b11-c550572274ce",
                                "method": "PGFS_AUTHORIZATION_CAPTURE",
                                "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "actingUserToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "amount": 10
                            },
                            "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "anonenridjkj"
                },
                {
                    "id": "dsafdsaf-dslf-dsfjhds-clearing",
                    "timestamp": "2021-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "status": "ERROR",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "PENDING",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "PENDING",
                        "token": "dsafdsaf-dslf-dsfjhds-clearing",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2021-06-17T02:34:15Z",
                        "userTransactionTime": "2021-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2021-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "sadas",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "225e4ce0-d385-41d8-8767-6f9f45036cf3jit_funding",
                            "amount": 10,
                            "createdTime": "2021-07-10T18:13:26Z",
                            "lastModifiedTime": "2021-07-10T18:13:26Z",
                            "transactionToken": "0ca8d264-6f6b-46fa-a6f7-21ec6342a76e",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 10,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********4ebf",
                                    "active": true,
                                    "name": "PGFS for simulating transactions",
                                    "createdTime": "2021-07-10T18:13:25Z",
                                    "lastModifiedTime": "2021-07-10T18:13:25Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "3fce1245-6fc8-4da6-aab2-0fb43b8a5c85",
                                    "transactionId": "your-jit-funding-token",
                                    "message": "Approved or completed successfully",
                                    "duration": 244,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "your-jit-funding-token",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "your-jit-funding-user",
                                                "amount": 10,
                                                "originalJitFundingToken": "your-jit-funding-token",
                                                "memo": "123"
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********4ebf",
                            "jitFunding": {
                                "token": "b6086727-1513-419e-8b11-c550572274ce",
                                "method": "PGFS_AUTHORIZATION_CAPTURE",
                                "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "actingUserToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "amount": 10
                            },
                            "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "sadas"
                },
                {
                    "id": "dsafdsaf-dslf-dsfjhds-32445",
                    "timestamp": "2021-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "status": "PROCESSED",
                    "stan": "248746",
                    "valueMinor": 600,
                    "eventType": "refund",
                    "source": "MARQETA",
                    "state": "PENDING",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "REFUND",
                        "state": "PENDING",
                        "token": "dsafdsaf-dslf-dsfjhds-32445",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2021-06-17T02:34:15Z",
                        "userTransactionTime": "2021-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 6,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2021-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "anonenridjkj",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Test refund",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "225e4ce0-d385-41d8-8767-6f9f45036cf3jit_funding",
                            "amount": 10,
                            "createdTime": "2021-07-10T18:13:26Z",
                            "lastModifiedTime": "2021-07-10T18:13:26Z",
                            "transactionToken": "0ca8d264-6f6b-46fa-a6f7-21ec6342a76e",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 10,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********4ebf",
                                    "active": true,
                                    "name": "PGFS for simulating transactions",
                                    "createdTime": "2021-07-10T18:13:25Z",
                                    "lastModifiedTime": "2021-07-10T18:13:25Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "3fce1245-6fc8-4da6-aab2-0fb43b8a5c85",
                                    "transactionId": "your-jit-funding-token",
                                    "message": "Approved or completed successfully",
                                    "duration": 244,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "your-jit-funding-token",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "your-jit-funding-user",
                                                "amount": 10,
                                                "originalJitFundingToken": "your-jit-funding-token",
                                                "memo": "123"
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********4ebf",
                            "jitFunding": {
                                "token": "b6086727-1513-419e-8b11-c550572274ce",
                                "method": "PGFS_AUTHORIZATION_CAPTURE",
                                "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "actingUserToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "amount": 10
                            },
                            "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "anonenridjkj"
                },
                {
                    "id": "dsafdsaf-dslf-dsfjhds-12345",
                    "timestamp": "2021-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "status": "PROCESSED",
                    "stan": "248746",
                    "valueMinor": 600,
                    "eventType": "refund",
                    "source": "MARQETA",
                    "state": "PENDING",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "REFUND",
                        "state": "PENDING",
                        "token": "dsafdsaf-dslf-dsfjhds-12345",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2021-06-17T02:34:15Z",
                        "userTransactionTime": "2021-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 6,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2021-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "anonenridjkj",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Test refund",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "country": "AU",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "225e4ce0-d385-41d8-8767-6f9f45036cf3jit_funding",
                            "amount": 10,
                            "createdTime": "2021-07-10T18:13:26Z",
                            "lastModifiedTime": "2021-07-10T18:13:26Z",
                            "transactionToken": "0ca8d264-6f6b-46fa-a6f7-21ec6342a76e",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 10,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********4ebf",
                                    "active": true,
                                    "name": "PGFS for simulating transactions",
                                    "createdTime": "2021-07-10T18:13:25Z",
                                    "lastModifiedTime": "2021-07-10T18:13:25Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "3fce1245-6fc8-4da6-aab2-0fb43b8a5c85",
                                    "transactionId": "your-jit-funding-token",
                                    "message": "Approved or completed successfully",
                                    "duration": 244,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "your-jit-funding-token",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "your-jit-funding-user",
                                                "amount": 10,
                                                "originalJitFundingToken": "your-jit-funding-token",
                                                "memo": "123"
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********4ebf",
                            "jitFunding": {
                                "token": "b6086727-1513-419e-8b11-c550572274ce",
                                "method": "PGFS_AUTHORIZATION_CAPTURE",
                                "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "actingUserToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "amount": 10
                            },
                            "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "anonenridjkj"
                },
                {
                    "id": "dsafdsaf-dslf-dsfjhds-clearing333",
                    "timestamp": "2021-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "status": "ERROR",
                    "stan": "248746",
                    "valueMinor": 250,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "PENDING",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "PENDING",
                        "token": "dsafdsaf-dslf-dsfjhds-clearing333",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2021-06-17T02:34:15Z",
                        "userTransactionTime": "2021-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 2.5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2021-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "sadas",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "225e4ce0-d385-41d8-8767-6f9f45036cf3jit_funding",
                            "amount": 10,
                            "createdTime": "2021-07-10T18:13:26Z",
                            "lastModifiedTime": "2021-07-10T18:13:26Z",
                            "transactionToken": "0ca8d264-6f6b-46fa-a6f7-21ec6342a76e",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 10,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********4ebf",
                                    "active": true,
                                    "name": "PGFS for simulating transactions",
                                    "createdTime": "2021-07-10T18:13:25Z",
                                    "lastModifiedTime": "2021-07-10T18:13:25Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "3fce1245-6fc8-4da6-aab2-0fb43b8a5c85",
                                    "transactionId": "your-jit-funding-token",
                                    "message": "Approved or completed successfully",
                                    "duration": 244,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "your-jit-funding-token",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "your-jit-funding-user",
                                                "amount": 10,
                                                "originalJitFundingToken": "your-jit-funding-token",
                                                "memo": "123"
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********4ebf",
                            "jitFunding": {
                                "token": "b6086727-1513-419e-8b11-c550572274ce",
                                "method": "PGFS_AUTHORIZATION_CAPTURE",
                                "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "actingUserToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "amount": 10
                            },
                            "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "sadas"
                },
                {
                    "id": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGCANCEaaaaLaasssdaaa",
                    "timestamp": "2021-06-17T02:34:15Z",
                    "reprocessCount": 0,
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "currency": "AUD",
                    "forcedReprocess": false,
                    "status": "ERROR",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "PENDING",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "PENDING",
                        "token": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGCANCEaaaaLaasssdaaa",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2021-06-17T02:34:15Z",
                        "userTransactionTime": "2021-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2021-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "225e4ce0-d385-41d8-8767-6f9f45036cf3jit_funding",
                            "amount": 10,
                            "createdTime": "2021-07-10T18:13:26Z",
                            "lastModifiedTime": "2021-07-10T18:13:26Z",
                            "transactionToken": "0ca8d264-6f6b-46fa-a6f7-21ec6342a76e",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 10,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********4ebf",
                                    "active": true,
                                    "name": "PGFS for simulating transactions",
                                    "createdTime": "2021-07-10T18:13:25Z",
                                    "lastModifiedTime": "2021-07-10T18:13:25Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "3fce1245-6fc8-4da6-aab2-0fb43b8a5c85",
                                    "transactionId": "your-jit-funding-token",
                                    "message": "Approved or completed successfully",
                                    "duration": 244,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "your-jit-funding-token",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "your-jit-funding-user",
                                                "amount": 10,
                                                "originalJitFundingToken": "your-jit-funding-token",
                                                "memo": "123"
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********4ebf",
                            "jitFunding": {
                                "token": "b6086727-1513-419e-8b11-c550572274ce",
                                "method": "PGFS_AUTHORIZATION_CAPTURE",
                                "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "actingUserToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "amount": 10
                            },
                            "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "12345678"
                },
                {
                    "id": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGCANCEaaaaLaaaa",
                    "timestamp": "2020-06-17T02:34:15Z",
                    "reprocessCount": 1,
                    "externalId": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                    "currency": "AUD",
                    "zipBatchId": 1609907061404,
                    "forcedReprocess": false,
                    "status": "BATCH_PROCESSED",
                    "stan": "248746",
                    "valueMinor": 500,
                    "eventType": "authorization.clearing",
                    "source": "MARQETA",
                    "state": "PENDING",
                    "referenceId": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                    "requestAmount": 10,
                    "marqetaData": {
                        "type": "AUTHORIZATION_CLEARING",
                        "state": "PENDING",
                        "token": "ee4316a2-63fd-4b8f-97d8-JITFUNDINGCANCEaaaaLaaaa",
                        "userToken": "8c7b7e75-ea6b-40e5-886e-b1cc62ea46ce",
                        "actingUserToken": "e9e38889-11f1-4f78-aa79-da80ea817fc5",
                        "cardToken": "52eed3e8-65c2-431a-b471-002c6a9962bd",
                        "precedingRelatedTransactionToken": "ee4316a2-63fd-4b8f-97d8-JITFUNDING",
                        "gpa": {
                            "currencyCode": "USD",
                            "ledgerBalance": 0,
                            "availableBalance": 0,
                            "creditBalance": 0,
                            "pendingCredits": 0,
                            "balances": {
                                "USD": {
                                    "currencyCode": "USD",
                                    "ledgerBalance": 0,
                                    "availableBalance": 0,
                                    "creditBalance": 0,
                                    "pendingCredits": 0
                                }
                            }
                        },
                        "createdTime": "2020-06-17T02:34:15Z",
                        "userTransactionTime": "2020-06-17T02:34:15Z",
                        "requestAmount": 10,
                        "amount": 5,
                        "currency": "AUD",
                        "response": {
                            "code": "1884",
                            "memo": "JIT response not sufficient funds."
                        },
                        "network": "DISCOVER",
                        "acquirerFeeAmount": 0,
                        "acquirer": {
                            "stan": "248746"
                        },
                        "user": {
                            "metadata": {}
                        },
                        "card": {
                            "metadata": {},
                            "lastFour": "8949"
                        },
                        "issuerReceivedTime": "2020-06-17T02:34:15.312Z",
                        "issuerPaymentNode": "00b8d031e0a4759766b5b5266f5229d8",
                        "networkReferenceId": "12345678",
                        "cardAcceptor": {
                            "mid": "123456890",
                            "mcc": "6411",
                            "networkMid": "123456890",
                            "name": "Chicken Tooth Music",
                            "address": "111 Main St",
                            "city": "Berkeley",
                            "state": "CA",
                            "zip": "94702",
                            "countryCode": "USA",
                            "poi": {
                                "partialApprovalcapable": "0"
                            }
                        },
                        "gpaOrder": {
                            "token": "225e4ce0-d385-41d8-8767-6f9f45036cf3jit_funding",
                            "amount": 10,
                            "createdTime": "2018-07-10T18:13:26Z",
                            "lastModifiedTime": "2018-07-10T18:13:26Z",
                            "transactionToken": "0ca8d264-6f6b-46fa-a6f7-21ec6342a76e",
                            "state": "PENDING",
                            "response": {
                                "code": "0000",
                                "memo": "Approved or completed successfully"
                            },
                            "funding": {
                                "amount": 10,
                                "source": {
                                    "type": "programgateway",
                                    "token": "**********4ebf",
                                    "active": true,
                                    "name": "PGFS for simulating transactions",
                                    "createdTime": "2018-07-10T18:13:25Z",
                                    "lastModifiedTime": "2018-07-10T18:13:25Z"
                                },
                                "gatewayLog": {
                                    "orderNumber": "3fce1245-6fc8-4da6-aab2-0fb43b8a5c85",
                                    "transactionId": "your-jit-funding-token",
                                    "message": "Approved or completed successfully",
                                    "duration": 244,
                                    "timedOut": false,
                                    "response": {
                                        "code": "200",
                                        "data": {
                                            "jitFunding": {
                                                "token": "your-jit-funding-token",
                                                "method": "PGFS_AUTHORIZATION",
                                                "userToken": "your-jit-funding-user",
                                                "amount": 10,
                                                "originalJitFundingToken": "your-jit-funding-token",
                                                "memo": "123"
                                            }
                                        }
                                    }
                                }
                            },
                            "fundingSourceToken": "**********4ebf",
                            "jitFunding": {
                                "token": "b6086727-1513-419e-8b11-c550572274ce",
                                "method": "PGFS_FORCE_CAPTURE",
                                "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "actingUserToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                                "amount": 10
                            },
                            "userToken": "b2d2c739-a9fe-4af0-9f34-964aae4c3e31",
                            "currencyCode": "USD"
                        }
                    },
                    "networkReferenceId": "12345678"
                }
            ],
            transactionsByUnexistingExternalId: [],
        },
};
