namespace CalculatorApp.Features.Reports
{
    using Models;
    using MediatR;

    public class MathEquationResultHandler : INotificationHandler<MathEquationResult>
    {
        private readonly IMathEquationResultRepository respository;

        public MathEquationResultHandler(IMathEquationResultRepository respository)
        {
            this.respository = respository;
        }

        void INotificationHandler<MathEquationResult>.Handle(MathEquationResult result)
        {
            if (result != null)
                this.respository.Commit(result);
        }
    }
}