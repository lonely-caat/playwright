Feature: GetCloseAccount
	In order to close consumer's account
	As an api consumer
	I want to get consumer's closure info to see if I can close this account

Scenario: Get CloseAccount
	When I make a get request to /consumers/id/closeaccount with following data
	| consumerId | expected |
	| 0          | 400      |
	| -23923     | 400      |
	| 39292      | 200      |
	Then the response status code should be expected
