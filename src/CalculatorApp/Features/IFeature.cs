namespace CalculatorApp.Features
{
    using Microsoft.Extensions.CommandLineUtils;

    public interface IFeature
    {
        void Register(CommandLineApplication app);
    }
}