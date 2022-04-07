Feature: GetContact
	In order to update consumer's contact info
	As an api consumer
	I want to get the current contact info

Scenario: Get Contact
	When I make a get request to /contacts/id with following consumerId
	| consumerId | expected |
	| 0          | 400      |
	| -2393      | 400      |
	| 390292     | 200      |
	Then the response status code should be expected
