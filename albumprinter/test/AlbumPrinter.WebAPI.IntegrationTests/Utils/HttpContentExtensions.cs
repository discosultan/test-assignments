using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlbumPrinter.WebAPI.IntegrationTests.Utils
{
    static class HttpContentExtensions
    {
        static readonly JsonSerializer JsonSerializer = JsonSerializer.CreateDefault();

        public static async Task<T> DeserializeJson<T>(this HttpContent content)
        {
            using (var streamReader = new StreamReader(await content.ReadAsStreamAsync()))
            using (var reader = new JsonTextReader(streamReader))
                return JsonSerializer.Deserialize<T>(reader);
        }
    }
}
