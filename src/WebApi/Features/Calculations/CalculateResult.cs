namespace WebApi.Features.Calculations
{
    public class CalculateResult
    {
        public CalculateResult(string expression, decimal result)
        {
            this.Expression = expression;
            this.Result = result;
        }

        public string Expression { get; set; }
        public decimal Result { get; set; }
    }
}