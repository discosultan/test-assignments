using System;
using System.Net.Http.Headers;
using System.Reactive;
using System.Reactive.Linq;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using Varus.Stopwatch.AzureTableStorage;
using Varus.Stopwatch.Repositories;
using Varus.Stopwatch.Services;
using Varus.Stopwatch.Web.Infrastructure;
using Varus.Stopwatch.Web.Infrastructure.SignalR;
using Varus.Stopwatch.Web.Tests.Hubs;

namespace Varus.Stopwatch.Web.Tests
{
    public class TestsStartup
    {
        public void Configuration(IAppBuilder app)
        {
            // Setup authentication.
            app.UseBasicAuthentication("stopwatch", Authenticate.Basic);
            app.UseAPIKeyAuthentication(Authenticate.APIKey);

            // Build DI container. This is shared between Web API and SignalR.
            long initialTicks = DateTime.UtcNow.Ticks;
            var stopwatchService = new StopwatchService(
                new InMemoryTimestampRepository(), // Uncomment to use in-memory data storage.
                //new AzureTableStorageTimestampRepository(), // Uncomment to use Azure Table Storage.
                () => // Timer will advance 1ms each time it is queried.
                {
                    initialTicks += TimeSpan.TicksPerMillisecond;
                    return initialTicks;
                });
            var container = new Container();
            container.RegisterSingleton(typeof(IStopwatchService), stopwatchService);
            // That's the SignalR timer to broadcast elapsed time to user when subject requests.
            container.RegisterSingleton(typeof(IObservable<Unit>), StopwatchHubTest.Subject.AsObservable());

            // Setup Web API.
            var httpConfiguration = new HttpConfiguration();
            ConfigureWebApi(httpConfiguration, container);
            app.UseWebApi(httpConfiguration);

            // Setup SignalR.
            var hubConfiguration = new HubConfiguration();
            ConfigureSignalR(hubConfiguration, container);
            app.MapSignalR(hubConfiguration);
        }

        private static void ConfigureWebApi(HttpConfiguration config, Container container)
        {
            // Setup routing.
            config.MapHttpAttributeRoutes();

            // Make json as the default response format.
            // Xml is still supported if "text/xml" header is sent.
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // Setup DI container.
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void ConfigureSignalR(HubConfiguration hubConfiguration, Container container)
        {
            // Enable SignalR to return detailed messages when exceptions occur.
            hubConfiguration.EnableDetailedErrors = true;

            // Setup DI.
            hubConfiguration.Resolver = new SimpleInjectorSignalRDependencyResolver(container);
        }
    }
}
