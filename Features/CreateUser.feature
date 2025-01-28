Feature: CreateUser
	Create a User in EAApp

@smoke @regression @release-514
Scenario: Create User with valid information
	Given I navigate to the site
	And I click login link
	When I enter login details like username as "admin" and password as "password"
	And I click the Employee list link
	When I click the Create New Button
	And I start creating user with following details
		| Name     | Salary | Duration Worked | Grade  | Email              |
		| DemoUser | 1000   | 8               | Junior | demouser@gmail.com |
	And I verify the user "DemoUser" is created
