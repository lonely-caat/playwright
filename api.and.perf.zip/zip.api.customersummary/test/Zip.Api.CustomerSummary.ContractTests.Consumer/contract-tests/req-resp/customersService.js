
module.exports = {
    queries: {
        validCustomer: '{ customerProfile(id: "259e364b-5da1-46ef-af5b-109d43ee9bfa") { FirstName: givenName, LastName: familyName, Gender: gender, driverLicence { id state }, applications { applicationId }, DateOfBirth: dateOfBirth, mobilePhone { phoneNumber }, residentialAddress { suburb state postcode streetNumber streetName unitNumber countryCode } } }'
    },
    requests: {
        checkoutTokenPost: {
            consumerId: 209245,
            deviceToken: '488E528C-8BCE-42E3-A619-4478313E01F8',
        },
        checkoutTokenError: {
            consumerId: 209245,
            deviceToken: '488E528-478313E01F8',
        },
    },
    responses: {
        validateEmailValid:
            {
                "duplicatedContactsExist": false,
                "success": true
            },
        validateEmailExisting:
            {
                "consumerIdsWithMatchingContact": [],
                "duplicatedContactsExist": true,
                "success": false,
                "message": "The address max.bilichenko@zip.co is already in use."
            },

    validateMobileValid:
        {
            "duplicatedContactsExist": false,
            "success": true
        },
    validateMobileExisting:
        {
            "consumerIdsWithMatchingContact": [
                577370
            ],
            "duplicatedContactsExist": true,
            "success": false,
            "message": "The number 0423235237 is already in use."
        },
    },

};
