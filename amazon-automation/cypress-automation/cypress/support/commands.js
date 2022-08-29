// ***********************************************
// This example commands.js shows you how to
// create various custom commands and overwrite
// existing commands.
//
// For more comprehensive examples of custom
// commands please read more here:
// https://on.cypress.io/custom-commands
// ***********************************************
//
//
// -- This is a parent command --
// Cypress.Commands.add('login', (email, password) => { ... })
//
//
// -- This is a child command --
// Cypress.Commands.add('drag', { prevSubject: 'element'}, (subject, options) => { ... })
//
//
// -- This is a dual command --
// Cypress.Commands.add('dismiss', { prevSubject: 'optional'}, (subject, options) => { ... })
//
//
// -- This will overwrite an existing command --
// Cypress.Commands.overwrite('visit', (originalFn, url, options) => { ... })

Cypress.Commands.add('isVisible', selector => {
    cy.get(selector).should('be.visible');
})
Cypress.Commands.add('isHidden', selector => {
    cy.get(selector).should('not.exist');
})

// TODO: question can default parameters be data types? e.g. username:string ?
// TODO rewrite as an API call maybe?
// TODO should we add a catch to check if logged in already?
// TODO should we add a check if we logged in? then we will need to repeat the same functionality for negative tests
Cypress.Commands.add('logIn', (username, password) => {
    // cy.get('.header__optionLineTwo').click();
    cy.contains('Sign-in').should('be.visible');
    cy.get('#email').type(username)
    cy.get('#password').type(password)
    cy.get('.login__signInButton').click();
})

// TODO maybe create one function intstead of two with flag like register=true? but then the function name will be confusing ><
Cypress.Commands.add('register', (username, password) => {
    // cy.get('.header__optionLineTwo').click();
    cy.contains('Sign-in').should('be.visible');
    cy.get('#email').type(username)
    cy.get('#password').type(password)
    cy.get('.login__registerButton').click();
})
Cypress.Commands.add('logInWithApi', (username, password) => {

})