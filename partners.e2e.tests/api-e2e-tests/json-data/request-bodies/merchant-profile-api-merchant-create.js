import randStr from '../../utils/api-helper'


export const requests = {
    validBody: {
        "CountryCode": "AU",
        "name": "test2021032902",
        "DisplayName": "test2021032902",
        "ABN": "11223491505",
        "BankAccountNumber": "12345678",
        "BsbNumber": "123456",
        "BankAccountName": "test2021032902",
        "TradingName": "test trading name",
        "WebsitePlatform": "test website",
        "Address": "Sydney",
        "WebsiteUrl": "http://www.google.com",
        "SettlementEmail": "meow-2021032902@mailinator.com",
        "adminEmail": `${randStr.getRandomString(5)}@zipteam222898.testinator.com`,
        "AdminFirstName": "fn-test2021032902",
        "AdminLastName": "ln-test2021032902",
        "AdminPhone": "0400000000",
        "DirectorName": "DirectorName",
        "DirectorEmail": "director88@zipteam222898.testinator.com",
        "DirectorPhone": "0400000000",
        "IsStrategicMerchant": 1,
        "AnnualTurnover": 1,
        "IntendedUse": 1,
        "CompanyName": "test",
        "Category": "Cosmetic & Beauty Retailers",
        "SubCategory": "Other",
        "CompanyId": "10"
    },
    duplicateEmail: {
        "CountryCode": "AU",
        "name": "test2021032902",
        "DisplayName": "test2021032902",
        "ABN": "11223491505",
        "BankAccountNumber": "12345678",
        "BsbNumber": "123456",
        "BankAccountName": "test2021032902",
        "TradingName": "test trading name",
        "WebsitePlatform": "test website",
        "Address": "Sydney",
        "WebsiteUrl": "http://www.google.com",
        "SettlementEmail": "meow-2021032902@mailinator.com",
        "adminEmail": `basduplicateemail@mailinator.com`,
        "AdminFirstName": "fn-test2021032902",
        "AdminLastName": "ln-test2021032902",
        "AdminPhone": "0400000000",
        "DirectorName": "DirectorName",
        "DirectorEmail": "director88@mailinator.com",
        "DirectorPhone": "0400000000",
        "IsStrategicMerchant": 1,
        "AnnualTurnover": 1,
        "IntendedUse": 1,
        "CompanyName": "test",
        "Category": "Cosmetic & Beauty Retailers",
        "SubCategory": "Other",
        "CompanyId": "10"
    },
    missingEmail: {
        "CountryCode": "AU",
        "name": "test2021032902",
        "DisplayName": "test2021032902",
        "ABN": "11223491505",
        "BankAccountNumber": "12345678",
        "BsbNumber": "123456",
        "BankAccountName": "test2021032902",
        "TradingName": "test trading name",
        "WebsitePlatform": "test website",
        "Address": "Sydney",
        "WebsiteUrl": "http://www.google.com",
        "SettlementEmail": "meow-2021032902@mailinator.com",
        "AdminFirstName": "fn-test2021032902",
        "AdminLastName": "ln-test2021032902",
        "AdminPhone": "0400000000",
        "DirectorName": "DirectorName",
        "DirectorEmail": "director88@mailinator.com",
        "DirectorPhone": "0400000000",
        "IsStrategicMerchant": 1,
        "AnnualTurnover": 1,
        "IntendedUse": 1,
        "CompanyName": "test",
        "Category": "Cosmetic & Beauty Retailers",
        "SubCategory": "Other",
        "CompanyId": "10"
    },
    invalidEmail: {
        "CountryCode": "AU",
        "name": "test2021032902",
        "DisplayName": "test2021032902",
        "ABN": "11223491505",
        "BankAccountNumber": "12345678",
        "BsbNumber": "123456",
        "BankAccountName": "test2021032902",
        "TradingName": "test trading name",
        "WebsitePlatform": "test website",
        "Address": "Sydney",
        "WebsiteUrl": "http://www.google.com",
        "SettlementEmail": "meow-2021032902@mailinator.com",
        "adminEmail": `ffff`,
        "AdminFirstName": "fn-test2021032902",
        "AdminLastName": "ln-test2021032902",
        "AdminPhone": "0400000000",
        "DirectorName": "DirectorName",
        "DirectorEmail": "director88@mailinator.com",
        "DirectorPhone": "0400000000",
        "IsStrategicMerchant": 1,
        "AnnualTurnover": 1,
        "IntendedUse": 1,
        "CompanyName": "test",
        "Category": "Cosmetic & Beauty Retailers",
        "SubCategory": "Other",
        "CompanyId": "10"
    },
    invalidAbn: {
        "CountryCode": "AU",
        "name": "test2021032902",
        "DisplayName": "test2021032902",
        "ABN": "test",
        "BankAccountNumber": "12345678",
        "BsbNumber": "123456",
        "BankAccountName": "test2021032902",
        "TradingName": "test trading name",
        "WebsitePlatform": "test website",
        "Address": "Sydney",
        "WebsiteUrl": "http://www.google.com",
        "SettlementEmail": "meow-2021032902@mailinator.com",
        "adminEmail": `${randStr.getRandomString(5)}@zipteam222898.testinator.com`,
        "AdminFirstName": "fn-test2021032902",
        "AdminLastName": "ln-test2021032902",
        "AdminPhone": "0400000000",
        "DirectorName": "DirectorName",
        "DirectorEmail": "director88@mailinator.com",
        "DirectorPhone": "0400000000",
        "IsStrategicMerchant": 1,
        "AnnualTurnover": 1,
        "IntendedUse": 1,
        "CompanyName": "test",
        "Category": "Cosmetic & Beauty Retailers",
        "SubCategory": "Other",
        "CompanyId": "10"
    },
    invalidIntendedUse: {
        "CountryCode": "AU",
        "name": "test2021032902",
        "DisplayName": "test2021032902",
        "ABN": "11223491505",
        "BankAccountNumber": "12345678",
        "BsbNumber": "123456",
        "BankAccountName": "test2021032902",
        "TradingName": "test trading name",
        "WebsitePlatform": "test website",
        "Address": "Sydney",
        "WebsiteUrl": "http://www.google.com",
        "SettlementEmail": "meow-2021032902@mailinator.com",
        "adminEmail": `${randStr.getRandomString(5)}@zipteam222898.testinator.com`,
        "AdminFirstName": "fn-test2021032902",
        "AdminLastName": "ln-test2021032902",
        "AdminPhone": "0400000000",
        "DirectorName": "DirectorName",
        "DirectorEmail": "director88@mailinator.com",
        "DirectorPhone": "0400000000",
        "IsStrategicMerchant": 1,
        "AnnualTurnover": 1,
        "IntendedUse": "test",
        "CompanyName": "test",
        "Category": "Cosmetic & Beauty Retailers",
        "SubCategory": "Other",
        "CompanyId": "10"
    },
}
export const responses = {
    emptyBody: {
        success: false,
        message: 'Request payload is not valid',
        errors: [
            'ABN must be 11 digits',
            'ABN must be valid',
            "'Name' should not be empty.",
            "'Trading Name' should not be empty.",
            "'Display Name' should not be empty.",
            "'Website Url' should not be empty.",
            "'Address' should not be empty.",
            "'Admin First Name' should not be empty.",
            "'Admin Last Name' should not be empty.",
            "'Director Name' should not be empty.",
            "'Admin Email' should not be empty.",
            "'Director Email' should not be empty.",
            "'Admin Phone' should not be empty.",
            "'Director Phone' should not be empty."
        ],
        inStoreApiKey: null,
        onlineApiKey: null,
        status: null,
        merchantId: null,
        "v3ApiKey":null
    },
    missingEmail: {
        "errors": ["'Admin First Name' should not be empty.", "'Admin Last Name' should not be empty.", "'Admin Email' should not be empty.", "DirectorEmail must be not empty and max 100 letters", "'Admin Phone' should not be empty."],
        "inStoreApiKey": null,
        "merchantId": null,
        "message": "Request payload is not valid",
        "onlineApiKey": null,
        "status": null,
        "success": false,
        "v3ApiKey":null
    },
    emptyEmail: {
        "errors": ["'Admin First Name' should not be empty.", "'Admin Last Name' should not be empty.", "'Admin Email' should not be empty.", "'Director Email' should not be empty.", "'Director Email' must be between 1 and 100 characters. You entered 0 characters.", "DirectorEmail must be not empty and max 100 letters", "'Admin Phone' should not be empty."],
        "inStoreApiKey": null,
        "merchantId": null,
        "message": "Request payload is not valid",
        "onlineApiKey": null,
        "status": null,
        "success": false,
        "v3ApiKey":null
    },
    invalidEmail: {
        "errors": ["'Admin First Name' should not be empty.", "'Admin Last Name' should not be empty.", "'Admin Email' should not be empty.", "DirectorEmail must be not empty and max 100 letters", "'Admin Phone' should not be empty."],
        "inStoreApiKey": null,
        "merchantId": null,
        "message": "Request payload is not valid",
        "onlineApiKey": null,
        "status": null,
        "success": false,
        "v3ApiKey":null
    },
    duplicateEmail: {
        "errors": ["'Admin First Name' should not be empty.", "'Admin Last Name' should not be empty.", "'Admin Email' should not be empty.", "'Admin Phone' should not be empty."],
        "inStoreApiKey": null,
        "merchantId": null,
        "message": "Request payload is not valid",
        "onlineApiKey": null,
        "status": null,
        "success": false,
        "v3ApiKey":null
    },
    invalidAbn: {
        "errors":[
            "ABN must be 11 digits",
            "ABN must be valid",
            "'Admin First Name' should not be empty.",
            "'Admin Last Name' should not be empty.",
            "'Admin Email' should not be empty.",
            "'Admin Phone' should not be empty."
        ],
        "inStoreApiKey":null,
        "merchantId":null,
        "message":"Request payload is not valid",
        "onlineApiKey":null,
        "status":null,
        "success":false,
        "v3ApiKey":null
    },
}