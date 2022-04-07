Feature: GetAvailableStatementDates
	In order to generate a statement
	As an api consumer
	I want to see all my statement date ranges beforehand

Scenario: Get available statement date ranges
	When I make a get request to /statements/availabledate with following test cases
	| accountId | expected |
	| 123123    | 200      |
	| 0         | 400      |
	Then the response status code should be expected
