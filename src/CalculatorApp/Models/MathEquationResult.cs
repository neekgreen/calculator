namespace CalculatorApp.Models
{
    using System;
    using MediatR;

    public class MathEquationResult : INotification
    {
        public MathEquationResult(MathEquation t)
        {
            if (!t.IsEvaluated)
                throw new InvalidOperationException();

            this.Expression = t.Expression;
            this.Result = t.Result.Value;
            this.Created = t.Created;
        }

        public string Expression { get; private set; }
        public decimal Result { get; private set; }
        public DateTimeOffset Created { get; private set; }
    }
}