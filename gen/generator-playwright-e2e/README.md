# generator-playwright-e2e 
> **generator for MS Playwright boilerplate for e2e tests**

### Installation

- Install [Yeoman](http://yeoman.io) globally
```bash
npm install -g yo
```
- Install **private package** @zip/generator-playwright-e2e globally (This is hosted in internal [Zip Artifactory registry](https://zipau.jfrog.io/artifactory/api/npm/npm-local/)), (*you might need to npm login into zip artifactory registry) 

```bash
npm install -g @zip/generator-playwright-e2e
```

### Generate boilerplate
generate your new playwright project at either in independent repo or within your existing team project:

```bash
cd to/parent/directory
yo @zip/playwright-e2e
```
#### Options during installation

- **Name of the project** - default is `playwright.e2e.tests`, however you can input any name. A directory is created with the same name and boilerplate code will be generated inside that
- **TypeScript/JavaScript** - This generator gives the flexibity to create the boilerplate in typescript or javascript. 
- **Good to go** - The dependencies for the new project are already installed and you can run `test:ui:sandbox` to run 2 sample tests! 

### Project brief
This sample project comes with **2 test specs, page objects, fixtures, data, util, helpers and CI Pipeline** etc. which should be good guide and  foundation to understand the framework and add your own test scripts. Happy Coding :-) 
