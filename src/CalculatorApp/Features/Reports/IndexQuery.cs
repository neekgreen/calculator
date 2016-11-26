namespace CalculatorApp.Features.Reports
{
    using CalculatorApp.Models;
    using MediatR;
    using PaginableCollections;

    public class IndexQuery : IRequest<IPaginable<MathEquationResult>>, IPaginableInfo
    {
        public IndexQuery()
        {
            this.ItemCountPerPage = 10;
        }

        public int PageNumber { get; set; }
        public int ItemCountPerPage { get; set; }
    }
}