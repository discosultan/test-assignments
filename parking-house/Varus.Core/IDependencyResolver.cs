using System;

namespace Varus.Core
{
    /// <summary>
    /// Abstraction for an IoC container.
    /// </summary>
    public interface IDependencyResolver
    {        
        object GetObject(Type type);
    }
}
