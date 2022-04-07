Feature: UpdateConsumer
	In order to update consumer's info 
	As an api consumer
	I want to do update

Scenario: Update name
	When In order to update names, I make a post request to /consumers with following new names
	| consumerId | firstName | lastName | expected |
	| 291        |           |          | 400      |
	| 0          | Larry     | Peter    | 400      |
	| -392       | Larry     | Peter    | 400      |
	| 1234       | Tiger     | Woods    | 200      |
	Then the response status code should be expected

Scenario: Update date of birth
	When In order to update date of birth, I make a post request to /consumers with following new birthdays
	| consumerId | date of birth | expected |
	| 123        | 2019-01-01    | 400      |
	| 3929       | 2000-01-01    | 200      |
	| 123        | 2030-01-01    | 400      |
	Then the response status code should be expected

Scenario: Update gender
	When In order to update gender, I make a post request to /consumer with following new genders
	| consumerId | gender         | expected |
	| 39292      | Other          | 200      |
	| 392        | Male           | 200      |
	| 39292      | Female         | 200      |
	| 3929       | FemaleWithKids | 200      |
	| 392        | MaleWithKids   | 200      |
	| 39292      | None           | 200      |
	| 0          | Male           | 400      |
	| -392       | Female         | 400      |
	Then the response status code should be as expected

Scenario: Update address
	When In order to update address, I make a post request to /consumer with following new addresses
	| consumerId | countryCode | state      | suburb   | streetName  | streetNumber | postCode | unitNumber | expected |
	| 123123     | "AU"        | "NSW"      | "Sydney" | "Spring St" | 10           | 2000     | LEVEL 10   | 200      |
	| 123123     | "NZ"        | "AUCKLAND" | "Smith"  | "Lady St"   | 39           | 3920     |            | 200      |
	| 123123     |             | "AUCKLAND" | "Smith"  | "Lady St"   | 39           | 3920     |            | 400      |
	| 123123     | "AU"        |            | "Smith"  | "Lady St"   | 39           | 3920     |            | 400      |
	| 123123     | "NZ"        | "AUCKLAND" |          | "Lady St"   | 39           | 3920     |            | 400      |
	| 123123     | "AU"        | "NSW"      | "Sydney" |             | 10           | 2000     | LEVEL 10   | 400      |
	| 123123     | "AU"        | "NSW"      | "Sydney" | "Spring St" |              | 2000     | LEVEL 10   | 400      |
	| 123123     | "AU"        | "NSW"      | "Sydney" | "Spring St" | 10           |          | LEVEL 10   | 400      |
	| 123123     | "AU"        | "NSW"      | "Sydney" | "Spring St" | 10           | 2000     |            | 200      |
	Then the response status code should be expected