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

        public class SomethingElse : IDomainEvent, INotification
    {
        public SomethingElse(Calculation calculation)
        {
            this.Calculation = calculation;
        }

        public Calculation Calculation { get; private set; }
    }


    public class SomethingElseHandler : INotificationHandler<SomethingElse>
    {
        private readonly ICalculationStorage history;

        public SomethingElseHandler(ICalculationStorage history)
        {
            this.history = history;
        }

        void INotificationHandler<SomethingElse>.Handle(SomethingElse notification)
        {
            var calculation = notification.Calculation;

            if (calculation != null)
                history.Record(calculation);
        }
    }
}