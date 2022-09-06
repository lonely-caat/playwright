Feature: Get LmsTransactions
	In order to get lms transactions
	As an api consumer
	I want to get the lms transactions of this consumer

Scenario: Get LmsTransactions
	When I make a get request to /accounts/accountId/lmstransactions with the following account ids the response status code should be expected
		| accountId | statusCode |
		| 12345     | 200        |
		| 39281     | 200        |
		| 9239023   | 200        |
		| 0         | 400        |
		| -123      | 400        |