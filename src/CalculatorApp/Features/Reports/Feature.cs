namespace CalculatorApp.Features.Reports
{
    using System;
    using CalculatorApp.Features;
    using CalculatorApp.Models;
    using Microsoft.Extensions.CommandLineUtils;

    public class Feature : IFeature 
    {
        private readonly ICalculationStorage calculationStorage;

        public Feature (ICalculationStorage calculationStorage)
        {
            this.calculationStorage = calculationStorage;
        }

        void IFeature.Register(CommandLineApplication app)
        {
            app.Command("report", c =>
            {
                const int DefaultItemCountPerPage = 20;
                const int DefaultPageNumber = 1;

                var pageNumberOption = c.Option("-pn|--pagenumber", "The page number of items to return", CommandOptionType.SingleValue);

                c.OnExecute(() =>
                {
                    var pageNumberOptionValue = Convert.ToInt32(pageNumberOption.Value() ?? DefaultPageNumber.ToString());

                    var paginable =
                        calculationStorage.GetPage(pageNumberOptionValue, DefaultItemCountPerPage);

                    Console.WriteLine("Calculation Report");
                    Console.WriteLine("Page {0} of {1} | {2} calculations", paginable.PageNumber, paginable.TotalPageCount, paginable.TotalItemCount);

                    foreach (var item in paginable)
                    {
                        Console.WriteLine("{0}={1}", item.Expression, item.Result);
                        Console.WriteLine("{0}", item.Created);
                    }

                    Console.WriteLine();
                    Console.WriteLine();

                    return 0;
                });

                c.HelpOption("-?|-h|--help");
            });
        }
    }
}