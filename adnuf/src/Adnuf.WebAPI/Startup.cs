using Adnuf.Clients;
using Adnuf.Housing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Adnuf.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            // We use a singleton rate limiter for shared rate limiting across all clients within
            // the process.
            services.AddSingleton(FundaClient.ConstructRateLimiter());
            services.AddHttpClient<FundaClient>(client =>
            {
                client.BaseAddress = FundaClient.ConstructBaseUri(
                    Configuration.GetValue<string>("token")
                        ?? throw new ArgumentNullException("Please configure Funda token."));
            });
            services.AddTransient<IAgentRepository, FundaAgentRepository>();
            services.AddSingleton<IMemoryCache, MemoryCache>();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
