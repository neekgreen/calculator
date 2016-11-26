namespace CalculatorApp.Features.Reports
{
    using Models;
    using MediatR;

    public class MathEquationResultHandler : INotificationHandler<MathEquationResult>
    {
        private readonly IMathEquationResultRepository commitApi;

        public MathEquationResultHandler(IMathEquationResultRepository commitApi)
        {
            this.commitApi = commitApi;
        }

        void INotificationHandler<MathEquationResult>.Handle(MathEquationResult result)
        {
            if (result != null)
                this.commitApi.Commit(result);
        }
    }
}