namespace CalculatorApp.Features.MathEquations
{
    using System;
    using Microsoft.Extensions.CommandLineUtils;
    using MediatR;

    public class Feature : IFeature
    {
        private readonly IMediator mediator;

        public Feature(IMediator mediator)
        {
            this.mediator = mediator;
        }

        void IFeature.Register(CommandLineApplication app)
        {
            app.Command("calculate", c =>
            {
                c.Description = "Calculate an expression";

                var expressionArgument = c.Argument("[expression]", "The math expression to evaluate.");
                var cacheOption = c.Option("-c|--cache", "Cache the expression and result.", CommandOptionType.NoValue);

                c.OnExecute(() =>
                {
                    if (expressionArgument.Value == null)
                    {
                        Console.WriteLine("You must specify an expression");
                        return 1;
                    }

                    var commandResult =
                        mediator.Send(
                            new EvaluateCommand
                            {
                                Expression = expressionArgument.Value,
                                IsCachable = cacheOption.Value() != null,
                            });

                    mediator.Publish(commandResult);

                    Console.WriteLine("{0}={1}", expressionArgument.Value, commandResult.Result);
                    Console.WriteLine("{0}", commandResult.Created);
                    Console.WriteLine();

                    return 0;
                });

                c.HelpOption("-?|-h|--help");
            });
        }
    }
}