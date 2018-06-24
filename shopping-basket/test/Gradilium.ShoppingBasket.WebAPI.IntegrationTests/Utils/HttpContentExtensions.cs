using Gradilium.ShoppingBasket.WebAPI.Utils;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gradilium.ShoppingBasket.WebAPI.IntegrationTests.Utils
{   
    static class HttpContentExtensions
    {
        static readonly JsonSerializer JsonSerializer = new JsonSerializer
        {
            ContractResolver = new PrivateSetterContractResolver()
        };

        public static async Task<T> DeserializeFromJson<T>(this HttpContent content)
        {
            using (var streamReader = new StreamReader(await content.ReadAsStreamAsync()))
            using (var reader = new JsonTextReader(streamReader))
                return JsonSerializer.Deserialize<T>(reader);
        }
    }
}
