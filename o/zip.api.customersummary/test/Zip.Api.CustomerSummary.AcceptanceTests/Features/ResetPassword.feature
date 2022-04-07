Feature: Reset Password
	In order to help customers
	As an api consumer
	I want to reset customer's password

Scenario: Reset password
	When I make a post request to /contacts/password/reset with following test cases
	| email       | firstName | classification | expected |
	| test@zip.co | Larry     | zipPay         | 200      |
	|             | Larry     | zipPay         | 400      |
	| test@zip.co |           | zipPay         | 400      |
	| test@zip.co | Larry     |                | 400      |
	Then the response status code should be expected
