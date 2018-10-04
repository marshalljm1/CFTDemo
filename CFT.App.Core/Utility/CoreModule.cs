using System.Reflection;
using Autofac;
using Autofac.Extras.AggregateService;
using CFT.App.Core.Interfaces;
using CFT.App.Core.Services;
using Plugin.Toasts;
using Module = Autofac.Module;

namespace CFT.App.Core.Utility
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
            builder.RegisterAssemblyTypes(assembly).Where(t => t.Namespace.Contains("Validation"));

            //Services
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<ToastNotification>().As<IToastNotificator>();
            builder.RegisterGeneric(typeof(DataStore<>)).As(typeof(IDataStore<>));
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterAggregateService<ICommonServices>();
        }
    }
}
