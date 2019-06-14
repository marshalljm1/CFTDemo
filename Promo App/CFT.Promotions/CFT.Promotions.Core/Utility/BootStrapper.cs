using Autofac;
using CFT.Promotions.Core.Interfaces;

namespace CFT.Promotions.Core.Utility
{
    public static class BootStrapper
    {
        public static IContainer Container;

        public static void Run()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<CoreModule>();
            Container = builder.Build();
            Container.Resolve<INavigationService>().InitializeAsync();
        }
    }
}
