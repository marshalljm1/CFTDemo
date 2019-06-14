using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace CFT.API.Utility
{
    internal class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            
            builder.RegisterAssemblyTypes(assembly).Where(t => t.Namespace.EndsWith("Repositories")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assembly).Where(t => t.Namespace.EndsWith("Interfaces"));
            builder.RegisterAssemblyTypes(assembly).Where(t => t.Namespace.EndsWith("Models"));
        }
    }
}
