import { test, expect } from '@playwright/test';

test.describe('/analytics-hub/reports', async () => {
  test('Get list of analytics dashboards', async ({ request }) => {
    const postResponse = await request.get('/analytics-hub/reports');

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });
});
