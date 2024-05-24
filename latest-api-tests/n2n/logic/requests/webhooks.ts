import { expect } from '@playwright/test';
import { createWebhookPayload } from '../../payloads/webhooks';

export async function deleteWebhookByName(request, webhookName: string) {
  const items = await request.get('webhook-manager/webhooks').then((res) => res.json());
  const webhookId = items.webhooks.find((item) => item.name === webhookName)?.id;
  webhookId
    ? await request.delete(`webhook-manager/webhooks/${webhookId}`)
    : console.log(`No webhook found with name: ${webhookName}`);
}

export async function createWebhookByName(request, webhookName: string) {
  const webhook = createWebhookPayload(webhookName);
  const response = await request.post('webhook-manager/webhooks', {
    data: webhook,
  });
  await expect(response).toBeOK();
}

export async function getWebhookByName(request, webhookName: string) {
  const items = await request.get('webhook-manager/webhooks').then((res) => res.json());
  return items.webhooks.find((item) => item.name === webhookName);
}
