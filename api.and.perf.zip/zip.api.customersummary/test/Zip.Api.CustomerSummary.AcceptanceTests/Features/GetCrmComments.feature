Feature: GetCrmComments
	In order to view customer's operations
	As an api consumer
	I want to get the list of crm comments

Scenario: Get Crm Comments
	When I make a get request to /crmcomments with following test cases
	| consumerId | expected |
	| 123455     | 200      |
	| 0          | 400      |
	Then the response status code should be expected
