namespace CalculatorApp.Features.Common
{
    using Microsoft.Extensions.CommandLineUtils;

    public interface IFeature
    {
        void Register(CommandLineApplication app);
    }
}