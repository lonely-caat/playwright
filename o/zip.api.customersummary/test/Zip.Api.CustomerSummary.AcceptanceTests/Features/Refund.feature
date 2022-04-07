Feature: Refund
	In order to refund
	As an api consumer
	I want to refund a payment

Scenario: Refund
	When I make a post request to /payments/id/refund with following test cases
	| id                                   | expected |
	| 5A0DE87C-5E0C-40ED-803C-E74AE51F18D6 | 200      |
	| 00000000-0000-0000-0000-000000000000 | 400      |
	Then the response status code should be expected
