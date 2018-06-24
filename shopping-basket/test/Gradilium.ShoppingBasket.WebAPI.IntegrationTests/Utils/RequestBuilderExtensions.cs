using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gradilium.ShoppingBasket.WebAPI.IntegrationTests.Utils
{
    static class RequestBuilderExtensions
    {
        public static RequestBuilder WithJsonBody(this RequestBuilder builder, object value) =>
            builder.And(req => req.Content = new StringContent(
                JsonConvert.SerializeObject(value),
                Encoding.UTF8,
                "application/json"));

        public static Task<HttpResponseMessage> DeleteAsync(this RequestBuilder builder) =>
            builder.SendAsync("DELETE");
    }
}
