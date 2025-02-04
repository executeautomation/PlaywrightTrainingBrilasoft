﻿using FluentAssertions;
using FluentAssertions.Execution;
using PlaywrightTestDemo.Utilities;
using System.Text.Json;

namespace PlaywrightTestDemo.APITesting
{
    public class PlaywrightAPITests : 
        IClassFixture<PlaywrightAPIDriver>, 
        IClassFixture<ProductDbContext>
    {
        private readonly PlaywrightAPIDriver _playwrightAPIDriver;
        private readonly ProductDbContext _productDbContext;

        public PlaywrightAPITests(
            PlaywrightAPIDriver playwrightAPIDriver,
            ProductDbContext productDbContext)
        {
            _playwrightAPIDriver = playwrightAPIDriver;
            _productDbContext = productDbContext;
        }

        [Fact]
        public async Task PerformGetProductByIdTestAsync()
        {

            var response = await _playwrightAPIDriver.APIRequestContext.GetAsync("/Product/GetProductById/1");

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
                //product.Value.GetRawText().Should().Be("Keyboard");

                product.Name.Should().Be("Keyboard");
                product.description.Should().Be("Gaming Keyboard with lights");
            }
        }

        [Fact]
        public async Task PerformGetProductsTestAsync()
        {
            //3. Perform Get Request
            var response = await _playwrightAPIDriver.APIRequestContext.GetAsync("/Product/GetProducts");

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
            var body = new Product(0, "DemoProductForDuplication", "Demo Description", 123, 3);


            //3. Perform POST Request
            var response = await _playwrightAPIDriver.APIRequestContext.PostAsync("/Product/Create", new() { DataObject = body });

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

                product.Name.Should().Be("DemoProductForDuplication");
                product.description.Should().Be("Demo Description");
            }
        }


        [Fact]
        public async Task PerformPostProductsTestAsync()
        {
            var body = new List<Product>()
            {
                new Product(0, "DemoProduct2", "Demo Description2", 456, 3),
                new Product(0, "DemoProduct3", "Demo Description3", 654, 2),
                new Product(0, "DemoProduct4", "Demo Description4", 789, 1),
            };

            //3. Perform POST Request
            var response = await _playwrightAPIDriver.APIRequestContext.PostAsync("/Product/CreateProducts", new() { DataObject = body });

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

                products
                    .Should()
                    .BeEquivalentTo(body, x => x.Excluding(y => y.Id));

                products.Should().NotBeNull();
            }
        }


        [Fact]
        public async Task PerformDeleteProductsTestAsync()
        {
            //1. Ensure that the product exist
            var dbProduct = _productDbContext.Products.FirstOrDefault(x => x.Name == "DemoProduct3");

            if (dbProduct is not null)
            {
                var response = await _playwrightAPIDriver.APIRequestContext.DeleteAsync($"/Product/Delete/?id={dbProduct.Id}");

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

                    product.Name.Should().Be("DemoProduct3");
                    product.description.Should().Be("Demo Description3");
                }
            }
        }

    }



}
