Feature: Add Repayment
	In order to add repayment for consumer
	As an api consumer
	I want to add repayment for consumer

Scenario: Add Repayment
	When I make a post request to /accounts/accountid/repayment with following repayments to add and expect the response status code is as expected
	| accountId | amount | frequency   | startDate (in * days) | changedBy   | statusCode |
	| 123       | 88     | Monthly     | 5                     | test@zip.co | 200        |
	| 234       | 40     | Fortnightly | 10                    | test@zip.co | 200        |
	| 456       | 20     | Weekly      | 50                    | test@zip.co | 200        |
	| 0         | 30     | Monthly     | 5                     | test@zip.co | 400        |
	| 234       | 0      | Monthly     | 5                     | test@zip.co | 400        |
	| 39223     | -20    | Weekly      | 30                    | test@zip.co | 400        |
	| 123       | 40     | Weekly      | -1                    | test@zip.co | 400        |
	| 333       | 40     | Weekly      | 15                    |             | 200        |
