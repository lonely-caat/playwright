
module.exports = {
    requests: {
        statementsTrigger: {
            "Accounts": [
                "134"
            ],
            "Classification": "1",
            "StatementDate": "2021-03-01"
        },

        statementsUnexistingTrigger: {
            "Accounts": [
                "noexist"
            ],
            "Classification": "noexist",
            "StatementDate": "3029-63-21"
        },

        responses: {},
    },
};
