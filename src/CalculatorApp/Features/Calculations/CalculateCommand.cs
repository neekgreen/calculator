namespace CalculatorApp.Features.Calculators
{
    using CalculatorApp.Models;
    using MediatR;
    using Microsoft.Extensions.Caching.Memory;

    public class CalculateCommand : IRequest<Calculation>, ICachableRequest
    {
        public string Expression { get; set; }
        public bool IsCachable { get;set; }


        string ICachableRequest.GetCacheKey()
        {
            return string.Format("{0}:{1}", this.GetType().Name, Expression);
        }

        MemoryCacheEntryOptions ICachableRequest.GetCacheOptions()
        {
            return new MemoryCacheEntryOptions();
        }
    }
}