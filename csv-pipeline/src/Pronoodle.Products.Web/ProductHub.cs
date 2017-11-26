using System;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pronoodle.Products.Web
{
    public class ProductHub
    {
        readonly IProductRepository _repository;

        public ProductHub(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task Upload(WebSocket ws)
        {
            var isFirst = true;

            for(;;)
            {
                var recMsg = await ws.Receive<string>();
                if (recMsg == null) break;

                var batchResult = ParseProduct.FromCsv(recMsg.Data, hasHeader: isFirst);
                await _repository.AddOrUpdate(batchResult.Successes);

                await ws.Send(batchResult);

                isFirst = false;
            }

            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
        }

        public async Task Overview(WebSocket ws)
        {
            var subscription = _repository.StreamAll()
                .Select(products => Observable.FromAsync(() => ws.Send(products)))
                .Concat()
                .Subscribe();

            for (;;)
            {
                var recMsg = await ws.Receive<object>();
                if (recMsg == null) break;
            }

            subscription.Dispose();
        }
    }
}
