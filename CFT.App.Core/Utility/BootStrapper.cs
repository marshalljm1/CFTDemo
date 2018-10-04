using Autofac;

namespace CFT.App.Core.Utility
{
    public static class BootStrapper
    {
        public static IContainer Container;

        public static void Run()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<CoreModule>();
            Container = builder.Build();
            //Container.Resolve<INavigationService>().InitializeAsync();
        }
    }
}
