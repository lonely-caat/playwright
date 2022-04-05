// playwright.config.js
// @ts-check
const localConfig = require('./src/config');

const config = {
  globalSetup: require.resolve('./src/steps/global-setup'),
  testDir: `${__dirname}/src/tests`,
  timeout: 60000,
  retries: localConfig.testRetry,
  workers: localConfig.workers,
  outputDir: `${__dirname}/output`,
  use: {
    headless: true,
    viewport: { width: 1280, height: 720 },
    ignoreHTTPSErrors: true,
    screenshot: 'only-on-failure',
    storageState: 'storageState.json',
    launchOptions: {
      slowMo: 30,
    },
    baseURL: 'https://admin.sandbox.zipmoney.com.au',
  },
  reporter: [['dot'], ['json', { outputFile: './output/results.json' }]],
  reportSlowTests: { max: 0, threshold: 90000 },
};
module.exports = config;
