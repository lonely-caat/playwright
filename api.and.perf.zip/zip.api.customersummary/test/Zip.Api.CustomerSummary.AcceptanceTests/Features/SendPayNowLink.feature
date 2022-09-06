Feature: SendPayNowLink
	In order to make a pay now payment
	As an api consumer
	I want to send a paynow link to customer's email

Scenario: Send PayNow Link
	When I make a post request to /outgoingmessages/paynowlink/sms with following test cases
	| consumerId | amount | expected |
	| 12345      | 12.43  | 200      |
	| 0          | 10     | 400      |
	| 12345      | 0      | 400      |
	Then the response status code should be expected
