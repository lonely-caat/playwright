Feature: CloseAccount
	In order to answer consumer's request
	As an api consumer
	I want to be able to close consuer's account

Scenario: Close Account
	When I make a post request to /consumers/id/closeaccount with following data
	| consumerId | accountId | creditStateType      | creditProfileId | comments                 | changedBy      | expected |
	| 0          | 12892     | Refer2               | 32              |                          | shan.ke@zip.co | 400      |
	| 123        | 0         | Refer2               | 32              |                          | shan.ke@zip.co | 400      |
	| 123        | 32        |                      | 32              |                          | shan.ke@zip.co | 400      |
	| 123        | 32        | ApplicationCompleted |                 |                          | shan.ke@zip.co | 400      |
	| 123        | 32        | ApplicationCompleted | 323             |                          |                | 200      |
	| 123        | 32        | ApplicationCompleted | 323             | no longer want to use it | shan.ke@zip.co | 200      |
	Then the response status code should be as expected
