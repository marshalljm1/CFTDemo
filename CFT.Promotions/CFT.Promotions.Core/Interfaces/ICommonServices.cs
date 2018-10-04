//add common services here

using Plugin.Toasts;

namespace CFT.Promotions.Core.Interfaces
{
    public interface ICommonServices
    {
        INavigationService Navigation { get; }
        IToastNotificator Notificator { get; }
    }
}
