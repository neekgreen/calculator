namespace CalculatorApp.Features
{
    using Microsoft.Extensions.Caching.Memory;

    public interface ICachableRequest
    {
        bool IsCachable { get;set; }
        string GetCacheKey();
        MemoryCacheEntryOptions GetCacheOptions();
    }
}