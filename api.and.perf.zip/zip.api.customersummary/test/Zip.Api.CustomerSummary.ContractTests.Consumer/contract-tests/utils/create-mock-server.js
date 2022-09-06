const path = require('path');
const { Pact } = require('@pact-foundation/pact');
require('dotenv-safe').config();

let mockServer;
async function finalizeMockServer() {
  if (mockServer) {
    await mockServer.finalize();
    mockServer = undefined;
  }
}

async function createMockServer(consumerName, providerName) {
  if (mockServer && mockServer.opts.consumer !== consumerName) {
    await finalizeMockServer();
  }
  if (!mockServer) {
    mockServer = new Pact({
      consumer: consumerName,
      provider: providerName,
      port: parseInt(process.env.MOCK_SERVER_PORT, 10),
      log: path.resolve(__dirname, '../logs', 'expectationLog.log'),
      logLevel: 'ERROR',
      dir: path.resolve(__dirname, '../pacts'),
      spec: 2,
    });
    await mockServer.setup();
  }
  return mockServer;
}
module.exports = { createMockServer, finalizeMockServer };
