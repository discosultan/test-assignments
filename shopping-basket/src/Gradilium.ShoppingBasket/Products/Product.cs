using Gradilium.ShoppingBasket.Common;
using System;

namespace Gradilium.ShoppingBasket.Products
{
    /// <summary>
    /// An aggregate root representing a type of product.
    /// </summary>
    public class Product : Entity
    {
        public const int MaxCount = 999;

        /// <summary>
        /// Initializes a new instance of a <see cref="Product"/> entity.
        /// </summary>
        /// <param name="name">The display name translation key.</param>
        /// <param name="manufacturer">The manufacturer entity.</param>
        /// <param name="price">The product price.</param>
        /// <param name="count">Number of products available.</param>
        /// <exception cref="ArgumentNullException">Any input is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="price"/> is negative.</exception>
        public Product(string name, string manufacturer, decimal price, int count)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Manufacturer = manufacturer ?? throw new ArgumentNullException(nameof(manufacturer));

            if (price < 0.0m) throw new InvalidOperationException($"{nameof(price)} cannot be negative.");
            Price = price;

            if (count < 0) throw new InvalidOperationException($"{nameof(count)} cannot be negative.");
            Count = count;
        }

        // TODO: To simplify, this is just a display name. With i18n this would contain a translation key.
        /// <summary>
        /// Gets the display name translation key.
        /// </summary>
        public string Name { get; private set; }
        // TODO: To simplify, this is just a display name. Ideally we would reference to a manufacturer entity.
        /// <summary>
        /// Gets the product manufacturer entity.
        /// </summary>
        public string Manufacturer { get; private set; }
        // TODO: In real life, pricing would most probably be considered separately because we might sell
        // fractions of stock with different prices. There are also promotions, etc.
        /// <summary>
        /// Gets the product price.
        /// </summary>
        public decimal Price { get; private set; }
        /// <summary>
        /// Gets the number of products available.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Updates stock by specified amount. Only as much as possible will be added or removed.
        /// </summary>
        /// <param name="by">Requested number of items to add or remove.</param>
        /// <returns>Number of items actually added or removed.</returns>
        public int UpdateStock(int by)
        {
            by = Math.Max(Math.Min(Count + by, MaxCount), 0) - Count;
            Count += by;
            return by;
        }
    }
}
