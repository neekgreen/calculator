namespace CalculatorApp.Models
{
    using System.Collections.Generic;
    using PaginableCollections;

    public class CalculationStorage : ICalculationStorage
    {
        private readonly List<Calculation> calculations = new List<Calculation>();

        void ICalculationStorage.Record(Calculation calculation)
        {
            this.calculations.Add(calculation);
        }

        IPaginable<Calculation> ICalculationStorage.GetPage(int pageNumber, int itemCountPerPage)
        {
            return calculations.ToPaginable(pageNumber, itemCountPerPage);
        }
    }
}