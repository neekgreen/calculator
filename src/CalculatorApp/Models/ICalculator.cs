namespace CalculatorApp.Models
{
    using System;
    using System.Threading.Tasks;
    
    public interface ICalculator
    {
        Task<CalculatorResult> Evaluate(string expression);
    }
}