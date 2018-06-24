using System;

namespace Gradilium.ShoppingBasket.Common
{
    public abstract class Entity
    {
        /// <summary>
        /// Gets the entity ID.
        /// </summary>
        public Guid Id { get; private set; } = Guid.NewGuid();

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Id.Equals(((Entity)obj).Id);
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
