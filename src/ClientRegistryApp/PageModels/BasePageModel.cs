using ClientRegistryApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClientRegistryApp.PageModels
{
    public partial class BasePageModel : ObservableObject
    {
        protected readonly INavigationService NavigationService;

        [ObservableProperty]
        string title;

        public BasePageModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        [RelayCommand]
        async Task Back()
        {
            await NavigationService.GoBackAsync();
        }
    }
}
