using ClientRegistryApp.PageModels;

namespace ClientRegistryApp.Services
{
    public interface INavigationService
    {
        Task PushAsync<TViewModel>(string routePrefix = "", string parameters = null) where TViewModel : BasePageModel;
        Task GoBackAsync(string prefix = "");
    }
}
