Feature: HoldPayment
	In order to hold consumer's payment
	As an api consumer
	I want to hold it

Scenario: Hold Payment
	Given I have the following AccountId 
	And HoldDate
	When I make a post request to /accounts/id/holdpayment 
	| accountId | holdDate in * days | expected |
	| 18271     | 10                 | 200      |
	| 0         | 3                  | 400      |
	| -1239     | 49                 | 400      |
	| 39281     | -19                | 400      |
	| -291      | -392               | 400      |
	Then the response status code should be same as expected
