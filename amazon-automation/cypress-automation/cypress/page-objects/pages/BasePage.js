export default class BasePage {
// TODO does this make sense?
    static logInfo(message) {
        cy.log(message)
    }

    static setMobileViewport() {
        cy.viewport('iphone-x')
    }

    static setTableViewport() {
        cy.viewport('ipad-2')
    }

    static setDesktopViewport() {
        cy.viewport('macbook-13')
    }

    static setLargeDesktopViewport() {
        cy.viewport('macbook-15')
    }
}
