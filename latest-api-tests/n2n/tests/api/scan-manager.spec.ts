import { test, expect } from '@playwright/test';

test.describe('/scan-manager/proxy', async () => {
  test('Get proxy settings', async ({ request }) => {
    const postResponse = await request.get('/scan-manager/proxy');

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });
});
