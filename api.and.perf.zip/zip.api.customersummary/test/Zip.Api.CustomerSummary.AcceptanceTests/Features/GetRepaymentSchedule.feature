Feature: GetRepaymentSchedule
	In order to get consumer's repayment schedule
	As an api consumer
	I want to be able to get it

Scenario: Get Repayment Schedule
	When I make a get request to /accounts/accountid/repaymentschedule with the following accountIds and the response status code should be match
	| accountId | statusCode |
	| 0         | 400        |
	| -1239129  | 400        |
	| 23934     | 200        |
	| 29291     | 200        |