const { somethingLike: like } = require('@pact-foundation/pact').Matchers;
const cardData = require('../req-resp/cardsApi');

module.exports = {
    getCardData: {
        state: 'it has the ability to return card json',
        uponReceiving: 'Get request to return card data based on card id',
        withRequest: {
            method: 'GET',
            path: '/internal/cards/e16a5f31-a6bf-43f7-b43a-bf5a4e381326',
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(cardData.responses.validCardData),
        },
    },
    getUnexistingCardData: {
        state: 'it returns error when trying to return json for a card that does not exist',
        uponReceiving: 'Get request to return card data based on card id that does not exist',
        withRequest: {
            method: 'GET',
            path: '/internal/cards/noexist',
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 404,
            body: like(cardData.responses.unexistingCardData),
        },
    },

    getCardDataByExternalId: {
        state: 'it has the ability to return card json by external Id',
        uponReceiving: 'Get request to return card data based on external id',
        withRequest: {
            method: 'GET',
            path: '/internal/cards',
            query: {externalId:'0581c676-246f-4eeb-b520-f9cd7140dadb'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(cardData.responses.validCardDataByExternalId),
        },
    },
    getCardDataByUnexistingExternalId: {
        state: 'it returns error when trying to return card json by external Id that does not exist',
        uponReceiving: 'Get request to return card data based on external id that does not exist',
        withRequest: {
            method: 'GET',
            path: '/internal/cards',
            query: {externalId:'noexist'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(cardData.responses.CardDataByUnexistingExternalId),
        },
    },

    getCardDataByCustomerId: {
        state: 'it has the ability to return card json by customer Id',
        uponReceiving: 'Get request to return card data based on customer id',
        withRequest: {
            method: 'GET',
            path: '/internal/cards',
            query: {customerId:'8b4450c9-895a-433e-882d-bb024fe5de47'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(cardData.responses.validCardDataByCustomerId),
        },
    },
    getCardDataByUnexistingCustomerId: {
        state: 'it returns error when trying to return card json by customer Id that does not exist',
        uponReceiving: 'Get request to return card data based on customer id that does not exist',
        withRequest: {
            method: 'GET',
            path: '/internal/cards',
            query: {customerId:'noexist'},
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 200,
            body: like(cardData.responses.CardDataByUnexistingCustomerId),
        },
    },

    putBlockValidCard: {
        state: 'it has the ability to block card by card id and customer id',
        uponReceiving: 'Put request to block card by card id and customer id',
        withRequest: {
            method: 'PUT',
            path: '/cards/7edca99d-857a-4252-8454-f8a0786f4a24/block',
            headers: {
                'Content-Type': 'application/json',
                'Customer-Id': '8b4450c9-895a-433e-882d-bb024fe5de47',
            },
        },
        willRespondWith: {
            status: 204,
        },
    },
    putBlockUnexistingCard: {
        state: 'it returns error for card id that does not exist when trying to block',
        uponReceiving: 'Put request to block card  with card id that does not exist',
        withRequest: {
            method: 'PUT',
            path: '/cards/noexist/block',
            headers: {
                'Content-Type': 'application/json',
                'Customer-Id': '8b4450c9-895a-433e-882d-bb024fe5de47',
            },
        },
        willRespondWith: {
            status: 404,
            body: like(cardData.responses.BlockUnexistingCard)
        },
    },
    putBlockUnexistingCustomerId: {
        state: 'it returns error for customer id that does not exist',
        uponReceiving: 'Put request to block card by customer id that does not exist',
        withRequest: {
            method: 'PUT',
            path: '/cards/7edca99d-857a-4252-8454-f8a0786f4a24/block',
            headers: {
                'Content-Type': 'application/json',
                'Customer-Id': 'noexist',

            },
        },
        willRespondWith: {
            status: 403,
        },
    },

    putUnblockValidCard: {
        state: 'it has the ability to Unblock card by card id and customer id',
        uponReceiving: 'Put request to Unblock card by card id and customer id',
        withRequest: {
            method: 'PUT',
            path: '/cards/7edca99d-857a-4252-8454-f8a0786f4a24/unblock',
            headers: {
                'Content-Type': 'application/json',
                'Customer-Id': '8b4450c9-895a-433e-882d-bb024fe5de47',
            },
        },
        willRespondWith: {
            status: 204,
        },
    },
    putUnblockUnexistingCard: {
        state: 'it returns error when trying to unblock by card id that does not exist',
        uponReceiving: 'Put request to Unblock card by card id that does not exist',
        withRequest: {
            method: 'PUT',
            path: '/cards/noexist/unblock',
            headers: {
                'Content-Type': 'application/json',
                'Customer-Id': '8b4450c9-895a-433e-882d-bb024fe5de47',
            },
        },
        willRespondWith: {
            status: 404,
            body: like(cardData.responses.BlockUnexistingCard)
        },
    },
    putUnblockUnexistingCustomerId: {
        state: 'it returns error for card id that does not exist for customer id when unblocking',
        uponReceiving: 'Put request to Unblock card by customer id that does not exist',
        withRequest: {
            method: 'PUT',
            path: '/cards/7edca99d-857a-4252-8454-f8a0786f4a24/unblock',
            headers: {
                'Content-Type': 'application/json',
                'Customer-Id': 'noexist',

            },
        },
        willRespondWith: {
            status: 403,
        },
    },

    postTransitionValidCard: {
        state: 'it has the ability to transition a card by body provided',
        uponReceiving: 'Post request to transition card',
        withRequest: {
            method: 'POST',
            path: '/internal/digitalwallet/tokentransition',
            body: cardData.requests.TransitionValidCard,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 201,
        },
    },
    postTransitionInValidCard: {
        state: 'it returns an error when trying to transition a card with invalid body provided',
        uponReceiving: 'Post request to transition invalid body',
        withRequest: {
            method: 'POST',
            path: '/internal/digitalwallet/tokentransition',
            body: cardData.requests.TransitionInValidCard,
            headers: {
                'Content-Type': 'application/json',
            },
        },
        willRespondWith: {
            status: 404,
            body: like(cardData.responses.TransitionInValidCard)
        },
    },

};
