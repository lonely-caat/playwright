Feature: AddCrmComment
	In order to record customer's operations
	As an api consumer
	I want to add crm comment

Scenario: Add Crm Comment
	When I make a post request to /crmcomments with following test cases
	| referenceId | category | type        | detail | commentby      | expected |
	| 123456      |          | Application | test   | shan.ke@zip.co | 400      |
	| 123456      | General  |             | test   | shan.ke@zip.co | 400      |
	| 0           | General  | Consumer    | test   | shan.ke@zip.co | 400      |
	| 123456      | General  | Consumer    |        | shan.ke@zip.co | 400      |
	| 123456      | General  | Consumer    | test   |                | 200      |
	| 123456      | General  | Consumer    | test   | shan.ke@zip.co | 200      |
	Then the response status code should be expected
