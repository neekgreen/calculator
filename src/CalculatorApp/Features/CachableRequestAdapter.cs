namespace CalculatorApp.Features
{
    using System;
    using MediatR;
    using Microsoft.Extensions.Caching.Memory;

    public class CachableRequestAdapter<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IMemoryCache cache;

        private readonly IRequestHandler<TRequest, TResponse> innerHandler;

        public CachableRequestAdapter(IMemoryCache cache, IRequestHandler<TRequest, TResponse> innerHandler)
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
                    var absoluteExpiration = (message as ICachableRequestWithAbsoluteExpiration)?.GetAbsoluteExpiration();
                    var slidingExpiration = (message as ICachableRequestWithSlidingExpiration)?.GetSlidingExpiration();

                    if (absoluteExpiration.HasValue)
                        entry.AbsoluteExpiration = absoluteExpiration.Value;

                    if (slidingExpiration.HasValue)
                        entry.SlidingExpiration = slidingExpiration.Value;

                    return this.innerHandler.Handle(message);
                });
            }

            return this.innerHandler.Handle(message);
        }

        private TResponse GetNew(ICacheEntry ce)
        {
            return default(TResponse);
        }
    }
}