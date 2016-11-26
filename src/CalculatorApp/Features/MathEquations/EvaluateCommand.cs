namespace CalculatorApp.Features.MathEquations
{
    using MediatR;
    using CalculatorApp.Models;

    public class EvaluateCommand : IRequest<MathEquationResult>
    {
        public string Expression { get; set; }
    }
}