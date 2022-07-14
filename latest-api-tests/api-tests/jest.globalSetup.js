const base = require('./utils/base-library');

module.exports = async () => {
  await base.isApiHealthy();
};
