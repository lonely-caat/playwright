import { defineConfig, devices, expect } from '@playwright/test';
import { LogLevel } from '@slack/web-api';
import { generateCustomLayout } from './slackReport';
import * as dotenv from 'dotenv';

// Load environment-specific .env file
const envFile = process.env.ENV_FILE || '.env'; // Default to '.env'
dotenv.config({ path: envFile });

export const STORAGE_STATE = 'playwright/.auth/user.json';
export const defaultBaseURL = process.env.BASE_URL || '';

const runURL = `${process.env.GITHUB_SERVER_URL}/${process.env.GITHUB_REPOSITORY}/actions/runs/${process.env.GITHUB_RUN_ID}`;

if (!process.env.CI) {
  dotenv.config();
}

expect.extend({
  async toMatchSchema(received, schema) {
    const result = await schema.safeParseAsync(received);
    if (result.success) {
      return {
        message: () => "schema matched",
        pass: true,
      };
    } else {
      return {
        message: () =>
            "Result does not match schema: " +
            result.error.issues.map((issue) => issue.message).join("\n") +
            "\n" +
            "Details: " +
            JSON.stringify(result.error, null, 2),
        pass: false,
      };
    }
  },
});

export const connectorSecrets = JSON.parse(process.env.CONNECTORS_SECRETS || '{}');

const metaInfo = [
  { key: 'ENVIRONMENT', value: process.env.ENVIRONMENT },
  { key: 'GITHUB_REPOSITORY', value: '<https://github.com/GetVisibility/n2n/|n2n>' },
  { key: 'GITHUB_REF', value: process.env.GITHUB_REF },
  { key: 'RUN_URL', value: runURL },
];

let slackChannel = 'uat-automation-tests-testing';

if (process.env.ENVIRONMENT === 'uat') {
  slackChannel = process.env.SLACK_CHANNEL_QA || '';
} else if (process.env.ENVIRONMENT === '') {
  slackChannel = process.env.SLACK_CHANNEL_DEV || '';
}

export default defineConfig({
  testDir: './tests',
  fullyParallel: true,
  forbidOnly: !!process.env.CI,
  retries: process.env.CI ? 2 : 0,
  workers: process.env.CI ? 1 : undefined,
  reporter: process.env.CI
      ? [
        [
          './node_modules/playwright-slack-report/dist/src/SlackReporter.js',
          {
            channels: [slackChannel],
            sendResults: 'always',
            layout: (summary) => generateCustomLayout(summary, metaInfo),
            disableUnfurl: true,
            slackLogLevel: LogLevel.DEBUG,
            showInThread: true,
          },
        ],
        ['html'],
      ]
      : [['dot'], ['list'], ['html']],
  use: {
    ignoreHTTPSErrors: true,
    baseURL: defaultBaseURL,
    trace: 'on-first-retry',
    extraHTTPHeaders: {
      Accept: 'application/json',
      Authorization: `Bearer ${process.env.API_TOKEN}`,
    },
    video: 'retain-on-failure',
  },
  globalSetup: require.resolve('./global-setup'),
  projects: [
    {
      name: 'setup',
      testMatch: /.*\.setup\.ts/,
    },
    {
      name: 'chromium',
      dependencies: ['setup'],
      use: { ...devices['Desktop Chrome'], storageState: STORAGE_STATE },
    },
  ],
});
