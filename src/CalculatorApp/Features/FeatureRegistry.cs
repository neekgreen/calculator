namespace CalculatorApp.Features
{
    using MediatR;
    using Microsoft.Extensions.Caching.Memory;
    using StructureMap;
    using FluentValidation;
    using System.Collections.Generic;
    using System.Reflection;
    using Microsoft.Extensions.Options;
    using Models;

    public class FeatureRegistry : Registry
    {
        public FeatureRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();

                scan.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                //scan.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<,>));
                scan.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                //scan.ConnectImplementationsToTypesClosing(typeof(IAsyncNotificationHandler<>));

                scan.AddAllTypesOf(typeof(IValidator<>));
                scan.AddAllTypesOf(typeof(IFeature));

                var handlerType = For(typeof(IRequestHandler<,>));

                //handlerType.DecorateAllWith(typeof(LoggingHandler<,>));
                handlerType.DecorateAllWith(typeof(CachableRequestHandler<,>), (t) => typeof(ICachableRequestHandler).IsAssignableFrom(t.ReturnedType)); //).Name.EndsWith("CommandHandler"));

                For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
                For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));

                For<IMediator>().Use<Mediator>();
                For<IMemoryCache>().Use(()=> new MemoryCache(Options.Create(new MemoryCacheOptions()))).Singleton();
                
                For<ICalculator>().DecorateAllWith<CachableCalculator>();
                For<ICalculator>().Use<Calculator>();

                For<IMathEquationResultRepository>().Use<MathEquationResultRepository>().Singleton();
            });
        }
    }
}