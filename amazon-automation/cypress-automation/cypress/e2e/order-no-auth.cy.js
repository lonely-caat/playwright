import {appUrl} from "../../config";
import Header from "../page-objects/components/Header";
import LoginPage from "../page-objects/pages/LoginPage";
import StorePage from "../page-objects/pages/StorePage";

describe('Positive order tests without authorization', () => {
    beforeEach(() => {
        cy.visit(appUrl)
    });
    it('can add items to cart', () => {
        StorePage.addToBasket('Amazon Echo')

    })
})