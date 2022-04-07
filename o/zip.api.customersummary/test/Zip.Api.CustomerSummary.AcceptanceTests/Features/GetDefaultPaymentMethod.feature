Feature: GetDefaultPaymentMethod
	In order to make a payment
	As an api consumer
	I want to get customer's default payment method

Scenario: Get Default PaymentMethod
	When I make a get request to /paymentmethods/default with following test cases
	| consumerId | expected |
	| 1292       | 200      |
	| 0          | 400      |
	Then the response status code should be expected
