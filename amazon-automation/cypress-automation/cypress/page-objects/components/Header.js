export default class Header {
     clickOnLogo(){
        cy.get('.header__logo').click();
    }
     searchProduct(product){
        cy.get('.header__searchInput').type(`${product} {enter}`)
    }
     getBasket(){
        cy.get('MuiSvgIcon-root').click();
    }
     signOut(){
        cy.contains('Sign Out').click();
    }
     signIn(){
        cy.contains('Sign In').click();
    }
     checkUserLoggedIn(username){
        cy.get('.header__optionLineOne').contains(`Hello ${username}`)
    }
}