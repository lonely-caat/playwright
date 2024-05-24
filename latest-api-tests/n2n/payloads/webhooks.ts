import { v4 as uuidv4 } from 'uuid';

export function createWebhookPayload(webhookName: string){
    return {
        "id": uuidv4(),
        "status": null,
        "createdAt": Date.now() / 1000,
        "queryStr": null,
        "updatedAt": Date.now() / 1000 + 1,
        "name": webhookName,
        "gql": "flow=CLASSIFICATION",
        "url": "https://google.com",
        "active": true,
        "dataset": "files"
    }
}
