namespace CalculatorApp.Models
{
    using System;
    using MediatR;

    public class MathEquationResult : INotification
    {
        public MathEquationResult(MathEquation t)
        {
            if (t == null)
                throw new InvalidOperationException();

            this.Expression = t.Expression;
            this.Result = t.Result;
            this.Created = t.Created;
            this.Calculated = t.LastCalculated.Value;

            Console.WriteLine(t.Created);
            Console.WriteLine(t.LastCalculated);
        }

        public string Expression { get; private set; }
        public decimal? Result { get; private set; }
        public DateTimeOffset Created { get; private set; }
        public DateTimeOffset Calculated {get; private set; }

        public bool IsNewCalculation { get { return Created == Calculated; } }
    }
}