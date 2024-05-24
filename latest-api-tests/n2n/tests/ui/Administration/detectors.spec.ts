import { test, expect } from '@playwright/test';
import { DetectorsPage } from '../../../logic/pages/detectors.page';
import { v4 as uuidv4 } from 'uuid';
import { detector } from '../../../payloads/detectors';
import { faker } from "@faker-js/faker";

test.describe('Detectors page', async () => {
  let detectorsPage: DetectorsPage;
  let detectorName: string;

  test.describe('Create detectors', async () => {
    test.beforeEach(async ({ page }) => {
      detectorsPage = new DetectorsPage(page);
      detectorName = uuidv4();
      await detectorsPage.navigateTo('administration/detectors');
    });

    test.afterEach(async ({ request }) => {
      const postResponse = await request.delete(`regex-api/detectors/queries/${detectorName}`);
      await expect(postResponse).toBeOK();
    });

    test('Create keywords detector: contains  @smoke @ui', async ({ page }) => {
      await detectorsPage.createDetector(detectorName, true, true, 'meow...');
      await expect(page.getByText(/error/i)).toHaveCount(0);
    });

    test('Create keywords detector: not contains  @smoke @ui', async ({ page }) => {
      await detectorsPage.createDetector(detectorName, true, true, undefined, 'Cats');
      await expect(page.getByText(/error/i)).toHaveCount(0);
    });

    test('Create keywords detector: disabled, case insensitive  @smoke @ui', async ({ page }) => {
      await detectorsPage.createDetector(detectorName, true, false, undefined, 'Test');
      await expect(page.getByText(/error/i)).toHaveCount(0);
    });

    test('Create Regex detector  @smoke @ui', async ({ page }) => {
      await detectorsPage.createRegexDetector(
        detectorName,
        true,
        '^((?=\\S*?[A-Z])(?=\\S*?[a-z])(?=\\S*?[0-9]).{6,})\\S$\n',
      );
      await expect(page.getByText(/error/i)).toHaveCount(0);
    });

    test('Create Regex detector disabled  @smoke @ui', async ({ page }) => {
      await detectorsPage.createRegexDetector(
        detectorName,
        false,
        '^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$',
      );
      await expect(page.getByText(/error/i)).toHaveCount(0);
    });
  });
  test.describe('Delete detectors', async () => {
    test.beforeEach(async ({ page, request }) => {
      detectorsPage = new DetectorsPage(page);
      detectorName = uuidv4();
      const payload = detector(detectorName, '[a-z][A-Z]', 'REGEX', true, true);
      const postResponse = await request.post(`regex-api/detectors/queries`, { data: payload });
      await expect(postResponse).toBeOK();
      await detectorsPage.navigateTo('administration/detectors');
    });
    test('Delete detector  @smoke @ui', async ({ page }) => {
      await detectorsPage.deleteDetectorByName(detectorName);
      await expect(page.getByText(/error/i)).toHaveCount(0);
    });
  });
  test.describe('Edit detectors', async () => {
    test.beforeEach(async ({ page, request }) => {
      detectorsPage = new DetectorsPage(page);
      detectorName = uuidv4();
      const payload = detector(detectorName, "'test'", 'MATCH', false, true);
      const postResponse = await request.post(`regex-api/detectors/queries`, { data: payload });
      await expect(postResponse).toBeOK();
      await detectorsPage.navigateTo('administration/detectors');
    });
    test.afterEach(async ({ request }) => {
      const postResponse = await request.delete(`regex-api/detectors/queries/${detectorName}`);
      await expect(postResponse).toBeOK();
    });

    test('Edit detector keywords  @smoke @ui', async ({ page }) => {
      await detectorsPage.editDetectorByName(detectorName, 'includeMe', 'notIncluded');
      await expect(page.getByText(/error/i)).toHaveCount(0);
    });

    test('Disable detector  @smoke @ui', async ({ page }) => {
      await detectorsPage.disableDetectorByName(detectorName);
      await expect(page.getByText(/error/i)).toHaveCount(0);
    });

    test('Change detector status @smoke @ui', async ({ page }) => {
      await detectorsPage.changeDetectorTumbler(detectorName);
      await expect(page.getByText(/error/i)).toHaveCount(0);
    });
  });
  test.describe('Test detectors', async () => {
    test.beforeEach(async ({ page, request }) => {
      detectorsPage = new DetectorsPage(page);
      detectorName = uuidv4();
    });

    test('Test keyword detector contains @smoke @ui', async ({ page, request }) => {
      const containsQuery = faker.word.sample();
      const payload = detector(detectorName, `"${containsQuery}"`, 'MATCH', false, true);
      const postResponse = await request.post(`regex-api/detectors/queries`, { data: payload });
      await expect(postResponse).toBeOK();
      await detectorsPage.navigateTo('administration/detectors');
      await detectorsPage.testDetector(containsQuery, detectorName);
      await request.delete(`regex-api/detectors/queries/${detectorName}`);
    });

    test('Test keyword detector not contains @smoke @ui', async ({ page, request }) => {
      const containsQuery = 'NOT ("AAAAAAAAAAAAAAAAAA")'
      const payload = detector(detectorName, `"${containsQuery}"`, 'MATCH', false, true);
      const postResponse = await request.post(`regex-api/detectors/queries`, { data: payload });
      await expect(postResponse).toBeOK();
      await detectorsPage.navigateTo('administration/detectors');
      await detectorsPage.testDetector(containsQuery, detectorName);
      await request.delete(`regex-api/detectors/queries/${detectorName}`);
    });

    test('Test regex detector @smoke @ui', async ({ page, request }) => {
      const containsQuery = faker.internet.email()
      const payload = detector(detectorName, `.+@.+\\..+`, 'REGEX', undefined, true);
      const postResponse = await request.post(`regex-api/detectors/queries`, { data: payload });
      await expect(postResponse).toBeOK();
      await detectorsPage.navigateTo('administration/detectors');
      await detectorsPage.testDetector(containsQuery, detectorName);
      await request.delete(`regex-api/detectors/queries/${detectorName}`);
    });
  });
});
