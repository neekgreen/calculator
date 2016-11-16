namespace CalculatorApp.Features.Calculators
{
    using System;
    using System.Linq;
    using CalculatorApp.Models;
    using MediatR;

    public class CalculateCommandHandler : IRequestHandler<CalculateCommand, Calculation>, ICachableRequestHandler
    {
        private readonly ICalculator calculator;
        private readonly IMediator mediator;

        public CalculateCommandHandler(ICalculator calculator, IMediator mediator)
        {
            this.calculator = calculator;
            this.mediator = mediator;
        }

        Calculation IRequestHandler<CalculateCommand, Calculation>.Handle(CalculateCommand message)
        {
            var calculation = new Calculation(message.Expression);
            calculation.Calculate(this.calculator);

            calculation.Events.OfType<INotification>().ToList().ForEach(t => this.mediator.Publish(t));

            return calculation;
        }
    }
}