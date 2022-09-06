const Mocha = require('mocha');
const fs = require('fs');
const path = require('path');

// Instantiate a Mocha instance.
const mochaOpts = {
    reporterOption: {
        mochaFile: './contract-tests/mochawesome-report/mochawesome.xml',
        quiet: true,
        reportDir: './contract-tests/mochawesome-report',
    },
    timeout: process.env.MOCHA_TIMEOUT,
    delay: 3000,
};

/**
 * Gets the test .spec.js file paths recursively from a given directory.
 */
function getTestPaths(dir, fileList) {
    const files = fs.readdirSync(dir);
    const pattern = '.*?(.*?.spec.js)';
    // eslint-disable-next-line
    fileList = fileList || [];
    files.forEach((file) => {
        if (fs.statSync(path.join(dir, file)).isDirectory()) {
            /* eslint-disable no-param-reassign */
            fileList = getTestPaths(path.join(dir, file), fileList);
        } else {
            fileList.push(path.join(dir, file));
        }
    });
    return fileList.filter((file) => file.match(pattern));
}

// Get all .spec.js paths and add each file to the mocha instance.
const testDir = './contract-tests/provider';
const mocha = new Mocha(mochaOpts);

getTestPaths(testDir).forEach((file) => {
    mocha.addFile(path.join(file));
});

// Run the tests.
mocha.run(async (failures) => {
    process.on('exit', () => {
        process.exit(failures); // exit with non-zero status if there were failures
    });
});
