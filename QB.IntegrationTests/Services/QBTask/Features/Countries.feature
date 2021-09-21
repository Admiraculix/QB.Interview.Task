Feature: 
As QuckBase interviewer 
I want to retrieve all Countries from Db

#@mytag
Scenario: Successfully retrieve all counties from QB.API
	Given I have country with id 1 and name U.S.A.
	When I get all countries
	Then the result list should be greater than 0 