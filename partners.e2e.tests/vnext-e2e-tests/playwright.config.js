// playwright.config.js
// @ts-check
const localConfig = require('./src/config');
const urls = require('./src/constants/urls')

const config = {
  globalSetup: require.resolve('./src/steps/global-setup'),
  testDir: `${__dirname}/src/tests`,
  timeout: 60000,
  retries: 0,
  workers: localConfig.workers,
  outputDir: `${__dirname}/output`,
  use: {
    headless: false,
    viewport: { width: 1280, height: 720 },
    ignoreHTTPSErrors: true,
    screenshot: 'only-on-failure',
    storageState: 'storageState.json',
    launchOptions: {
      slowMo: 30,
    },
    baseURL: urls.hostName,
  },
  reporter: [['dot'], ['json', { outputFile: './output/results.json' }]],
  reportSlowTests: { max: 0, threshold: 90000 },
};
module.exports = config;
