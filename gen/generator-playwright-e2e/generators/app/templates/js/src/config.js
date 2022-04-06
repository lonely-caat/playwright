const { TEST_ENV, TIMEOUT, HEADLESS, TEST_CI, TEST_RETRY } = process.env;

const isCI = TEST_CI === 'true';

exports.isSandbox = TEST_ENV === 'SANDBOX';
exports.isDev = TEST_ENV === 'DEV';
exports.isStaging = TEST_ENV === 'STAGING';
exports.TEST_TIMEOUT = typeof TIMEOUT === 'string' ? parseInt(TIMEOUT, 10) : 60000;
exports.isHeadless = HEADLESS === 'true';
exports.workers = isCI ? 50 : 5;
exports.testRetry = TEST_RETRY ? parseInt(TEST_RETRY, 10) : 1;
exports.reporter = isCI ? 'dot' : 'line';
