using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Pronoodle.Products
{
    /// <summary>
    /// An in-memory implementation of the <see cref="IProductRepository"/> contract.
    /// </summary>
    public class InMemoryProductRepository : IProductRepository
    {
        const int StreamChunkSize = 2; // Must be > 0.
        static readonly TimeSpan StreamAllChunkDelay = TimeSpan.FromMilliseconds(20);

        static readonly TimeSpan AddOrUpdateDelay = TimeSpan.FromMilliseconds(200);

        readonly Dictionary<string, Product> _repository = new Dictionary<string, Product>();
        readonly Subject<IEnumerable<Product>> _subject = new Subject<IEnumerable<Product>>();
        readonly object _syncRoot = new object();

        /// <summary>
        /// Streams all the existing as well as future products (both added and modified).
        /// </summary>
        /// <returns>A stream of products.</returns>
        public IObservable<IEnumerable<Product>> StreamAll()
        {
            // Stream existing products.
            return Observable
                .Create<IEnumerable<Product>>(observer =>
                {
                    // We need to synchronize both read and write acccess to the dictionary as well
                    // as write access to subject to enable parallel concurrency.
                    lock (_syncRoot)
                    // Stream in chunks.
                    foreach (var chunk in Chunkify(_repository.Values))
                        observer.OnNext(chunk);

                    observer.OnCompleted();
                    return Disposable.Empty;
                })
                // Start streaming new/updated products.
                .Concat(_subject)
                // Simulate slow loading.
                .Select(products => Observable.Timer(StreamAllChunkDelay).Select(_ => products))
                .Concat();
        }

        /// <summary>
        /// Adds new or updates existing products (in case products with same key exist).
        /// </summary>
        /// <param name="products">Products to add or update.</param>
        /// <returns>A task signaling the completion of this async operation.</returns>
        public async Task AddOrUpdate(IEnumerable<Product> products)
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            lock (_syncRoot)
            {
                foreach (Product product in products)
                    _repository[product.Key] = product;

                foreach (var chunk in Chunkify(products))
                    _subject.OnNext(chunk);
            }

            // Simulate slow processing.
            await Task.Delay(AddOrUpdateDelay);
        }

        // TODO: A good candidate for generalisation and moving to a util/extension.
        IEnumerable<List<Product>> Chunkify(IEnumerable<Product> products)
        {
            var chunk = new List<Product>(StreamChunkSize);

            foreach (var product in products)
            {
                chunk.Add(product);
                if (chunk.Count == StreamChunkSize)
                {
                    yield return chunk;
                    chunk = new List<Product>(StreamChunkSize);
                }
            }

            // Send last chunk if partial.
            if (chunk.Count > 0) yield return chunk;
        }
    }
}
