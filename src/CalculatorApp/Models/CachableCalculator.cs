namespace CalculatorApp.Models
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Memory;

    public class CachableCalculator : ICalculator
    {
        private readonly IMemoryCache memoryCache;
        private readonly ICalculator calculator;

        public CachableCalculator(IMemoryCache memoryCache, ICalculator calculator)
        {
            this.memoryCache = memoryCache;
            this.calculator = calculator;
        }

        Task<decimal> ICalculator.Evaluate(string expression)
        {
            var cacheKey = expression;

            return memoryCache.GetOrCreate(cacheKey, entry =>
            {
                entry.SetSlidingExpiration(TimeSpan.FromHours(1));

                return this.calculator.Evaluate(expression);
            });
        }
    }
}