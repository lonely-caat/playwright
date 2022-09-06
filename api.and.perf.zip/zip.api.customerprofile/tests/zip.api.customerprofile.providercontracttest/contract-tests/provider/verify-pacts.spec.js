const chai = require('chai');
const {describe, it, run} = require('mocha');
const getResults = require('./api-provider');

const {expect} = chai;

(async () => {
    const results = await getResults();
    describe('Veryfing Pacts for Customer Profile Api', () => {
        results.forEach((test) => {
            it(`${test.interactionDescription} - ${test.testDescription}`, () => {
                expect(test.status).to.be.equal('passed')
            });
        });
    });
    run();
})();
