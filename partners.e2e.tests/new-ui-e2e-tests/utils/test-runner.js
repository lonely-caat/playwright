import createTestCafe from 'testcafe';

let testcafe = null;
function getBrowsers() {
    let browserList;
    let supported_browsers = ['chrome', 'firefox']
    if (process.argv.length <= 2) {
        browserList = ['chrome']}
    else if (process.argv.length === 3) {
        let browsers = process.argv[2].filter(element => supported_browsers.includes(element))
        if (!browsers.length){
            browserList = browsers}
        else {throw new Error(`Unsupported browser, supported browsers are: ${supported_browsers.toString()}` )}
        }
    else {
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
