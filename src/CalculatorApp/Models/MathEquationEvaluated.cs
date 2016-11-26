namespace CalculatorApp.Models
{
    using MediatR;
    
    public class MathEquationEvaluated : IDomainEvent, INotification
    {
        public MathEquationEvaluated(MathEquation context)
        {
            this.Context = context;
        }

        public MathEquation Context { get; private set; }
    }
}