namespace WebApi.Features
{
    using System.Reflection;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Options;
    using StructureMap;

    public class FeatureRegistry : Registry
    {
        public FeatureRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();

                scan.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                scan.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));

                scan.AddAllTypesOf(typeof(IValidator<>));

                For(typeof(IRequestHandler<,>))
                    .DecorateAllWith(typeof(
                        CachableRequestHandler<,>), 
                        (t) => typeof(ICachableRequestHandler).IsAssignableFrom(t.ReturnedType));

                For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
                For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));

                For<IMediator>().Use<Mediator>();
                For<IMemoryCache>().Use(()=> new MemoryCache(Options.Create(new MemoryCacheOptions()))).Singleton();
            });
        }
    }
}