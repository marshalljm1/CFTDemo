using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace CFT.API.Utility
{
    public static class Bootstrapper
    {
        public static IContainer Container;

        public static void Run(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule<CoreModule>();
            Container = builder.Build();
        }
    }
}
