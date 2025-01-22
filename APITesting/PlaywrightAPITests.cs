using FluentAssertions;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var product = await response.JsonAsync();

            //Fluent Assertions
            response.Should().NotBeNull();
            response.Status.Should().Be(200);
            response.StatusText.Should().Be("OK");
            product.Value.GetRawText().Should().Contain("Keyboard");

        }
    }
}
