using System;

namespace Gradilium.ShoppingBasket.Baskets
{
    /// <summary>
    /// An value type representing a number of products in a shopping basket.
    /// </summary>
    public struct Item
    {
        /// <summary>
        /// Initializes a new instance of an <see cref="Item"/> entity.
        /// </summary>
        /// <param name="productId">The id of the type of product this item represents.</param>
        /// <param name="count">The number of items.</param>
        public Item(Guid productId, int count)
        {
            ProductId = productId;
            Count = count;
        }

        /// <summary>
        /// Gets the id of the type of product this item represents.
        /// </summary>
        public Guid ProductId { get; private set; }
        /// <summary>
        /// Gets the number of products in a basket.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Get the maximum number of items that can be added or removed.
        /// </summary>
        /// <returns>The resulting allowed change.</returns>
        public int GetMaxAllowedChange(int by, int max) => Math.Max(Math.Min(Count + by, max), 0) - Count;

        public static Item operator +(Item lhv, int rhv) => new Item(lhv.ProductId, lhv.Count + rhv);
        public static Item operator -(Item lhv, int rhv) => new Item(lhv.ProductId, lhv.Count - rhv);
    }
}
