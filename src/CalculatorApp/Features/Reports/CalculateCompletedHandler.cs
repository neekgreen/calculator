namespace CalculatorApp.Features.Reports
{
    using Models;
    using MediatR;

    public class CalculateCompletedHandler : INotificationHandler<CalculateCompleted>
    {
        private readonly ICalculationStorage calculationStorage;

        public CalculateCompletedHandler(ICalculationStorage calculationStorage)
        {
            this.calculationStorage = calculationStorage;
        }

        void INotificationHandler<CalculateCompleted>.Handle(CalculateCompleted notification)
        {
            if (notification.Calculation != null)
                this.calculationStorage.Record(notification.Calculation);
        }
    }
}