import BasePage from '../pages/BasePage'

export default class LoginPage extends BasePage {
    checkAlertText(expectedText) {
        cy.on('window:alert', (str) => {
            expect(str).to.equal(expectedText)
        })
    }
}