using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using SimpleInjector;

namespace Varus.Stopwatch.Web.Infrastructure.SignalR
{
    public class SimpleInjectorSignalRDependencyResolver : DefaultDependencyResolver
    {
        private readonly Container _container;

        public SimpleInjectorSignalRDependencyResolver(Container container) => _container = container;

        public override object GetService(Type type)
            => IsRegistered(type) ? _container.GetInstance(type) : base.GetService(type);

        public override IEnumerable<object> GetServices(Type type)
            => IsRegistered(type) ? _container.GetAllInstances(type) : base.GetServices(type);

        private bool IsRegistered(Type type) => _container.GetRegistration(type) != null;
    }
}