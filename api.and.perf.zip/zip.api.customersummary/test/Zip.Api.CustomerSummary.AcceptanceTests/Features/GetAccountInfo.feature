Feature: GetAccountInfo
	In order to get consumer's account info
	As an api consumer
	I want to get consumer's account info

Scenario: Get AccountInfo
	When I make get request to /consumers/accountinfo with following data
	| consumerId | expected |
	| 2919       | 200      |
	| 0          | 400      |
	| -2309      | 400      |
	Then the response status code should as expected
