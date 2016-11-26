namespace CalculatorApp.Models
{
    using System;
    using System.Collections.Generic;
    using PaginableCollections;

    public class MathEquationResultRepository : IMathEquationResultRepository
    {
        private readonly IList<MathEquationResult> innerList = new List<MathEquationResult>();

        IPaginable<MathEquationResult> IMathEquationResultRepository.Get(int pageNumber, int itemCountPerPage)
        {
            return innerList.ToPaginable(pageNumber, itemCountPerPage);
        }

        void IMathEquationResultRepository.Commit(MathEquationResult result)
        {
            this.innerList.Add(result);
        }
    }
}