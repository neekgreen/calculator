namespace CalculatorApp.Models
{
    using System;

    public class Calculation : Entity
    {
        public Calculation(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new ArgumentNullException(nameof(expression));
                
            this.Expression = expression;
        }

        private Calculation()
        {

        }

        public string Expression { get; private set; }
        public long? Result { get; private set; }


        public void Calculate(ICalculator calculator, bool recalculate = false)
        {
            if (calculator == null)
                throw new ArgumentNullException(nameof(calculator));

            if (Result == null || recalculate)
            {
                this.Result = calculator.Evaluate(this.Expression);
                OnUpdated(new CalculateCompleted(this));
            }
        }
    }
}