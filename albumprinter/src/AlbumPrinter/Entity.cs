using System;

namespace AlbumPrinter
{
    public abstract class Entity
    {
        /// <summary>
        /// Gets the entity ID.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Id.Equals(((Entity)obj).Id);
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
