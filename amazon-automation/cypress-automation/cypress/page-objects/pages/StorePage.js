export default class StorePage{
    static addToBasket(item, amount=1){
        console.log(cy.get('.product__info').contains(item).children())

    }

}