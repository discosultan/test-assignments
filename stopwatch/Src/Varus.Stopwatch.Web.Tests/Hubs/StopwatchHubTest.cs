using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Owin.Hosting;
using Varus.Stopwatch.Web.Hubs;
using Xunit;

namespace Varus.Stopwatch.Web.Tests.Hubs
{
    [Collection("stopwatch")]
    public class StopwatchHubTest : IClassFixture<StopwatchHubTest.Context>, IDisposable
    {
        [Fact]
        public async Task GetAsync_UnauthorizedWithoutAuth()
        {
            await _connection.Start();
            await Assert.ThrowsAsync(
                typeof(InvalidOperationException),
                () => _proxy.Invoke("GetAsync", "testuser"));
        }

        [Fact]
        public async Task GetAsync_OKWithBasicAuth()
        {
            _connection.Headers.Add("Authorization", BasicAuth);
            await _connection.Start();
            await _proxy.Invoke("GetAsync", "testuser");
        }

        [Fact]
        public async Task GetAsync_OKWithAPIKeyAuth()
        {
            _connection.Headers.Add("Authorization", APIKeyAuth);
            await _connection.Start();
            await _proxy.Invoke("GetAsync", "testuser");
        }


        [Fact]
        public async Task PostAsync_UnauthorizedWithoutAuth()
        {
            await _connection.Start();
            await Assert.ThrowsAsync(
                typeof(InvalidOperationException),
                () => _proxy.Invoke("PostAsync", "testname"));
        }

        [Fact]
        public async Task PostAsync_UnauthorizedWithAPIKeyAuth()
        {
            _connection.Headers.Add("Authorization", APIKeyAuth);
            await _connection.Start();
            await Assert.ThrowsAsync(
                typeof(InvalidOperationException),
                () => _proxy.Invoke("PostAsync", "testname"));
        }

        [Fact]
        public async Task PostAsync_BadRequestWithoutNameArg()
        {
            _connection.Headers.Add("Authorization", BasicAuth);
            await _connection.Start();
            await Assert.ThrowsAsync(
                typeof(InvalidOperationException),
                () => _proxy.Invoke("PostAsync"));
        }

        [Fact]
        public async Task PostAsync_OKWithBasicAuth()
        {
            _connection.Headers.Add("Authorization", BasicAuth);
            await _connection.Start();
            await _proxy.Invoke("PostAsync", "testname");
        }


        [Fact]
        public async Task StartStopwatchAndGetElapsedTimePeriodically()
        {
            const int expectedReceiveCount = 3;
            const int timeout = 10000;

            _connection.Headers.Add("Authorization", BasicAuth);
            await _connection.Start();

            var countdownEvent = new CountdownEvent(expectedReceiveCount);

            long expectedElapsedMS = 1;
            _proxy.On<Dictionary<string, long>>(nameof(IStopwatchHubClient.BroadcastElapsed), values =>
            {
                Assert.Equal(expectedElapsedMS++, values["testname"]);
                countdownEvent.Signal();
            });

            await _proxy.Invoke("PostAsync", "testname");
            await _proxy.Invoke("GetAsync", "testuser");

            for (int i = 0; i < expectedReceiveCount; i++)
                Subject.OnNext(Unit.Default);

            Assert.True(countdownEvent.Wait(timeout));
        }


        private const string BasicAuth = "Basic dGVzdHVzZXI6dGVzdHVzZXI="; // testuser:testuser
        private const string APIKeyAuth = "API-Key test";
        public static Subject<Unit> Subject { get; } = new Subject<Unit>();

        private readonly HubConnection _connection;
        private readonly IHubProxy _proxy;

        public StopwatchHubTest(Context ctx)
        {
            _connection = new HubConnection(ctx.Url);
            _proxy = _connection.CreateHubProxy(nameof(StopwatchHub));
        }

        public void Dispose() => _connection.Dispose();

        public class Context : IDisposable
        {
            private readonly IDisposable _server;
            public Context() => _server = WebApp.Start<TestsStartup>(Url);
            public string Url { get; } = "http://localhost:8675";
            public void Dispose() => _server.Dispose();
        }
    }
}
