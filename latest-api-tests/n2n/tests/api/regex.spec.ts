import { test, expect } from '@playwright/test';
import { regexSchema } from "../../logic/responses/schemas/regex-schemas";


test.describe('/regex-api/expression', async () => {
  test('Get list of regexes', async ({ request }) => {
    const postResponse = await request.get('/regex-api/expression');

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('List of regexes data format validation', async ({ request }) => {
    const jsonSchema = regexSchema();
    const postResponse = await request.get('/regex-api/expression');

    const status = await postResponse.status();
    const body = await postResponse.json();

    expect(status).toBe(200);
    expect(body).toBeDefined();
    body.files?.length && await expect(body.files[0]).toMatchSchema(jsonSchema);
  });

  test('Get list of regexes with filter by single tag', async ({ request }) => {
    const postResponse = await request.get(
      '/regex-api/expression?tags=PII&perPage=20&pageIndex=0',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get list of regexes with filter by multiple tags', async ({
    request,
  }) => {
    const postResponse = await request.get(
      '/regex-api/expression?tags=PII&tags=US&perPage=20&pageIndex=0',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get list of regexes with filter by subcategory', async ({
    request,
  }) => {
    const postResponse = await request.get(
      '/regex-api/expression?subcategory=CV&perPage=20&pageIndex=0',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get list of regexes with filter by category', async ({ request }) => {
    const postResponse = await request.get(
      '/regex-api/expression?category=HR_Documents&perPage=20&pageIndex=0',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get list of regexes with filter by expression name', async ({
    request,
  }) => {
    const postResponse = await request.get(
      '/regex-api/expression?expressionName=ABA&perPage=20&pageIndex=0',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get list of regex tags', async ({ request }) => {
    const postResponse = await request.get('/regex-api/expression/tags');

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });
});
