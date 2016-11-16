namespace CalculatorApp.Features.Reports
{
    using CalculatorApp.Models;
    using MediatR;
    using PaginableCollections;

    public class PaginableCommand : IRequest<IPaginable<Calculation>>, IPaginableInfo
    {
        public PaginableCommand()
        {
            this.ItemCountPerPage = 10;
        }

        public int PageNumber { get; set; }
        public int ItemCountPerPage { get; set; }
    }
}