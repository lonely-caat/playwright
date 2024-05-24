const { request, expect } = require('@playwright/test');

module.exports = async () => {
    const username = process.env.USERNAME || "gv";
    const password = process.env.PASSWORD || "gv";
    const baseUrl = process.env.BASE_URL || "https://enterpriseqa.gvdevelopment.k3s.getvisibility.com";

    const createRequestContext = await request.newContext();
    const formData = new URLSearchParams({
        username: username,
        password: password,
        client_id: 'dashboard',
        grant_type: 'password'
    });

    try {
        const tokenResponse = await createRequestContext.post(`${baseUrl}/auth/realms/gv/protocol/openid-connect/token`, {
            ignoreHTTPSErrors: true,
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            ignoreHTTPSErrors: true,
            data: formData.toString()
        });

        expect(tokenResponse.status()).toBe(200);

        const responseBody = await tokenResponse.json();
        const token = responseBody.access_token;

        expect(token).toBeDefined();

        process.env.API_TOKEN = token;
        console.log('JWT token successfully loaded');
    } catch (error) {
        console.error('Error fetching API token:', error);
        throw error;
    }
};
