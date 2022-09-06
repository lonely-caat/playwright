Feature: SetDefaultPaymentMethod
	In order to make a payment
	As an api consumer
	I want to set a default payment method for customers

Scenario: Set Default PaymentMethod
	When I make a post request to /paymentmethods/id/default with following test cases
	| paymentMethodId                      | consumerId | expected |
	|                                      | 123        | 400      |
	| 53867006-B07E-4BCD-8D9B-52FF33C876AA | 0          | 400      |
	| 53867006-B07E-4BCD-8D9B-52FF33C876AA | 123        | 200      |
	Then the response status code should be expected
