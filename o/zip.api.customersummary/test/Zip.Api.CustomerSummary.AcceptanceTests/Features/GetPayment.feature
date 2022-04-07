Feature: GetPayment
	In order to view customer's payment
	As an api consumer
	I want to get payment of customers

Scenario: Get Payment
	When I make a get request to /payments/id with following test cases
	| id                                   | expected |
	| BC738FE9-F070-42A3-8570-9DB914C046B3 | 200      |
	|                                      | 400      |
	Then the response status code should be expected
