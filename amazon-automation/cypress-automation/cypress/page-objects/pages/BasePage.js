export default class BasePage {
    logInfo(message) {
        cy.log(message)
    }

    setMobileViewport() {
        cy.viewport('iphone-x')
    }

    setTableViewport() {
        cy.viewport('ipad-2')
    }

    setDesktopViewport() {
        cy.viewport('macbook-13')
    }

    setLargeDesktopViewport() {
        cy.viewport('macbook-15')
    }
}
