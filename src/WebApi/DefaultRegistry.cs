namespace WebApi
{
    using StructureMap;

    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            Scan(scan =>
            {
                scan.AssemblyContainingType<DefaultRegistry>();
                scan.LookForRegistries();
                scan.WithDefaultConventions();
            });
        }
    }
}