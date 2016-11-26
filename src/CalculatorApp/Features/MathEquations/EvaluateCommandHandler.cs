namespace CalculatorApp.Features.MathEquations
{
    using CalculatorApp.Models;
    using MediatR;

    public class EvaluateCommandHandler : IRequestHandler<EvaluateCommand, MathEquationResult>
    {
        private readonly ICalculator calculator;
        private readonly IMediator mediator;

        public EvaluateCommandHandler(ICalculator calculator, IMediator mediator)
        {
            this.calculator = calculator;
            this.mediator = mediator;
        }

        MathEquationResult IRequestHandler<EvaluateCommand, MathEquationResult>.Handle(EvaluateCommand message)
        {
            var result = new MathEquation(message.Expression);
            result.Evaluate(this.calculator);

            return new MathEquationResult(result);
        }
    }
}