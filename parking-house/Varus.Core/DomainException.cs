using System;

namespace Varus.Core
{
    public abstract class DomainException : Exception
    {
        public Guid Id { get; set; }
    }
}
