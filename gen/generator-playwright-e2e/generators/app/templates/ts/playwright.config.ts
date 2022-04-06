import { PlaywrightTestConfig } from '@playwright/test';
import {
  isHeadless, workers, testRetry, reporter,
} from './src/config';

const config: PlaywrightTestConfig = {
  testDir: `${__dirname}/src/tests`,
  timeout: 900000,
  retries: testRetry,
  workers,
  outputDir: `${__dirname}/output`,
  use: {
    headless: isHeadless,
    viewport: { width: 1280, height: 720 },
    ignoreHTTPSErrors: true,
    screenshot: 'only-on-failure',
    launchOptions: {
      slowMo: 20,
    },
  },
  reporter,
};
export default config;
