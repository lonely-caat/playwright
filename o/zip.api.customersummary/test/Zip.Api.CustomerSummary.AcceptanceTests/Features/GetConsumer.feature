Feature: Get Consumer
	In order to retrieve consumer's information
	As an api consumer
	I want to get consumer

Scenario: Get Consumer
	Given I have following ConsumerId
	When I make a get request to /consumers/id
	| consumerId | expected |
	| 2919       | 200      |
	| 0          | 400      |
	| -1324      | 400      |
	Then the response status code should be as expected
