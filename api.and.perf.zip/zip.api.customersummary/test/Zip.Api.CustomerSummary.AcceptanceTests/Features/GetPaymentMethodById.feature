Feature: GetPaymentMethodById
	In order to get payment method
	As an api consumer
	I want to get payment method by its id

Scenario: Get PaymentMethod by id
	When I make a get request to /paymentmethods/id with following test cases
	| id                                   | expected |
	| DEFA44C8-A23E-4219-9B41-FDCF66D355AE | 200      |
	| 00000000-0000-0000-0000-000000000000 | 400      |
	Then the response status code should be expected
