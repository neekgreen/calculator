namespace CalculatorApp.Models
{
    using System;
    using System.Threading.Tasks;

    public class MathEquation : Entity
    {
        public MathEquation(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new ArgumentOutOfRangeException(nameof(expression));
                
            this.Expression = expression;
        }

        private MathEquation()
        {

        }

        public string Expression { get; private set; }
        public decimal? Result { get; private set; }
        public bool IsEvaluated { get { return Result.HasValue; } }


        public void Evaluate(ICalculator calculator, bool recalculate = false)
        {
            if (calculator == null)
                throw new ArgumentNullException(nameof(calculator));

            if (Result == null || recalculate)
            {
                this.Result = calculator.Evaluate(this.Expression).Result;
                OnUpdated(new MathEquationEvaluated(this));
            }
        }
    }
}