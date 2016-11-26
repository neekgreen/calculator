namespace CalculatorApp.Features.MathEquations
{
    using System;
    using MediatR;
    using Microsoft.Extensions.Caching.Memory;
    using CalculatorApp.Models;

    public class EvaluateCommand : IRequest<MathEquationResult>, ICachableRequest
    {
        public string Expression { get; set; }

        public bool IsCachable { get;set; }

        string ICachableRequest.GetCacheKey()
        {
            return string.Format("{0}:{1}", this.GetType().Name, Expression);
        }

        MemoryCacheEntryOptions ICachableRequest.GetCacheOptions()
        {
            return 
                new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1));
        }
    }
}