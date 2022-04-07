Feature: GenerateStatements
	In order to statements
	As an api consumer
	I want to generate statements

Scenario: Generate Statements
	When I make a post request to /statements with following test cases
	| accountId | startDate  | endDate    | expected |
	| 0         | 2019-01-01 | 2020-01-01 | 400      |
	| 123       | 2019-01-01 | 2020-01-01 | 200      |
	| 129       |            |            | 200      |
	Then the response status code should be expected
