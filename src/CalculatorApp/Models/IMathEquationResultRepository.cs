namespace CalculatorApp.Models
{
    using PaginableCollections;

    public interface IMathEquationResultRepository
    {
        IPaginable<MathEquationResult> Get(int pageNumber, int itemCountPerPage);
        void Commit(MathEquationResult result);
    }
}