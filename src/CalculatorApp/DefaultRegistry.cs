namespace CalculatorApp
{
    using StructureMap;

    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            Scan(scan =>
            {
                scan.AssemblyContainingType<Program>();
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();

                scan.LookForRegistries();
            });
        }
    }
}