Feature: FindTransactions
	In order to view my transactions in history
	As an api consumer
	I want to be told the sum of two numbers

@mytag
Scenario: Find Transactions
	When I make a get request to /transactions with following test cases
	| consumerId | startDate  | endDate    | expected |
	| 0          |            |            | 400      |
	| 123        |            |            | 200      |
	| 123        | 2019-01-01 | 2020-01-01 | 200      |
	Then the response status code should be expected
