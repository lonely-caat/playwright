Feature: SearchAccounts
	In order to get consumers
	As an api consumer
	I want to get a list of consumers

Scenario: Search Accounts
	When I make a get request to /accounts
	Then the response should be 200 OK
	And the response content should be AccountListItems
