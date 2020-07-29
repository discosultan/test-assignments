using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Adnuf.WebAPI.IntegrationTests.Utils
{
    public class MockHttpMessageHandler : DelegatingHandler
    {
        readonly Queue<string> responses;

        public MockHttpMessageHandler(IEnumerable<string> responses)
        {
            this.responses = new Queue<string>(responses);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responses.Dequeue())
            };
            return Task.FromResult(responseMessage);
        }
    }
}
