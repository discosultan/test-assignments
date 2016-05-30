using System;

namespace Varus.Core
{
    public abstract class Event
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
    }
}
