namespace CalculatorApp.Models
{
    using MediatR;
    
    public class CalculateCompleted : IDomainEvent, INotification
    {
        public CalculateCompleted(Calculation calculation)
        {
            this.Calculation = calculation;
        }

        public Calculation Calculation { get; private set; }
    }
}