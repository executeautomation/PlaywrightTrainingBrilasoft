using Microsoft.Playwright;
using PlaywrightTestDemo.Utilities;

namespace PlaywrightTestDemo.Pages
{
    public class CreateProduct
    {
        private readonly IPage _page;
        //Constructor
        public CreateProduct(IPage page) => _page = page;

        //Locators to be declared in the page level
        private ILocator _lnkCreate => _page.Locator("//a[normalize-space(.)='Create']");
        private ILocator _fieldName => _page.Locator("//input[@id='Name']");
        private ILocator _fieldDescription => _page.Locator("//input[@id='Description']");
        private ILocator _fieldPrice => _page.Locator("//input[@id='Price']");
        private ILocator _dropdownType => _page.Locator("//select[@id='ProductType']");
        private ILocator _btnCreate => _page.Locator("//input[@id='Create']");
        private ILocator _productExist(string product) => _page.Locator($"//td[normalize-space(.)='{product}']").Nth(0);


        //Action methods
        public async Task ClickCreateAsync()
        {
            await _lnkCreate.ClickAsync();
        }

        public async Task CreateNewProduct(Product product)
        {
            await _fieldName.FillAsync(product.Name);
            await _fieldDescription.FillAsync(product.description);
            await _fieldPrice.FillAsync(product.price.ToString());
            await _dropdownType.SelectDropDownWithIndexAsync(product.ProductType);
            await _btnCreate.ClickAsync();
        }

        public async Task DoesProductExist(string productName)
        {
            Assert.True(await _productExist(productName).IsVisibleAsync());
        }
    }
}
