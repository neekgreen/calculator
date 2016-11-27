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
            app.Command("eval", c =>
            {
                c.Description = "Calculate an expression";

                var expressionArgument = c.Argument("[expression]", "The math expression to evaluate.");
 
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
                                Expression = expressionArgument.Value
                            });

                    mediator.Publish(commandResult);

                    Console.WriteLine("{0}={1}", expressionArgument.Value, commandResult.Result);
                    Console.WriteLine();

                    return 0;
                });

                c.HelpOption("-?|-h|--help");
            });
        }
    }
}