namespace WebApi.Features.Calculations
{
    using MediatR;
    using WebApi.Models;

    public class CalculateCommandHandler : IRequestHandler<CalculateCommand, CalculateResult>, ICachableRequestHandler
    {
        private readonly ICalculatorEngine calculatorEngine;
        private readonly IMediator mediator;

        public CalculateCommandHandler(ICalculatorEngine calculatorEngine, IMediator mediator)
        {
            this.calculatorEngine = calculatorEngine;
            this.mediator = mediator;
        }

        CalculateResult IRequestHandler<CalculateCommand, CalculateResult>.Handle(CalculateCommand message)
        {
            var result = 
                new CalculateResult(
                    message.Expression,
                    this.calculatorEngine.Calculate(message.Expression)
                );

            return result;
        }
    }
}