export default class LoginPage{
    proceedToCheckout() {
        cy.contains('Proceed to Checkout').click();
    }

    removeFromBasket(item){
        console.log(cy.get('.checkoutProduct__title').contains(item).children())
    }
}
