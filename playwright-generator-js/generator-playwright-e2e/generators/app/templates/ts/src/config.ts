const {
  TEST_ENV, TIMEOUT, HEADLESS, TEST_CI, TEST_RETRY,
} = process.env;

export const isSandbox = TEST_ENV === 'SANDBOX';
export const isDev = TEST_ENV === 'DEV';
export const isStaging = TEST_ENV === 'STAGING';
export const TEST_TIMEOUT = typeof TIMEOUT === 'string' ? parseInt(TIMEOUT, 10) : 60000;
export const isHeadless = HEADLESS === 'true';
export const isCI = TEST_CI === 'true';
export const enableScreenshots = isCI ? 'off' : 'only-on-failure';
export const workers = isCI ? 50 : 5;
export const testRetry = TEST_RETRY ? parseInt(TEST_RETRY, 10) : 1;
export const reporter = isCI ? 'dot' : 'line';
