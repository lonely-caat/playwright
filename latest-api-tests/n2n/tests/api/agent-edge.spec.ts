import { test, expect } from '@playwright/test';

test.describe('/agent-edge/configuration', async () => {
  test('Get files count', async ({ request }) => {
    const postResponse = await request.get('/agent-edge/configuration');

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });
});

test.describe('/agent-edge/agent/v2/agents', async () => {
  test('Get list of agents', async ({ request }) => {
    const postResponse = await request.get('/agent-edge/agent/v2/agents');

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get list of agents with default UI filter', async ({ request }) => {
    const postResponse = await request.get(
      '/agent-edge/agent/v2/agents?sort=column=lastSeen&order=DESC&perPage=20&pageIndex=0',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });
});

test.describe('/agent-edge/audit/v2/gql/bookmarks', async () => {
  test('Get list of agent activity bookmarks', async ({ request }) => {
    const postResponse = await request.get(
      '/agent-edge/audit/v2/gql/bookmarks',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });
});

test.describe('/agent-edge/agent/v2/gql/bookmarks', async () => {
  test('Get list of agent management bookmarks', async ({ request }) => {
    const postResponse = await request.get(
      '/agent-edge/agent/v2/gql/bookmarks',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });
});

test.describe('/agent-edge/audit/v2/metrics', async () => {
  test('Get metrics count', async ({ request }) => {
    const postResponse = await request.get(
      '/agent-edge/audit/v2/metrics?field=*&function=count',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get metrics count with GQL filter', async ({ request }) => {
    const postResponse = await request.get(
      '/agent-edge/audit/v2/metrics?field=*&function=count&gql=lastModificationTime="-5YEARS"',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get metrics sum', async ({ request }) => {
    const postResponse = await request.get(
      '/agent-edge/audit/v2/metrics?field=contentLength&function=sum',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get metrics average', async ({ request }) => {
    const postResponse = await request.get(
      '/agent-edge/audit/v2/metrics?field=contentLength&function=average',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get metrics min', async ({ request }) => {
    const postResponse = await request.get(
      '/agent-edge/audit/v2/metrics?field=contentLength&function=min',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get metrics median', async ({ request }) => {
    const postResponse = await request.get(
      '/agent-edge/audit/v2/metrics?field=contentLength&function=median',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });
});

test.describe('/agent-edge/agent/v2/metrics', async () => {
  test('Get metrics count', async ({ request }) => {
    const postResponse = await request.get(
      '/agent-edge/agent/v2/metrics?field=*&function=count',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get metrics count with GQL filter', async ({ request }) => {
    const postResponse = await request.get(
      '/agent-edge/agent/v2/metrics?field=*&function=count&gql=lastSeen="-5YEARS"',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get metrics average', async ({ request }) => {
    const postResponse = await request.get(
      '/agent-edge/agent/v2/metrics?field=lastSeen&function=average',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get metrics min', async ({ request }) => {
    const postResponse = await request.get(
      '/agent-edge/agent/v2/metrics?field=lastSeen&function=min',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });

  test('Get metrics median', async ({ request }) => {
    const postResponse = await request.get(
      '/agent-edge/agent/v2/metrics?field=lastSeen&function=median',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });
});

test.describe('/agent-edge/audit/v2/buckets', async () => {
  test('Get bucket by operation type', async ({ request }) => {
    const postResponse = await request.get(
      '/agent-edge/audit/v2/buckets?field=operation&limit=10&count=0',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });
});

test.describe('/agent-edge/agent/v2/buckets', async () => {
  test('Get bucket by hostName', async ({ request }) => {
    const postResponse = await request.get(
      '/agent-edge/agent/v2/buckets?field=hostName&limit=10&count=0',
    );

    const status = await postResponse.status();
    const body = await postResponse.body();

    expect(status).toBe(200);
    expect(body).toBeDefined();
  });
});
