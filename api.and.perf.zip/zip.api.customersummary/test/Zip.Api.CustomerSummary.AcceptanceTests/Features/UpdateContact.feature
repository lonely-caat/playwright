Feature: UpdateContact
	In order to update consumer's contact information
	As an api consumer
	I want to update it

Scenario: Update Contact
	When I make a put request to /contacts with following data
	| consumerId | email       | mobile     | expected |
	| 12345      |             |            | 400      |
	| 0          | test@zip.co | 0412345678 | 400      |
	| 12345      | test@zip.co | 0412345678 | 200      |
	| 12345      | test@zip.co |            | 200      |
	| 12345      |             | 0412345678 | 200      |
	 Then the response status code should be expected
