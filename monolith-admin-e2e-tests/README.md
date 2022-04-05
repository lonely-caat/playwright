# Playwright UI Testing

Best results on [Node.js](https://nodejs.org/) v14+ to run.

### Install
```sh
npm i
// if receiving self-signed certificate errors due to Netskope npm i, disable Netskope and try again

```
### Environment variables
tests are written to run on sand only

### Run
```
npm run test:ui:sandbox
```
### Debugging
Debug single UI test by adding word **debug** to test title and run
```
npm run test:ui:debug
```
Set breakpoints enabling step through debugging via `await page.pause()`
More on [Playwright debugging](https://playwright.dev/docs/debug)
