import { Block, KnownBlock } from '@slack/types';
import { SummaryResults } from 'playwright-slack-report/dist/src';

export function generateCustomLayout(
    summaryResults: SummaryResults,
    meta: Array<{ key: string; value: string }>,
): Array<KnownBlock | Block> {
  const { tests } = summaryResults;

  const header: KnownBlock = {
    type: 'header',
    text: {
      type: 'plain_text',
      text: '🎭 Playwright Results',
      emoji: true,
    },
  };

  const summary: KnownBlock = {
    type: 'section',
    text: {
      type: 'mrkdwn',
      text: `✅ Passed: ${summaryResults.passed} | ❌ Failed: ${summaryResults.failed} | ⏩ Skipped: ${summaryResults.skipped}`,
    },
  };

  const failedTestNames = new Set();
  tests.forEach(test => {
    const testName = `${test.suiteName}:${test.name}`;
    if (test.status === 'failed') {
      failedTestNames.add(testName);
    } else if (test.status === 'passed') {
      failedTestNames.delete(testName);
    }
  });

  const failedTests = Array.from(failedTestNames).map(testName => ({
    type: 'section',
    text: {
      type: 'mrkdwn',
      text: `❌ ${testName}`,
    },
  }));

  const metaSections = meta.map(item => ({
    type: 'section',
    text: {
      type: 'mrkdwn',
      text: `*${item.key}:* ${item.value}`,
    },
  }));

  let headerText = 'No tests failed. :cat-roomba:';
  if (failedTestNames.size > 0) {
    headerText = '😿 *Tests failed:*';
    if (failedTestNames.size >= 5) {
      headerText = '😿 *Too many tests failed, aborting run. Tests failed::*';
    }
  }

  const failedTestsHeader: KnownBlock = {
    type: 'section',
    text: {
      type: 'mrkdwn',
      text: headerText,
    },
  };

  return [header, summary, ...metaSections, failedTestsHeader, ...failedTests];
}
