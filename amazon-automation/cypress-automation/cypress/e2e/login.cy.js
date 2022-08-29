import { appUrl } from '../../config'
import Header from '../page-objects/components/Header'
import LoginPage from '../page-objects/pages/LoginPage'

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

describe('Positive login tests', () => {
})