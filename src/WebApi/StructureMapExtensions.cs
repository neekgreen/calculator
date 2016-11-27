namespace WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using StructureMap;
    using StructureMap.Configuration.DSL.Expressions;
    using StructureMap.Graph;
    using StructureMap.Pipeline;

    /// The classes within this file are copied directly from github. I could not get the NuGet package
    /// to work properly, and I got tired of trying :(.
    ///
    /// https://github.com/structuremap/StructureMap.Microsoft.DependencyInjection
    internal class AspNetConstructorSelector : IConstructorSelector
    {
        // ASP.NET expects registered services to be considered when selecting a ctor, SM doesn't by default.
        public ConstructorInfo Find(Type pluggedType, DependencyCollection dependencies, PluginGraph graph) =>
            pluggedType.GetTypeInfo()
                .DeclaredConstructors
                .Select(ctor => new { Constructor = ctor, Parameters = ctor.GetParameters() })
                .Where(x => x.Parameters.All(param => graph.HasFamily(param.ParameterType)))
                .OrderByDescending(x => x.Parameters.Length)
                .Select(x => x.Constructor)
                .FirstOrDefault();
    }

    internal static class HelperExtensions
    {
        public static bool IsGenericEnumerable(this Type type)
        {
            return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        public static GenericFamilyExpression LifecycleIs(this GenericFamilyExpression instance, ServiceLifetime lifetime)
        {
            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    return instance.LifecycleIs(Lifecycles.Singleton);
                case ServiceLifetime.Scoped:
                    return instance.LifecycleIs(Lifecycles.Container);
                case ServiceLifetime.Transient:
                    return instance.LifecycleIs(Lifecycles.Unique);
                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }
        }

        public static bool HasFamily<TPlugin>(this PluginGraph graph)
        {
            return graph.HasFamily(typeof(TPlugin));
        }
    }
        public static class ContainerExtensions
    {
        /// <summary>
        /// Populates the container using the specified service descriptors.
        /// </summary>
        /// <remarks>
        /// This method should only be called once per container.
        /// </remarks>
        /// <param name="container">The container.</param>
        /// <param name="descriptors">The service descriptors.</param>
        public static void Populate(this IContainer container, IEnumerable<ServiceDescriptor> descriptors)
        {
            container.Configure(config => config.Populate(descriptors));
        }

        /// <summary>
        /// Populates the container using the specified service descriptors.
        /// </summary>
        /// <remarks>
        /// This method should only be called once per container.
        /// </remarks>
        /// <param name="config">The configuration.</param>
        /// <param name="descriptors">The service descriptors.</param>
        public static void Populate(this ConfigurationExpression config, IEnumerable<ServiceDescriptor> descriptors)
        {
            Populate((Registry) config, descriptors);
        }

        /// <summary>
        /// Populates the registry using the specified service descriptors.
        /// </summary>
        /// <remarks>
        /// This method should only be called once per container.
        /// </remarks>
        /// <param name="registry">The registry.</param>
        /// <param name="descriptors">The service descriptors.</param>
        public static void Populate(this Registry registry, IEnumerable<ServiceDescriptor> descriptors)
        {
            // HACK: We insert this action in order to prevent Populate being called twice on the same container.
            registry.Configure(ThrowIfMarkerInterfaceIsRegistered);

            registry.For<IMarkerInterface>();

            registry.Policies.ConstructorSelector<AspNetConstructorSelector>();

            registry.For<IServiceProvider>()
                .LifecycleIs(Lifecycles.Container)
                .Use<StructureMapServiceProvider>();

            registry.For<IServiceScopeFactory>()
                .LifecycleIs(Lifecycles.Container)
                .Use<StructureMapServiceScopeFactory>();

            registry.Register(descriptors);
        }

        private static void ThrowIfMarkerInterfaceIsRegistered(PluginGraph graph)
        {
            if (graph.HasFamily<IMarkerInterface>())
            {
                throw new InvalidOperationException("Populate should only be called once per container.");
            }
        }

        private static void Register(this IProfileRegistry registry, IEnumerable<ServiceDescriptor> descriptors)
        {
            foreach (var descriptor in descriptors)
            {
                registry.Register(descriptor);
            }
        }

        private static void Register(this IProfileRegistry registry, ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationType != null)
            {
                registry.For(descriptor.ServiceType)
                    .LifecycleIs(descriptor.Lifetime)
                    .Use(descriptor.ImplementationType);

                return;
            }

            if (descriptor.ImplementationFactory != null)
            {
                registry.For(descriptor.ServiceType)
                    .LifecycleIs(descriptor.Lifetime)
                    .Use(descriptor.CreateFactory());

                return;
            }

            registry.For(descriptor.ServiceType)
                .LifecycleIs(descriptor.Lifetime)
                .Use(descriptor.ImplementationInstance);
        }

        private static Expression<Func<IContext, object>> CreateFactory(this ServiceDescriptor descriptor)
        {
            return context => descriptor.ImplementationFactory(context.GetInstance<IServiceProvider>());
        }

        private interface IMarkerInterface { }
    }

    public sealed class StructureMapServiceProvider : IServiceProvider, ISupportRequiredService
    {
        public StructureMapServiceProvider(IContainer container)
        {
            Container = container;
        }

        private IContainer Container { get; }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsGenericEnumerable())
            {
                // Ideally we'd like to call TryGetInstance here as well,
                // but StructureMap does't like it for some weird reason.
                return GetRequiredService(serviceType);
            }

            return Container.TryGetInstance(serviceType);
        }

        public object GetRequiredService(Type serviceType)
        {
            return Container.GetInstance(serviceType);
        }
    }


    internal sealed class StructureMapServiceScopeFactory : IServiceScopeFactory
    {
        public StructureMapServiceScopeFactory(IContainer container)
        {
            Container = container;
        }

        private IContainer Container { get; }

        public IServiceScope CreateScope()
        {
            return new StructureMapServiceScope(Container.GetNestedContainer());
        }

        private class StructureMapServiceScope : IServiceScope
        {
            public StructureMapServiceScope(IContainer container)
            {
                Container = container;
                ServiceProvider = container.GetInstance<IServiceProvider>();
            }

            private IContainer Container { get; }

            public IServiceProvider ServiceProvider { get; }

            public void Dispose() => Container.Dispose();
        }
    }
}