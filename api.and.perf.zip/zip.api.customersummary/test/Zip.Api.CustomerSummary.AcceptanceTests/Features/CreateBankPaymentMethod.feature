Feature: CreateBankPaymentMethod
	In order to create a new payment method for customer
	As an api consumer 
	I want to create a bank payment method

Scenario: Create Bank PaymentMethod
	When I make a post request to /paymentmethods/bank with following test cases
	| consumerId | accountName | accountNumber | bsb    | originatorEmail | expected |
	| 0          | Larry       | 38291928      | 082938 | test@zip.co     | 400      |
	| 1292       |             | 38291030      | 082920 | test@zip.co     | 400      |
	| 123123     | Larry       |               | 082938 | test@zip.co     | 400      |
	| 123123     | Larry       | 38291928      |        | test@zip.co     | 400      |
	| 123123     | Larry       | 38291928      | 082938 |                 | 200      |
	| 123        | Larry       | 38291928      | 082938 | test@zip.co     | 200      |
	Then the response status code should be expected
