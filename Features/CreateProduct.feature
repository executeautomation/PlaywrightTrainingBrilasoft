Feature: CreateProduct
	Create a Product

Background: 
	Given I clean up the application products before execution
		| ProductName     |
		| Product_Umang   |
		| Product_Ranjtha |

@smoke @regression @release-514
Scenario: Create Product with all valid details
	Given I navigate to the site
	And I click Product link
	When I click Create link
	And I enter the following data into the new product form
    | Name          | Description       | Price | ProductType |
    | Product_Umang | Description_Umang | 72    | 2           |
	Then I should see "Product_Umang" in list
	And I also verify the backend of application


@smoke @regression @release-514
Scenario: Create Product with more valid details
	Given I navigate to the site
	And I click Product link
	When I click Create link
	And I enter the following data into the new product form
    | Name            | Description          | Price | ProductType |
    | Product_Ranjtha | Description_Ranjitha | 72    | 2           |
	Then I should see "Product_Ranjtha" in list
	And I also verify the backend of application