using System.Reflection;
using Autofac;
using Autofac.Extras.AggregateService;
using CFT.Promotions.Core.Interfaces;
using CFT.Promotions.Core.Models;
using CFT.Promotions.Core.Services;
using Module = Autofac.Module;

namespace CFT.Promotions.Core.Utility
{
    internal class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).Where(t => t.Namespace.EndsWith("Controls"));
            builder.RegisterAssemblyTypes(assembly).Where(t => t.Namespace.EndsWith("Models"));
            builder.RegisterAssemblyTypes(assembly).Where(t => t.Namespace.Contains("View"));
            builder.RegisterAssemblyTypes(assembly).Where(t => t.Namespace.EndsWith("ViewModel"));

            //Services
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterAggregateService<ICommonServices>();
#if DEBUG
            builder.RegisterType<MockDataStore>().As<IDataStore<BaseItem>>().SingleInstance();
#else
            builder.RegisterType<FruitionDataStore>().As<IDataStore<Item>>().SingleInstance();
#endif
        }
    }
}
