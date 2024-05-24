import { test, expect } from '@playwright/test';
import { classificationSchema } from "../../logic/responses/schemas/classification-schemas";

test.describe('/classification-tags/tags', async () => {
  test('Get list of classification tags', async ({ request }) => {
    const postResponse = await request.get('/classification-tags/tags');

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('List of classification tags data format validation', async ({ request }) => {
    const jsonSchema = classificationSchema;
    const postResponse = await request.get('/classification-tags/tags');

    const status = await postResponse.status();
    const body = await postResponse.json();

    expect(status).toBe(200);
    expect(body).toBeDefined();
    body.files?.length && await expect(body).toMatchSchema(jsonSchema);
  });

  test('Get list of classification categories', async ({ request }) => {
    const postResponse = await request.get('/classification-tags/categories');

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });
});
