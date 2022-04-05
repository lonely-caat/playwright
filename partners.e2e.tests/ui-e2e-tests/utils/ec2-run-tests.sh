rm -rf ./test-project/*
cd ./test-project || exit
git clone http://coreE2eToken:Sdr69_fGfxQT3TgSwxw-@gitlab.com/zip-au/qa/partners.e2e.tests.git
cd partners.e2e.tests/ui-e2e-tests || exit
npm install
