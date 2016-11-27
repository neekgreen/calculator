namespace CalculatorApp.Models
{
    using System;

    public class CalculatorResult
    {
        public CalculatorResult(string expression, decimal result)
        {
            this.Expression = expression;
            this.Result = result;
            this.Created = DateTimeOffset.Now;
        }

        public string Expression { get; private set; }
        public decimal Result { get; private set; }
        public DateTimeOffset Created { get; private set; }
    }
}