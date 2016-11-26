namespace CalculatorApp
{
    using System;
    using StructureMap;
    using System.Linq;
    using Microsoft.Extensions.CommandLineUtils;
    using Features;


    public class Program
    {
        private const int ExitCode = -1;
        private static Container ParentScope { get; set; }

        public static void Main(string[] args)
        {
            ParentScope = new Container(c => c.AddRegistry<DefaultRegistry>());

            string input = null;
            int result = 0;

            do
            {
                var app = new CommandLineApplication(throwOnUnexpectedArg: false);

                app.Command("quit", c =>
                {
                    c.OnExecute(() =>
                    {
                        return ExitCode;
                    });
                });

                ParentScope.GetAllInstances(typeof(IFeature))
                    .OfType<IFeature>()
                    .ToList()
                    .ForEach(t =>
                    {
                        t.Register(app);
                    });

                Console.WriteLine("Enter command:");
                input = Console.ReadLine();

                try
                {
                    args = input.ToCommandLineArgs();
                    result = app.Execute(args);
                }
                catch
                {
                    Console.WriteLine("Invalid command - try again.");
                }

            } while (result != ExitCode);
        }
    }
}