using Gradilium.ShoppingBasket.Common;
using System.Collections.Generic;
using ProductId = System.Guid;

namespace Gradilium.ShoppingBasket.Baskets
{
    /// <summary>
    /// An aggregate root representing a shopping basket of a user.
    /// </summary>
    public class Basket : Entity
    {
        /// <summary>
        /// Gets a map of items in the basket.
        /// </summary>
        public Dictionary<ProductId, Item> Items { get; private set; } = new Dictionary<ProductId, Item>();
    }
}
