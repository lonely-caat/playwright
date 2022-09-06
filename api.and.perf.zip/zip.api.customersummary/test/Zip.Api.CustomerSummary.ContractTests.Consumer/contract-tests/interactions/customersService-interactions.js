const { somethingLike: like } = require('@pact-foundation/pact').Matchers;
const customersServiceData = require('../req-resp/customersService');

module.exports = {
    putEmailValidate: {
        state: 'it has the ability to validate email by customerid',
        uponReceiving: 'Put request to return customers profile data based on validation',
        withRequest: {
            method: 'PUT',
            path: '/customer-api/v1/customers/1/email/validate',
            body: {'EmailAddress':'basTest@mailiantor.com'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(customersServiceData.responses.validateEmailValid),
        },
    },
    putEmailValidateExistingEmail: {
        state: 'it has the ability to validate email by customerid for existing client',
        uponReceiving: 'Put request to return customers profile data based on validation for existing client',
        withRequest: {
            method: 'PUT',
            path: '/customer-api/v1/customers/1/email/validate',
            body: {'EmailAddress':'max.bilichenko@zip.co'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(customersServiceData.responses.validateEmailExisting),
        },
    },
    putEmailValidateInvalidEmail: {
        state: 'it has the ability to validate invalid email by customerid',
        uponReceiving: 'Put request to return customers profile data based on validation with invalid email supplied',
        withRequest: {
            method: 'PUT',
            path: '/customer-api/v1/customers/1/email/validate',
            body: {'EmailAddress':''},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(customersServiceData.responses.validateEmailValid),
        },
    },
    putEmailValidateInvalidCustomerid: {
        state: 'it has the ability to validate customerid',
        uponReceiving: 'Put email validate request to return data based on validation with invalid customerid supplied',
        withRequest: {
            method: 'PUT',
            path: '/customer-api/v1/customers/10000000000000000000000000/email/validate',
            body: {'EmailAddress':'basTest@mailiantor.com'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(customersServiceData.responses.validateEmailValid),
        },
    },


    putEmailUpdate: {
        state: 'it has the ability to update email by customerid',
        uponReceiving: 'Put request to update email',
        withRequest: {
            method: 'PUT',
            path: '/customer-api/v1/customers/1/email',
            body: {'EmailAddress':'basTest2@mailiantor.com'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(customersServiceData.responses.validateEmailValid),
        },
    },
    putEmailUpdateExistingEmail: {
        state: 'it has the ability to update existing email by customerid',
        uponReceiving: 'duplicate email error',
        withRequest: {
            method: 'PUT',
            path: '/customer-api/v1/customers/1/email',
            body: {'EmailAddress':'max.bilichenko@zip.co'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(customersServiceData.responses.validateEmailValid),
        },
    },
    putEmailUpdateInvalidEmail: {
        state: 'it has the ability to not return error for invalid email',
        uponReceiving: 'invalid email error',
        withRequest: {
            method: 'PUT',
            path: '/customer-api/v1/customers/1/email',
            body: {'EmailAddress':''},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(customersServiceData.responses.validateEmailValid),
        },
    },
    putEmailUpdateInvalidCustomerid: {
        state: 'it has the ability to not return error for invalid customerid',
        uponReceiving: 'invalid customerid error',
        withRequest: {
            method: 'PUT',
            path: '/customer-api/v1/customers/10000000000000000000000000/email',
            body: {'EmailAddress':'basTest2@mailinator.com'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(customersServiceData.responses.validateEmailValid),
        },
    },

    putMobileValidate: {
        state: 'it has the ability to validate Mobile by customerid',
        uponReceiving: 'Put request to return customers profile data based on validation',
        withRequest: {
            method: 'PUT',
            path: '/customer-api/v1/customers/1/mobile/validate',
            body: {'PhoneNumber':'0400000000'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(customersServiceData.responses.validateMobileValid),
        },
    },
    putMobileValidateExistingMobile: {
        state: 'it has the ability to validate Mobile by customerid for existing client',
        uponReceiving: 'Put request to return customers profile data based on validation for existing client',
        withRequest: {
            method: 'PUT',
            path: '/customer-api/v1/customers/1/mobile/validate',
            body: {'PhoneNumber':'0423235237'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(customersServiceData.responses.validateMobileExisting),
        },
    },
    putMobileValidateInvalidMobile: {
        state: 'it has the ability to validate invalid Mobile by customerid',
        uponReceiving: 'Put request to return customers profile data based on validation with invalid Mobile supplied',
        withRequest: {
            method: 'PUT',
            path: '/customer-api/v1/customers/1/mobile/validate',
            body: {'PhoneNumber':''},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(customersServiceData.responses.validateMobileValid),
        },
    },
    putMobileValidateInvalidCustomerid: {
        state: 'it has the ability to validate invalid customerid for mobile validate',
        uponReceiving: 'Put validate mobile request to return data based on validation with invalid customerid supplied',
        withRequest: {
            method: 'PUT',
            path: '/customer-api/v1/customers/10000000000000000000000000/mobile/validate',
            body: {'PhoneNumber':'0400000000'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(customersServiceData.responses.validateMobileValid),
        },
    },


    putMobileUpdate: {
        state: 'it has the ability to update Mobile by customerid',
        uponReceiving: 'Put request to update Mobile',
        withRequest: {
            method: 'PUT',
            path: '/customer-api/v1/customers/1/mobile',
            body: {'PhoneNumber':'0400000000'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(customersServiceData.responses.validateMobileValid),
        },
    },
    putMobileUpdateExistingMobile: {
        state: 'it has the ability to update existing Mobile by customerid',
        uponReceiving: 'duplicate Mobile error',
        withRequest: {
            method: 'PUT',
            path: '/customer-api/v1/customers/1/mobile',
            body: {'PhoneNumber':'0423235237'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(customersServiceData.responses.validateMobileValid),
        },
    },
    putMobileUpdateInvalidMobile: {
        state: 'it has the ability to not return error for invalid phone number',
        uponReceiving: 'invalid Mobile error',
        withRequest: {
            method: 'PUT',
            path: '/customer-api/v1/customers/1/mobile',
            body: {'PhoneNumber':''},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(customersServiceData.responses.validateMobileValid),
        },
    },
    putMobileUpdateInvalidCustomerid: {
        state: 'it has the ability not to return error for invalid customer id',
        uponReceiving: 'invalid customerid error',
        withRequest: {
            method: 'PUT',
            path: '/customer-api/v1/customers/10000000000000000000000000/mobile',
            body: {'PhoneNumber':'0400000000'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(customersServiceData.responses.validateMobileValid),
        },
    },

};
