const frisby = require('frisby'),
    Joi = require('@hapi/joi'),
    DataExport = require('../../common/lib/dataExport'),
    cpData = DataExport.customerProfileData,
    request = require('../../common/request');
let endPoint = `https://api-customerprofile.internal.${process.env.environment}.au.edge.zip.co/graphql`;

beforeAll(() => {

    frisby.addExpectHandler('profileData', (response) => {
        let num_of_records = response.json.data.customerProfile.length;
        let json = response.json.data.customerProfile[0];
        expect(num_of_records).toEqual(1);
        expect(json.id).toBe(cpData.customerId);
        expect(json.email).toBe(cpData.email);
    });

});

describe('Customer profile tests==> ', () => {

    it('Response status should be 200', async () => {
        await frisby
            .post(endPoint, {
                query: request(cpData.email)
            })
            .expect('status', 200);
    })

    it('Should match schema', async () => {

        let graphqlResp = await frisby
            .post(endPoint, {
                query: request(cpData.email)
            });
        let resp = JSON.parse(graphqlResp._body).data.customerProfile[0];
        let schema = Joi.object({
            id: Joi.string().guid(),
            email: Joi.string().email()
        });
        const {error} = schema.validate(resp);
        if (error) {
            console.log(error);
            throw 'Schema validation failed'
        }
    })

    it('Should successfully validate the CustomerProfile data', async () => {

        await frisby
            .post(endPoint, {
                query: request(cpData.email)
            })
            .expect('profileData');
    })

})
