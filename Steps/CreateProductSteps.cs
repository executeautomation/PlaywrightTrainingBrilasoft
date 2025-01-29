using ExecuteAutomation.Reqnroll.Dynamics;
using FluentAssertions;
using Microsoft.Playwright;
using PlaywrightTestDemo.Pages;
using PlaywrightTestDemo.Utilities;
using Reqnroll;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlaywrightTestDemo.Steps
{
    [Binding]
    public class CreateProductSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly ProductDbContext _productDbContext;
        private CreateProduct _createProduct;
        public CreateProductSteps(ScenarioContext scenarioContext, ProductDbContext productDbContext)
        {
            _scenarioContext = scenarioContext;
            _productDbContext = new ProductDbContext();
        }
        [Given("I click Product link")]
        public async Task GivenIClickProductLink()
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
        public async Task WhenIEnterTheFollowingDataIntoTheNewProductForm(DataTable dataTable)
        {
            dynamic data = dataTable.CreateDynamicInstance();

            var product = new Product(0, (string)data.Name, (string)data.Description, (int)data.Price, (int)data.ProductType);

            await _createProduct.CreateNewProduct(product);

            _scenarioContext.Add("ProductName", (string)data.Name);
        }

        [Then("I should see {string} in list")]
        public async Task ThenIShouldSeeInList(string productName)
        {
            await _createProduct.DoesProductExist(productName);
        }

        [Then("I also verify the backend of application")]
        public void ThenIAlsoVerifyTheBackendOfApplication()
        {
            var productName = (string)_scenarioContext["ProductName"];

            var product = _productDbContext.Products.FirstOrDefault(x => x.Name == productName);

            product.Should().NotBeNull();
            product.Name.Should().BeEquivalentTo(productName);
        }


    }
}
