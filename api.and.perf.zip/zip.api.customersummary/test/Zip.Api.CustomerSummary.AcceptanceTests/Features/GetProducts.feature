Feature: Get Products
	In order to see how many products there supports
	As an API consumer
	I want to get products list

Scenario: Get Products
	When I make a get request to /products
	Then the response status code should be 200 OK
	And the response content should be products
