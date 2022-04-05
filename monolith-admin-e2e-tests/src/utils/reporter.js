const supertest = require('supertest');
const results = require('../../output/results.json');

const body = {
  blocks: [
    {
      type: 'divider',
    },
    {
      type: 'context',
      elements: [
        {
          type: 'mrkdwn',
          text: `:point_right: *Zip critical user journeys finished running on: ${process.env.TEST_ENV}!* :muscle:\n\n*Pipeline URL*: ${process.env.JOB_URL}\n\n`,
        },
      ],
    },
    {
      type: 'section',
      text: {
        type: 'mrkdwn',
        text: '*Here are the Results:* :point_down:',
      },
    },
    {
      type: 'divider',
    },
  ],
};

results.suites.forEach((file) => {
  file.suites.forEach((describe) => {
    describe.specs.forEach((spec) => {
      const { title } = spec;
      const timeTook = spec.tests[0].results[0].duration / 1000;
      if (timeTook < 1) {
        body.blocks.push({
          type: 'section',
          text: {
            type: 'mrkdwn',
            text: `:face_with_monocle: *"${title}"* is skipped`,
          },
        });
      } else if (spec.ok) {
        body.blocks.push({
          type: 'section',
          text: {
            type: 'mrkdwn',
            text: `:tada: *"${title}"* passed!`,
          },
        });
      } else {
        body.blocks.push({
          type: 'section',
          text: {
            type: 'mrkdwn',
            text: `:x: *"${title}"* failed!`,
          },
        });
      }
    });
  });
});
body.blocks.push({ type: 'divider' });

const account = async () =>
  supertest('https://hooks.slack.com')
    .post('/services/T06KXCZNC/B02PS0BMDRP/dECfv437HWcZMFaXuWBzXxuo')
    .send(body);

account();
