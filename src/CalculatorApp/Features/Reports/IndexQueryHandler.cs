namespace CalculatorApp.Features.Reports
{
    using MediatR;
    using Models;
    using PaginableCollections;

    public class IndexQueryHandler : IRequestHandler<IndexQuery, IPaginable<MathEquationResult>>
    {
        private readonly IMathEquationResultRepository calculations;

        public IndexQueryHandler(IMathEquationResultRepository calculations)
        {
            this.calculations = calculations;
        }

        IPaginable<MathEquationResult> IRequestHandler<IndexQuery, IPaginable<MathEquationResult>>.Handle(IndexQuery message)
        {
            return calculations.Get(message.PageNumber, message.ItemCountPerPage);
        }
    }  
}