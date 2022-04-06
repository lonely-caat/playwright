// @ts-check
const localConfig = require('./src/config');

const config = {
  testDir: `${__dirname}/src/tests`,
  timeout: 900000,
  retries: localConfig.testRetry,
  workers: localConfig.workers,
  outputDir: `${__dirname}/output`,
  use: {
    headless: localConfig.isHeadless,
    slowMo: 20,
    viewport: { width: 1280, height: 720 },
    ignoreHTTPSErrors: true,
    screenshot: 'only-on-failure',
  },
  reporter: localConfig.reporter,
};
exports.default = config;
