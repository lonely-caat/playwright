# n2n
### Installation
```
yarn install
```

### Running locally

```
npx playwright test --workers=3 --retries=3 --max-failures=35
```

The most convenient way to run is to run Playwright tests within VS Code. Follow the official guide provided by Playwright for detailed instructions: [Run tests in VS Code.](https://playwright.dev/docs/getting-started-vscode)
This guide will walk you through the steps required to configure your VS Code environment for running and debugging Playwright tests.


### .env

To run locally this project requires .env file to be created in the root folder.
The connector secrets can be found in 1Pass named "[All connectors authentication](https://start.1password.com/open/i?a=SCAJ6FBZD5FLBNDRIDANPCI3AU&v=3xgqhesynkxthue2joyxtgxuvy&i=offxx7nit7hbopqcck2dj3jfzy&h=getvisibility.1password.com)"
The contents should look like this:

```
CONNECTORS_SECRETS='SECRETS_FROM_1PASS'
```
