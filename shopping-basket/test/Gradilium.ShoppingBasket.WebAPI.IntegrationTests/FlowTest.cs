using Gradilium.ShoppingBasket.Baskets;
using Gradilium.ShoppingBasket.Products;
using Gradilium.ShoppingBasket.WebAPI.IntegrationTests.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Gradilium.ShoppingBasket.WebAPI.IntegrationTests
{
    public class FlowTest
    {
        [Fact]
        public async Task AddProduct_AddToBasket_RemoveFromBasket_GetBasket()
        {
            // If we were accessing external services, we would use a library like Moq to mock them away.
            // We want to exercise the server and repository that are part of this microservice, though.

            var userId = Guid.NewGuid();
            var product = new Product("name", "manufacturer", 1.0m, 2);
            var expectedBasketIems = new Dictionary<Guid, Item>
            {
                [product.Id] = new Item(product.Id, 1)
            };

            var builder = new WebHostBuilder()
                .UseStartup<Startup>();

            using (var server = new TestServer(builder))
            {
                var response = await server
                    .CreateRequest("product")
                    .WithJsonBody(product)
                    .PostAsync();
                response.EnsureSuccessStatusCode();

                response = await server
                    .CreateRequest($"basket/{userId}")
                    .WithJsonBody(new Item(product.Id, 2))
                    .PostAsync();
                response.EnsureSuccessStatusCode();

                response = await server
                    .CreateRequest($"basket/{userId}")
                    .WithJsonBody(new Item(product.Id, 1))
                    .DeleteAsync();
                response.EnsureSuccessStatusCode();

                response = await server
                    .CreateRequest($"basket/{userId}")
                    .GetAsync();
                response.EnsureSuccessStatusCode();

                var basket = await response.Content.DeserializeFromJson<Basket>();

                Assert.Equal(expectedBasketIems, basket.Items);
            }
        }
    }
}
