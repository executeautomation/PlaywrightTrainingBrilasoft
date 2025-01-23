using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Playwright;
using PlaywrightTestDemo.Utilities;
using System.Text.Json;

namespace PlaywrightTestDemo.APITesting
{
    public class PlaywrightAPITests
    {

        [Fact]
        public async Task PerformGetProductByIdTestAsync()
        {

            //1. First Create an instance of Playwright
            var playwright = await Playwright.CreateAsync();

            //2. Create instance of API Request
            var apiRequestContext = new APIRequestNewContextOptions
            {
                BaseURL = "http://localhost:8001/"
            };

            var apiRequest = await playwright.APIRequest.NewContextAsync(apiRequestContext);


            //3. Perform Get Request
            var response = await apiRequest.GetAsync("/Product/GetProductById/1");

            //4. Convert the response to JSON
            var jsonResponse = await response.JsonAsync();

            var product = jsonResponse?.Deserialize<Product>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            //Fluent Assertions

            using (new AssertionScope())
            {
                response.Should().NotBeNull();
                response.Status.Should().Be(200);
                response.StatusText.Should().Be("okay");
                //product.Value.GetRawText().Should().Be("Keyboard");

                product.Name.Should().Be("Keyboard");
                product.description.Should().Be("Gaming Keyboard with light");
            }
        }

        [Fact]
        public async Task PerformGetProductsTestAsync()
        {
            //1. First Create an instance of Playwright
            var playwright = await Playwright.CreateAsync();

            //2. Create instance of API Request
            var apiRequestContext = new APIRequestNewContextOptions
            {
                BaseURL = "http://localhost:8001/"
            };

            var apiRequest = await playwright.APIRequest.NewContextAsync(apiRequestContext);


            //3. Perform Get Request
            var response = await apiRequest.GetAsync("/Product/GetProducts");

            //4. Convert the response to JSON
            var jsonResponse = await response.JsonAsync();

            var products = jsonResponse?.Deserialize<List<Product>>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            //LINQ
            var product = products.FirstOrDefault(x => x.Name == "Mouse");


            //Fluent Assertions

            using (new AssertionScope())
            {
                response.Should().NotBeNull();
                response.Status.Should().Be(200);
                response.StatusText.Should().Be("OK");

                product.Name.Should().Be("Mouse");
                product.description.Should().Be("Gaming Mouse");
            }
        }


        [Fact]
        public async Task PerformPostProductTestAsync()
        {
            //1. First Create an instance of Playwright
            var playwright = await Playwright.CreateAsync();

            //2. Create instance of API Request
            var apiRequestContext = new APIRequestNewContextOptions
            {
                BaseURL = "http://localhost:8001/"
            };

            var apiRequest = await playwright.APIRequest.NewContextAsync(apiRequestContext);


            var body = new Product(null,"DemoProductForDuplication", "Demo Description", 123, 3);


            //3. Perform POST Request
            var response = await apiRequest.PostAsync("/Product/Create", new() { DataObject = body });

            //4. Convert the response to JSON
            var jsonResponse = await response.JsonAsync();

            var product = jsonResponse?.Deserialize<Product>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            //Fluent Assertions

            using (new AssertionScope())
            {
                response.Should().NotBeNull();
                response.Status.Should().Be(200);
                response.StatusText.Should().Be("OK");

                product.Name.Should().Be("DemoProduct");
                product.description.Should().Be("Demo Description");
            }
        }



        [Fact]
        public async Task PerformPostProductsTestAsync()
        {
            //1. First Create an instance of Playwright
            var playwright = await Playwright.CreateAsync();

            //2. Create instance of API Request
            var apiRequestContext = new APIRequestNewContextOptions
            {
                BaseURL = "http://localhost:8001/"
            };

            var apiRequest = await playwright.APIRequest.NewContextAsync(apiRequestContext);


            var body = new List<Product>()
            {
                new Product(12, "DemoProduct2", "Demo Description2", 456, 3),
                new Product(13, "DemoProduct3", "Demo Description3", 654, 2),
                new Product(14, "DemoProduct4", "Demo Description4", 789, 1),
            };
                
            //3. Perform POST Request
            var response = await apiRequest.PostAsync("/Product/CreateProducts", new() { DataObject = body });

            //4. Convert the response to JSON
            var jsonResponse = await response.JsonAsync();

            var products = jsonResponse?.Deserialize<List<Product>>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            //Fluent Assertions

            using (new AssertionScope())
            {
                response.Should().NotBeNull();
                response.Status.Should().Be(200);
                response.StatusText.Should().Be("OK");

               products.Should().NotBeNull();
            }
        }
    }



}
