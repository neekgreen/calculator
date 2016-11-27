namespace CalculatorApp.Models
{
    using System;

    public class MathEquation : Entity
    {
        public MathEquation(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new ArgumentOutOfRangeException(nameof(expression));
                
            this.Expression = expression;

            OnCreated();
        }

        private MathEquation()
        {

        }

        public string Expression { get; private set; }
        public decimal? Result { get; private set; }
        public DateTimeOffset? LastCalculated { get; set; }


        public void Evaluate(ICalculator calculator)
        {
            if (calculator == null)
                throw new ArgumentNullException(nameof(calculator));

            var result = calculator.Evaluate(this.Expression).Result;

            this.Result = result?.Result;
            this.LastCalculated = result?.Created;

            OnUpdated(new MathEquationEvaluated(this));
        }
    }
}