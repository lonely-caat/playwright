
module.exports = {
    responses: {
        getUser: {
            "id": "9a9d137c-3d15-4780-b2bb-b0c0219220dc",
            "userName": "max.bilichenko@zip.co",
            "email": "max.bilichenko@zip.co",
            "isActive": true,
            "roles": [
                {
                    "id": "98cf96f8-f89a-4ca8-9cbe-ffbc44053393",
                    "name": "Admin"
                }
            ]
        },
        getUnexistingUser: {
            "message": "Unable to find User by email [bastest@zip.co]"
        },
        getEmptyUser: 'Email [] is NullOrEmpty'
    },
};
