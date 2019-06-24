using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlbumPrinter.WebAPI.IntegrationTests.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace AlbumPrinter.WebAPI.IntegrationTests
{
    public class CustomersControllerTest
    {
        // TODO: Test for 400 and 404 response codes.

        [Fact]
        public async Task AddCustomer_AddOrder_GetCustomers_GetCustomer()
        {
            var customer = new Customer(
                name: "Ilkin Yawen",
                email: "iyawen@madeup.com");
            var order = new Order(
                price: 1.5m,
                createdDate: DateTime.UtcNow);

            using (var server = new TestServer(new WebHostBuilder().UseStartup<Startup>()))
            {
                var response = await server
                    .CreateRequest("customers")
                    .WithJsonBody(customer)
                    .PostAsync();
                response.EnsureSuccessStatusCode();

                response = await server
                    .CreateRequest($"customers/{customer.Id}/orders")
                    .WithJsonBody(order)
                    .PostAsync();
                response.EnsureSuccessStatusCode();

                response = await server
                    .CreateRequest("customers")
                    .GetAsync();
                response.EnsureSuccessStatusCode();
                var y = await response.Content.DeserializeJson<List<Customer>>();
                AssertExtensions.JsonEqual(
                    new[] { customer },
                    y);

                response = await server
                    .CreateRequest($"customers/{customer.Id}")
                    .GetAsync();
                response.EnsureSuccessStatusCode();
                var x = await response.Content.DeserializeJson<Customer>();
                AssertExtensions.JsonEqual(
                    customer,
                    x);
            }
        }
    }
}
