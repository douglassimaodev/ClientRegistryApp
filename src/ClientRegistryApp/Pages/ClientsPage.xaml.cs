using ClientRegistryApp.PageModels;

namespace ClientRegistryApp.Pages;

public partial class ClientsPage : ContentPage
{
    private readonly ClientsPageModel _pageModel;

    public ClientsPage(ClientsPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = _pageModel = pageModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (!_pageModel.Clients.Any())
            _pageModel.LoadClients();
    }
}