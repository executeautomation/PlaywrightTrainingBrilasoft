Feature: Login
	Perform login operations in the EA App

	#BDD Scenario which can be understood by BA/Dev/QA and other stake holders
@smoke @regression @release-514
Scenario: Login with correct details
	Given I navigate to the site
	And I click login link
	When I enter login details like username as "admin" and password as "password"
	Then I should see the log off link in the page


@smoke @regression @release-514
Scenario: Login with Incorrect details
	Given I navigate to the site
	And I click login link
	When I enter login details like username as "admin" and password as "passwords"
	Then I should not see the log off link in the page

