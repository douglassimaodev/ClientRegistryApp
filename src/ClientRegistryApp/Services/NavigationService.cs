using ClientRegistryApp.PageModels;

namespace ClientRegistryApp.Services
{
    public class NavigationService: INavigationService
    {
        public Task PushAsync<TViewModel>(string routePrefix = "", string parameters = null) where TViewModel : BasePageModel
        {
            return GoToAsync<TViewModel>(routePrefix, parameters);
        }

        public Task GoBackAsync(string prefix = "")
        {
            return Shell.Current.GoToAsync(prefix + "..", true);
        }

        private Task GoToAsync<TViewModel>(string routePrefix, string parameters) where TViewModel : BasePageModel
        {
            var route = routePrefix + typeof(TViewModel).Name;
            if (!string.IsNullOrWhiteSpace(parameters))
            {
                route += $"?{parameters}";
            }

            return Shell.Current.GoToAsync(route);
        }
    }    
}
