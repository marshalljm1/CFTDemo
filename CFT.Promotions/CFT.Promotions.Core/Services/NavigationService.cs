using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using CFT.Promotions.Core.Interfaces;
using CFT.Promotions.Core.ViewModels;
using CFT.Promotions.Core.Views;
using CFT.Promotions.Core.Utility;
using Xamarin.Forms;

namespace CFT.Promotions.Core.Services
{
    public class NavigationService : INavigationService
    {

        //Public Members
        public ViewModelBase PreviousPage { get; private set; }


        //Public methods
        public Task InitializeAsync()
        {
#if DEBUG
            return string.IsNullOrEmpty(Settings.AuthAccessToken) ? NavigateToAsync<LoginViewModel>() : NavigateToAsync<MainViewModel>();
#else
            return NavigateToAsync<SignUpViewModel>();
#endif
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type type)
        {
            return InternalNavigateToAsync(type, null);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveBackStackAsync()
        {
            throw new NotImplementedException();
        }


        //Private methods
        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            var page = CreatePage(viewModelType, parameter);

            if (page is LoginView)
            {
                Application.Current.MainPage = new CustomNavigationView(page);
            }
            else
            {
                //setup master/detail page if loading for first time
                //if(page == login)
                if (!(Application.Current.MainPage is MasterDetailPage))
                {
                    App.MasterDetailPage = new MasterDetailPage
                    {
                        Master = ResolvePage(typeof(MenuView)),
                        Detail = ResolvePage(typeof(CustomNavigationView))
                    };
                    Application.Current.MainPage = App.MasterDetailPage;
                }

                if (App.MasterDetailPage.Detail is CustomNavigationView navigationPage)
                {
                    //don't add another page to the stack if the same one is already shown
                    if (navigationPage.CurrentPage?.GetType() != page.GetType())
                    {
                        PreviousPage = navigationPage.CurrentPage?.BindingContext as ViewModelBase;
                        await navigationPage.PushAsync(page);
                    }

                    App.MasterDetailPage.IsPresented = false;
                }
                else
                {
                    App.MasterDetailPage.Detail = new CustomNavigationView(page);
                    App.MasterDetailPage.IsPresented = false;
                }
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        //return page type for parameter view model type
        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(
                CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        //create instance of page using autofac container
        private Page CreatePage(Type viewModelType, object parameter)
        {
            var pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            return ResolvePage(pageType);
        }

        private Page ResolvePage(Type type)
        {
            return BootStrapper.Container.Resolve(type) as Page;
        }


    }
}
