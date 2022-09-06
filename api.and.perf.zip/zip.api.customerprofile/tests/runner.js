const jest = require('jest');
const path = require('path');
process.env.NODE_TLS_REJECT_UNAUTHORIZED = 0;
process.env.environment = process.argv[2];
const options = {
    projects: [path.join(__dirname, 'api/specs')]
};
jest.runCLI(options, options.projects);