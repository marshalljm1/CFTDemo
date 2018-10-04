using System;
using System.Threading.Tasks;
using CFT.App.Core.Models;

namespace CFT.App.Core.Interfaces
{
    public interface INavigationService
    {
        ViewModelBase PreviousPage { get; }
        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        Task NavigateToAsync(Type type);
        Task RemoveLastFromBackStackAsync();
        Task RemoveBackStackAsync();
    }
}
