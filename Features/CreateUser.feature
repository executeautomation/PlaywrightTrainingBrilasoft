Feature: CreateUser
	Create a User in EAApp

@smoke @regression @release-514
Scenario: Create User with valid information
	Given I navigate to the site
	And I click login link
	When I enter login details like username as "admin" and password as "password"
	Then I should see the log off link in the page
