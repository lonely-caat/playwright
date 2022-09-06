Feature: GetPaymentMethods
	In order to view customer's payment methods
	As an api consumer
	I want to get the list of existing payment methods

Scenario: Get PaymentMethods
	When I make a get request to /paymentmethods with following test cases
	| consumerId | state    | expected |
	| 19219      |          | 200      |
	| 0          |          | 400      |
	| 128928     | Declined | 200      |
	Then the response status code should be expected
