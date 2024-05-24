import { test, expect } from '@playwright/test';

export async function retryCheck(page, actionFunction, checkFunction, targetElements) {
    const maxRetries = 100;
    const retryInterval = 10000;

    for (let attempt = 1; attempt <= maxRetries; attempt++) {
        console.log(`Attempt ${attempt} of ${maxRetries}`);

        // Performing the action
        await actionFunction(page);

        // Calling the function that returns a list
        const list = await checkFunction();

        // Verifying if all target elements are in the list
        const allElementsFound = targetElements.every(element => list.includes(element));

        if (allElementsFound) {
            console.log('Test passed: All elements found in the list.');
            return true;
        }

        // Wait for retryInterval before the next attempt
        if (attempt < maxRetries) {
            console.log(`Waiting for ${retryInterval / 1000} seconds before next attempt...`);
            await page.waitForTimeout(retryInterval);
        }
    }

    // If the loop completes without finding all elements
    return false;

}
