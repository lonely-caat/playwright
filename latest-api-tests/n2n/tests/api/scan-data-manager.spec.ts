import { test, expect } from '@playwright/test';
import {filesClassificationSchema, filesSchema} from "../../logic/responses/schemas/files-schemas";

test.describe('/scan-data-manager/files', async () => {
  test('Get files count', async ({ request }) => {
    const postResponse = await request.get('/scan-data-manager/files/count');

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get files count with simple GQL query', async ({ request }) => {
    const postResponse = await request.get(
      '/scan-data-manager/files/count?gql=fileType=pdf',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get list of files', async ({ request }) => {
    const postResponse = await request.get('/scan-data-manager/files');

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('List of files data format validation', async ({ request }) => {
    const jsonSchema = filesSchema();
    const postResponse = await request.get('/scan-data-manager/files');

    // const status = await postResponse.status();
    const body = await postResponse.json();

    expect(status).toBe(200);
    // TODO: return to this when implementing NJ-114
    // body.files?.length && await expect(body.files[0]).toMatchSchema(jsonSchema);
  });

  test('List of files classification flow data format validation', async ({ request }) => {
    const jsonSchema = filesClassificationSchema();
    const postResponse = await request.get('/scan-data-manager/files?gql=flow=CLASSIFICATION');

    const status = await postResponse.status();
    const body = await postResponse.json();

    expect(status).toBe(200);
    // TODO: return to this when implementing NJ-114
    // body.files?.length && await expect(body.files[0]).toMatchSchema(jsonSchema);
  });

  test('Get list of files with simple GQL query', async ({ request }) => {
    const postResponse = await request.get('/scan-data-manager/files?gql=fileType=pdf');

    const status = await postResponse.status();
    const bodyBuffer = await postResponse.body();
    const bodyString = bodyBuffer.toString();

    expect(status).toBe(200);
    expect(bodyString).toBeDefined();
  });
});

test.describe('/scan-data-manager/files/gql/fields', async () => {
  test('Get list of files fields', async ({ request }) => {
    const postResponse = await request.get(
      '/scan-data-manager/files/gql/fields',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get list of files fields values', async ({ request }) => {
    const postResponse = await request.get(
      '/scan-data-manager/files/gql/fields/values?field=fileType',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get list of files fields values with term predicate', async ({
    request,
  }) => {
    const postResponse = await request.get(
      '/scan-data-manager/files/gql/fields/values?field=fileType&term=pdf',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test.describe('/scan-data-manager/files/extras', async () => {
    test('Get list of types', async ({ request }) => {
      const postResponse = await request.get('/scan-data-manager/files/types');

      const status = await postResponse.status();
      const body = await postResponse.body();

      expect(status).toBe(200);
      expect(body).toBeDefined();
    });

    test('Get list of sources', async ({ request }) => {
      const postResponse = await request.get(
        '/scan-data-manager/files/sources',
      );

      const status = await postResponse.status();
      const body = await postResponse.body();

      expect(status).toBe(200);
      expect(body).toBeDefined();
    });
  });
});

test.describe('/scan-data-manager/files/buckets', async () => {
  test('Get bucket by source', async ({ request }) => {
    const postResponse = await request.get(
      '/scan-data-manager/files/buckets?field=source&limit=10&count=0',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });
});

test.describe('/scan-data-manager/files/metrics', async () => {
  test('Get metrics count', async ({ request }) => {
    const postResponse = await request.get(
      '/scan-data-manager/files/metrics?field=*&function=count',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get metrics count with GQL filter', async ({ request }) => {
    const postResponse = await request.get(
      '/scan-data-manager/files/metrics?field=*&function=count&gql=lastModifiedAt="-5YEARS"',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get metrics average', async ({ request }) => {
    const postResponse = await request.get(
      '/scan-data-manager/files/metrics?field=contentLength&function=average',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get metrics min', async ({ request }) => {
    const postResponse = await request.get(
      '/scan-data-manager/files/metrics?field=contentLength&function=min',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get metrics median', async ({ request }) => {
    const postResponse = await request.get(
      '/scan-data-manager/files/metrics?field=contentLength&function=median',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });
});
