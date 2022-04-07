Feature: FindPayments
	In order to find out user's payments history
	As an api consumer
	I want to find payments

Scenario: Find Payments
	When I make a get request to /payments with following query strings
	| accountId | fromDate   | toDate     | batchId                              | expected |
	| 0         |            |            |                                      | 400      |
	| 123       |            |            |                                      | 200      |
	| 123       | 2019-01-01 | 2020-01-01 |                                      | 200      |
	| 123       | 2019-01-01 | 2020-01-01 | 7472E228-F2DA-4E86-8B76-8D4838AC5715 | 200      |
	Then the response status code should be expected
