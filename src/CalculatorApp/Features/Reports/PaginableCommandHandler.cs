namespace CalculatorApp.Features.Reports
{
    using MediatR;
    using Models;
    using PaginableCollections;

    public class PaginableCommandHandler : IRequestHandler<PaginableCommand, IPaginable<Calculation>>
    {
        private readonly ICalculationStorage calculations;

        public PaginableCommandHandler(ICalculationStorage calculations)
        {
            this.calculations = calculations;
        }

        IPaginable<Calculation> IRequestHandler<PaginableCommand, IPaginable<Calculation>>.Handle(PaginableCommand message)
        {
            return calculations.GetPage(message);
        }
    }  
}