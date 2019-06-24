using System;

namespace AlbumPrinter
{
    public class Order : Entity
    {
        private decimal _price;

        /// <summary>
        /// Initializes a new instance of an <see cref="Order"/> entity.
        /// </summary>
        /// <param name="price">The order price.</param>
        /// <param name="createdDate">The date and time the order was created.</param>
        /// <exception cref="ArgumentException"><paramref name="price"/> is negative.</exception>
        public Order(decimal price, DateTime createdDate)
        {
            Price = price;
            CreatedDate = createdDate;
        }

        private Order() { }

        public decimal Price
        {
            get => _price;
            set
            {
                if (value < 0m)
                    throw new ArgumentException("Price cannot be negative.", nameof(value));
                _price = value;
            }
        }

        public DateTime CreatedDate { get; set; }

        public Guid CustomerID { get; set; }

        public Customer Customer { get; set; }
    }
}
