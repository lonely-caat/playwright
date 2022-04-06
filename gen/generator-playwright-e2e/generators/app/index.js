const Generator = require('yeoman-generator');
const mkdirp = require('mkdirp');

module.exports = class extends Generator {
  async prompting() {
    this.log('*** Welcome to PlayWright e2e polierplate generator ***');
    this.answers = await this.prompt([
      {
        type: 'input',
        name: 'appName',
        message: 'Your project name',
        default: 'playwright.e2e.tests' // Default to current folder name
      },
      {
        type: 'list',
        name: 'languageSelected',
        message: 'Select Language',
        choices: ['Javascript', 'Typescript']
      }
    ]);
    this.log('-----------------------------------------------');
    this.log('\nTEST ROOT DIRECTORY  -->  ', this.answers.appName);
    this.log(
      `\nSELECTED BOILERPLATE LANGUAGE-->  ${this.answers.languageSelected}\n`
    );
    this.log('------------------------------------------------');
  }

  default() {
    mkdirp(this.answers.appName);
    this.destinationRoot(this.destinationPath(this.answers.appName));
  }

  writing() {
    if (this.answers.languageSelected === 'Typescript') {
      this._appContext('ts');
    } else {
      this._appContext('js');
    }
  }

  install() {
    this.npmInstall();
  }

  _appContext(lang) {
    this.sourceRoot(this.templatePath(`./${lang}`));
    this.fs.copy(this.templatePath('./**/*'), this.destinationRoot(), {
      globOptions: { dot: true }
    });
  }
};
