namespace CalculatorApp.Models
{
    using System;

    public class Calculation : Entity
    {
        public Calculation(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new ArgumentOutOfRangeException("expression");
                
            this.Expression = expression;
        }

        private Calculation()
        {

        }

        public string Expression { get; private set; }
        public long? Result { get; private set; }


        public void Calculate(ICalculator calculator, bool recalculate = false)
        {
            if (Result == null || recalculate)
            {
                this.Result = calculator.Evaluate(this.Expression);
                OnUpdated(new CalculateCompleted(this));
            }
        }
    }
}