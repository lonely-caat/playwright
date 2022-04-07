Feature: GetLatestPaymentMethods
	In order to know what are the customer's default payment methods
	As an api consumer
	I want to know it

Scenario: Get Latest PaymentMethods
	When I make a get request to /paymentmethods/latest with following test cases
	| consumerId | state    | expected |
	| 123        |          | 200      |
	| 0          |          | 400      |
	| 123        | Declined | 200      |
	Then the response status code should be expected
