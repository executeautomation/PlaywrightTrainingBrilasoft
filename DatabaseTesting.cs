using Microsoft.Playwright;
using PlaywrightTestDemo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightTestDemo
{
    public class DatabaseTesting
    {

        [Fact]
        public async Task PerformDatabaseTesting()
        {

            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
            });
            var context = await browser.NewContextAsync();


            var products = DbHelperForSqlite.GetProducts();

            //Clear the database
            //1. Connect to the database
            //2. Command to query the data from DB and delete it
            DbHelperForSqlite.DeleteProductByName("FirstProduct");

            var page = await context.NewPageAsync();
            await page.GotoAsync("http://localhost:8000/");
            await page.GetByRole(AriaRole.Link, new() { Name = "Product" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Create" }).ClickAsync();
            await page.GetByLabel("Name").ClickAsync();
            await page.GetByLabel("Name").FillAsync("FirstProduct");
            await page.GetByLabel("Description").ClickAsync();
            await page.GetByLabel("Description").FillAsync("Contains the first MVC product");
            await page.GetByLabel("Description").ClickAsync();
            await page.GetByLabel("Description").FillAsync("Contains the first MVP product");
            await page.Locator("#Price").ClickAsync();
            await page.Locator("#Price").FillAsync("20000");
            await page.GetByLabel("ProductType").SelectOptionAsync(new[] { "1" });
            await page.GetByRole(AriaRole.Button, new() { Name = "Create" }).ClickAsync();

            //3. Verify the data in the database which is created above
            var response = DbHelperForSqlite.GetProductByName("FirstProduct");

            Assert.Equal(response.Name, "FirstProduct");
        }




        [Fact]
        public async Task PerformDatabaseTestingUsingEf()
        {

            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
            });
            var context = await browser.NewContextAsync();

            ProductDbContext productDbContext = new ProductDbContext();
            productDbContext.Products.Count();

            var product = productDbContext.Products.SingleOrDefault(x => x.Name == "FirstProduct");
            if (product is not null)
            {
                productDbContext.Products.Remove(product);
                productDbContext.SaveChanges();
            }

            var page = await context.NewPageAsync();
            await page.GotoAsync("http://localhost:8000/");
            await page.GetByRole(AriaRole.Link, new() { Name = "Product" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Create" }).ClickAsync();
            await page.GetByLabel("Name").ClickAsync();
            await page.GetByLabel("Name").FillAsync("FirstProduct");
            await page.GetByLabel("Description").ClickAsync();
            await page.GetByLabel("Description").FillAsync("Contains the first MVC product");
            await page.GetByLabel("Description").ClickAsync();
            await page.GetByLabel("Description").FillAsync("Contains the first MVP product");
            await page.Locator("#Price").ClickAsync();
            await page.Locator("#Price").FillAsync("20000");
            await page.GetByLabel("ProductType").SelectOptionAsync(new[] { "1" });
            await page.GetByRole(AriaRole.Button, new() { Name = "Create" }).ClickAsync();

            //3. Verify the data in the database which is created above
            var response = productDbContext.Products.SingleOrDefault(x => x.Name == "FirstProduct");

            Assert.Equal(response.Name, "FirstProduct");
        }
    }
}
