import BasePage from '../pages/BasePage'

export default class LoginPage extends BasePage {
    static checkAlertText(expectedText) {
        cy.on('window:alert', (str) => {
            expect(str).to.equal(expectedText)
        })
    }
    // TODO: should we maybe just reuse cy.logIn command?
    // static logIn(user,password){
    //     cy.logIn(user,password)
    // }
}