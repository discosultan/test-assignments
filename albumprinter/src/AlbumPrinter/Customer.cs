using System;
using System.Collections.Generic;

namespace AlbumPrinter
{
    public class Customer : Entity
    {
        private string _name; // Not null.
        private string _email; // Not null.

        /// <summary>
        /// Initializes a new instance of a <see cref="Customer"/> entity.
        /// </summary>
        /// <param name="name">The customer name.</param>
        /// <param name="email">The customer email.</param>
        /// <exception cref="ArgumentNullException">Any input is <c>null</c>.</exception>
        public Customer(string name, string email)
        {
            Name = name;
            Email = email;
        }

        private Customer() { }

        // TODO: We could get rid of the throw expressions once we declare the params
        // non-nullable in C# 8.
        // TODO: Disallow 0 length names and potentially add additional constraints.
        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(value));
        }

        // TODO: Match email pattern.
        public string Email
        {
            get => _email;
            set => _email = value ?? throw new ArgumentNullException(nameof(value));
        }

        public IReadOnlyList<Order> Orders { get; } = new List<Order>();
    }
}
