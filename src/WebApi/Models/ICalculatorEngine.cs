namespace WebApi.Models
{
    public interface ICalculatorEngine
    {
        decimal Calculate(string expression);
    }
}