using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pronoodle.Products
{
    /// <summary>
    /// A storage contract for <see cref="Product"/> models.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Streams all the existing as well as future products (both added and modified).
        /// </summary>
        /// <returns>A stream of products.</returns>
        IObservable<IEnumerable<Product>> StreamAll();

        /// <summary>
        /// Adds new or updates existing products (in case products with same key exist).
        /// </summary>
        /// <param name="products">Products to add or update.</param>
        /// <returns>A task signaling the completion of this async operation.</returns>
        Task AddOrUpdate(IEnumerable<Product> products);
    }
}
