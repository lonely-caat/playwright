export default class LoginPage{
    static proceedToCheckout() {
        cy.contains('Proceed to Checkout').click();
    }

    static removeFromBasket(item){
        console.log(cy.get('.checkoutProduct__title').contains(item).children())
    }
}
