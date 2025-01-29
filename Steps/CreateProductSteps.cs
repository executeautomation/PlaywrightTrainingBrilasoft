using ExecuteAutomation.Reqnroll.Dynamics;
using Microsoft.Playwright;
using PlaywrightTestDemo.Pages;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightTestDemo.Steps
{
    [Binding]
    public class CreateProductSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private CreateProduct _createProduct;
        public CreateProductSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [Given("I click Product link")]
        public async void GivenIClickProductLink()
        {
            var homePage = new HomePage((IPage)_scenarioContext["Page"]);
            _createProduct = await homePage.ClickProduct();
        }

        [When("I click Create link")]
        public async Task WhenIClickCreateLink()
        {
            await _createProduct.ClickCreateAsync();
        }

        [When("I enter the following data into the new product form")]
        public void WhenIEnterTheFollowingDataIntoTheNewProductForm(DataTable dataTable)
        {
            var data = dataTable.CreateDynamicInstance();

        }

        [Then("I should see {string} in list")]
        public void ThenIShouldSeeInList(string p0)
        {
            throw new PendingStepException();
        }

    }
}
