Feature: Get Countries
	In Order to get countries list from the API
	As an API Consumer
	I want to get countries list

Scenario: Get Countries
	When I make a get request to /countries
	Then the response status code is 200 OK
	And the response content are countries