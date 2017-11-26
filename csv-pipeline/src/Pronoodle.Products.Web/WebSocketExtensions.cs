using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pronoodle.Products.Web
{
    public static class WebSocketExtensions
    {
        static readonly JsonSerializerSettings JsonConfig = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static async Task Send<T>(this WebSocket ws, T data)
        {
            var msg = new Message<T> { Data = data };
            string json = JsonConvert.SerializeObject(msg, JsonConfig);

            // TODO: Prevent allocation using a shared buffer (per socket connection).
            var bytes = Encoding.UTF8.GetBytes(json);

            await ws.SendAsync(
                new ArraySegment<byte>(bytes, 0, bytes.Length),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None);
        }

        public static async Task<Message<T>> Receive<T>(this WebSocket ws)
        {
            WebSocketReceiveResult result;
            var length = 0;

            // TODO: Prevent allocation using a shared buffer (per socket connection).
            var buffer = new byte[1024 * 4];

            do
            {
                result = await ws.ReceiveAsync(
                    new ArraySegment<byte>(buffer, length, buffer.Length - length),
                    CancellationToken.None);
                length += result.Count;
            }
            while (!result.EndOfMessage);

            if (result.CloseStatus.HasValue)
            {
                return null;
            }
            else
            {
                var json = Encoding.UTF8.GetString(buffer, 0, length);
                return JsonConvert.DeserializeObject<Message<T>>(json);
            }
        }
    }
}
