import { expect } from '@playwright/test';


export async function getSource(request): Promise<string> {
    const sourceRequest = await request.get('/scan-data-manager/files/sources');
    const responseBody = await sourceRequest.json();
    expect(responseBody.length, 'Source response body is empty').toBeGreaterThan(0);
    return responseBody[0];
}

export async function getExtensions(request): Promise<string[]> {
    const sourceRequest = await request.get('scan-data-manager/files/types');
    const responseBody = await sourceRequest.json();
    expect(responseBody.length, 'Source response body is empty').toBeGreaterThan(0);
    return responseBody;
}
