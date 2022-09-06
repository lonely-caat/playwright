import { appUrl } from '../../config'
import Header from '../page-objects/components/Header'
import LoginPage from '../page-objects/pages/LoginPage'
import Helpers from '../support/helpers'


describe('Positive login tests', () => {
  beforeEach(() => {
    cy.visit(appUrl)
    Header.signIn()
    cy.wrap(Helpers.generateValidLogin()).as('validLogin')
    cy.wrap(Helpers.generateValidPassword(6)).as('validPassword')
  });
  it.only('Valid credentials: register and login', function() {
  cy.register(this.validLogin, this.validPassword)
  Header.checkUserLoggedIn(this.validLogin)
  })
})

describe('Negative login tests', () => {
  beforeEach(() => {
    cy.visit(appUrl)
    Header.signIn()
  });
  it('invalid credentials: invalid format', () => {
    cy.logIn('invalid', 'invalid')
    LoginPage.checkAlertText('The email address is badly formatted.')
  })
  it('invalid credentials: empty credentials', () => {
    cy.logIn(' ', ' ')
    LoginPage.checkAlertText('The email address is badly formatted.')
  })
  it('invalid credentials: unexisting user', () => {
    cy.logIn('valid@format.but', 'no such user')
    LoginPage.checkAlertText('There is no user record corresponding to this identifier. The user may have been deleted.')
  })
})
