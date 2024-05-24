import { test, expect } from '@playwright/test';
import {webhookSchema} from "../../logic/responses/schemas/webhook-schemas";

test.describe('/webhook-manager/webhooks', async () => {
  test('Get webhooks list', async ({ request }) => {
    const postResponse = await request.get('/webhook-manager/webhooks');

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('List of webhooks data format validation', async ({ request }) => {
    const jsonSchema = webhookSchema;
    const postResponse = await request.get('/webhook-manager/webhooks');

    const status = await postResponse.status();
    const body = await postResponse.json();

    expect(status).toBe(200);
    expect(body).toBeDefined();
    body.files?.length && await expect(body).toMatchSchema(jsonSchema);
  });
});
