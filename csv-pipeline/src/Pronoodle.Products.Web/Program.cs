using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Pronoodle.Products.Web
{
    public class Program
    {
        public static void Main() => new WebHostBuilder()
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureServices(services => services
                .AddSingleton<IProductRepository, InMemoryProductRepository>()
                .AddSingleton<ProductHub>()
            )
            .Configure(app => app
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseWebSockets()
                .Use(ProcessWebSocketRequest)
            )
            .Build()
            .Run();

        public static async Task ProcessWebSocketRequest(HttpContext ctx, Func<Task> next)
        {
            if (!ctx.WebSockets.IsWebSocketRequest)
            {
                await next();
                return;
            }

            var productHub = ctx.RequestServices.GetService<ProductHub>();
            var ws = await ctx.WebSockets.AcceptWebSocketAsync();

            // Manual routing.
            switch (ctx.Request.Path.Value.ToLowerInvariant())
            {
                case "/upload":
                    await productHub.Upload(ws);
                    break;
                case "/overview":
                    await productHub.Overview(ws);
                    break;
                default:
                    ctx.Response.StatusCode = 400;
                    break;
            }
        }
    }
}
