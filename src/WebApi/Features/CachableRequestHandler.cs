namespace WebApi.Features
{
    using MediatR;
    using Microsoft.Extensions.Caching.Memory;

    public class CachableRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IMemoryCache cache;

        private readonly IRequestHandler<TRequest, TResponse> innerHandler;

        public CachableRequestHandler(IMemoryCache cache, IRequestHandler<TRequest, TResponse> innerHandler)
        {
            this.cache = cache;
            this.innerHandler = innerHandler;
        }

        TResponse IRequestHandler<TRequest, TResponse>.Handle(TRequest message)
        {
            var cachableRequest = message as ICachableRequest;

            if (cachableRequest != null && cachableRequest.IsCachable)
            {
                var cacheKey = cachableRequest.GetCacheKey();

                return cache.GetOrCreate(cacheKey, entry =>
                {
                    entry.SetOptions(cachableRequest.GetCacheOptions());

                    return this.innerHandler.Handle(message);
                });
            }

            return this.innerHandler.Handle(message);
        }
    }
}