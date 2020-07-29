using System;

namespace Adnuf
{
    public abstract class Entity
    {
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
