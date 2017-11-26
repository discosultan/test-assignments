using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Pronoodle.Products.Web;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Pronoodle.Products.Web.Program;

namespace Pronoodle.Products.Integration
{
    public class ProductHubTest
    {
        [Fact]
        public async Task Upload()
        {
            var productRepo = new InMemoryProductRepository();

            var builder = new WebHostBuilder()
                .ConfigureServices(services => services
                    .AddSingleton<IProductRepository>(productRepo)
                    .AddSingleton<ProductHub>()
                )
                .Configure(app => app
                    .UseWebSockets()
                    .Use(ProcessWebSocketRequest)
                );

            using (var server = new TestServer(builder))
            {
                var client = server.CreateWebSocketClient();
                var ws = await client.ConnectAsync(new Uri("ws://localhost/upload"), CancellationToken.None);

                await ws.Send(
@"Key,Artikelcode,colorcode,description,price,discountprice,delivered in,q1,size,color
00000002groe56,2,broek,Gaastra,8.00,,1-3 werkdagen,baby,56,groen"
                );
                var msg = await ws.Receive<BatchResult<Product, string>>();
                var result = msg.Data;

                Assert.Single(result.Successes);
                Assert.Empty(result.Errors);

                var products = new List<Product>();
                var signal = new ManualResetEvent(false);
                productRepo.StreamAll().Subscribe(p =>
                {
                    products.AddRange(p);
                    signal.Set();
                });

                signal.WaitOne();
                var product = products[0];

                Assert.Equal("00000002groe56", product.Key);
                Assert.Equal("2", product.ArticleCode);
                Assert.Equal("broek", product.ColorCode);
                Assert.Equal("Gaastra", product.Description);
                Assert.Equal(8.00m, product.Price);
                Assert.Null(product.DiscountPrice);
                Assert.Equal("1-3 werkdagen", product.DeliveredIn);
                Assert.Equal("baby", product.Q1);
                Assert.Equal(56, product.Size);
                Assert.Equal("groen", product.Color);
            }
        }

        [Fact]
        public async Task Overview()
        {
            var productRepo = new InMemoryProductRepository();
            await productRepo.AddOrUpdate(new[] { new Product { Key = "test1" } });

            var builder = new WebHostBuilder()
                .ConfigureServices(services => services
                    .AddSingleton<IProductRepository>(productRepo)
                    .AddSingleton<ProductHub>()
                )
                .Configure(app => app
                    .UseWebSockets()
                    .Use(ProcessWebSocketRequest)
                );

            using (var server = new TestServer(builder))
            {
                var client = server.CreateWebSocketClient();
                var ws = await client.ConnectAsync(new Uri("ws://localhost/overview"), CancellationToken.None);

                var msg = await ws.Receive<List<Product>>();

                Assert.Equal("test1", msg.Data[0].Key);

                await productRepo.AddOrUpdate(new[] { new Product { Key = "test2" } });
                msg = await ws.Receive<List<Product>>();

                Assert.Equal("test2", msg.Data[0].Key);
            }
        }
    }
}
