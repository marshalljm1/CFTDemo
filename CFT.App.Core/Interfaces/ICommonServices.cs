//add common services here

using Plugin.Toasts;

namespace CFT.App.Core.Interfaces
{
    public interface ICommonServices
    {
        INavigationService Navigation { get; }
        IToastNotificator Notificator { get; }
    }
}
