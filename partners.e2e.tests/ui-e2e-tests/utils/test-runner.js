import createTestCafe from 'testcafe';

let testcafe = null;
function getBrowsers() {
    let browserList;
    if (process.argv.length <= 2) {
        browserList = ['chrome'];
    } else {
        browserList = process.argv.slice(2);
    }
  console.log('Browsers to run tests on > ', browserList);
  return browserList;
}
createTestCafe('localhost')
  .then((tc) => {
    testcafe = tc;
    const runner = testcafe.createRunner();
    return runner.browsers(getBrowsers()).run({
      selectorTimeout: Number(process.env.SELECTOR_TIMEOUT),
      pageLoadTimeout: Number(process.env.PAGE_LOAD_TIMEOUT),
      assertionTimeout: Number(process.env.ASSERTION_TIMEOUT),
    });
  })
  .then((failedCount) => {
    console.log(`Tests failed: ${failedCount}`);
    testcafe.close();
    process.exit(failedCount ? 1 : 0);
  });
