Feature: MakePayNowPayment
	In order to make an instant payment
	As an api consumer
	I want to make a paynow payment

Scenario: Make PayNow Payment
	When I make a post request to /payments/paynow with following test cases
	| consumerId | amount | email       | ip        | expected |
	| 0          | 120    | test@zip.co | 127.0.0.1 | 400      |
	| 123        | 0      | test@zip.co | 127.0.0.1 | 400      |
	| 123        | 120    |             | 127.0.0.1 | 400      |
	| 1292       | 120    | test@zip.co |           | 400      |
	| 2921888    | 120    | test@zip.co | 127.0.0.1 | 200      |
	Then the response status code should be expected
