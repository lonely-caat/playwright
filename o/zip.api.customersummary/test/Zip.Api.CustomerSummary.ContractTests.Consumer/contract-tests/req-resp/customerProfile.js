
module.exports = {
    queries: {
        getGraphql: '{ customerProfile(id: "0eb30d9e-102a-413e-9646-f042ad8e5476") { FirstName: givenName, LastName: familyName, Gender: gender, driverLicence { id state }, applications { applicationId }, DateOfBirth: dateOfBirth, mobilePhone { phoneNumber }, residentialAddress { suburb state postcode streetNumber streetName unitNumber countryCode } } }',
        // getGraphql: {
        //     "query":"{ customerProfile(id: \"259e364b-5da1-46ef-af5b-109d43ee9bfa\") { FirstName: givenName, LastName: familyName, Gender: gender, driverLicence { id state }, applications { applicationId }, DateOfBirth: dateOfBirth, mobilePhone { phoneNumber }, residentialAddress { suburb state postcode streetNumber streetName unitNumber countryCode } } }"
        // },
        getGraphqlNoData: '{ customerProfile(id: "11111111-1111-1111-1111-111111111111") { FirstName: givenName, LastName: familyName, Gender: gender, driverLicence { id state }, applications { applicationId }, DateOfBirth: dateOfBirth, mobilePhone { phoneNumber }, residentialAddress { suburb state postcode streetNumber streetName unitNumber countryCode } } }',
    },
    requests: {
    },
    responses: {
        validCustomer: {
            "data":{
                "customerProfile":[
                    {
                        "FirstName":"John",
                        "LastName":"Doe",
                        "Gender":"Male",
                        "driverLicence":{
                            "id":"1111111111",
                            "state":"VIC"
                        },
                        "applications":[
                            {
                                "applicationId":"441607"
                            }
                        ],
                        "DateOfBirth":"2000-01-01",
                        "mobilePhone":{
                            "phoneNumber":"412831825"
                        },
                        "residentialAddress":{
                            "suburb":"BRIGHTON EAST",
                            "state":"VIC",
                            "postcode":"3187",
                            "streetNumber":"1",
                            "streetName":"Cheeseman Ave",
                            "unitNumber":"",
                            "countryCode":"AU"
                        }
                    }
                ]
            }
        },
        unexistingCustomer: {"data":{
            "customerProfile": []
                }
                },
    },
};
