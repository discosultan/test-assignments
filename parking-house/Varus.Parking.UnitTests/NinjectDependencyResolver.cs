using System;
using Ninject;
using Varus.Core;

namespace Varus.Parking.UnitTests
{
    class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
        }

        public object GetObject(Type type)
        {
            return _kernel.Get(type);
        }
    }
}
