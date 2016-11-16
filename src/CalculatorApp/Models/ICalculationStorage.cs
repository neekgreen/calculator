namespace CalculatorApp.Models
{
    using PaginableCollections;

    public interface ICalculationStorage
    {
        void Record(Calculation calculation);
        IPaginable<Calculation> GetPage(int pageNumber, int itemCountPerPage);
    }

    public static class CalculationStorageExtensions
    {
        public static IPaginable<Calculation> GetPage(this ICalculationStorage cs, IPaginableInfo paginableInfo)
        {
            return cs.GetPage(paginableInfo.PageNumber, paginableInfo.ItemCountPerPage);
        }
    }
}