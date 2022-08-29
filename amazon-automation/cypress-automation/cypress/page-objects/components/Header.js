export default class Header {
    static clickOnLogo(){
        cy.get('.header__logo').click();
    }
    static searchProduct(product){
        cy.get('.header__searchInput').type(`${product} {enter}`)
    }
    static getBasket(){
        cy.get('MuiSvgIcon-root').click();
    }
    static signOut(){
        cy.contains('Sign Out').click();
    }
    static signIn(){
        cy.contains('Sign In').click();
    }
}