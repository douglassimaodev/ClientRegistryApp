using ClientRegistryApp.Common.Messegers;
using ClientRegistryApp.Models;
using ClientRegistryApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace ClientRegistryApp.PageModels;

public partial class ClientsPageModel : BasePageModel
{
    [ObservableProperty]
    ObservableCollection<Client> clients = new();

    private List<Client> AllClients { get; set; } = new();

    [ObservableProperty]
    string searchText;

    private readonly IClientService _clientService;

    public ClientsPageModel(INavigationService navigationService, IClientService clientService) : base(navigationService)
    {
        Title = "Clients";

        WeakReferenceMessenger.Default.Register<UpdateClientListMessage>(this, (recipient, message) =>
        {
            var existingClient = Clients.FirstOrDefault(c => c.Id == message.Client.Id);
            if (existingClient != null)
            {
                var index = Clients.IndexOf(existingClient);
                Clients[index] = message.Client;
            }
            else
            {
                Clients.Add(message.Client);
            }

            AllClients = [.. Clients];
        });

        _clientService = clientService;
    }

    [RelayCommand]
    async Task SelectedItem(Client client)
    {
        if (client == null)
        {
            return;
        }

        await NavigationService.PushAsync<ClientPageModel>(parameters: $"{nameof(client.Id)}={client.Id}");
    }

    [RelayCommand]
    async Task Add()
    {
        SearchText = null;
        await NavigationService.PushAsync<ClientPageModel>();
    }

    [RelayCommand]
    async Task Delete(Client client)
    {
        if (client == null)
            return;

        bool confirm = await Shell.Current.DisplayAlert("Delete Client", $"Are you sure you want to delete {client.Name} {client.LastName}?", "Yes", "No");
        if (confirm)
        {
            _clientService.Delete(client.Id);
            Clients.Remove(client);
        }

        LoadClients();
    }

    public void LoadClients()
    {
        AllClients = _clientService.GetAll();
        FilterClients();
    }

    [RelayCommand]
    void Search()
    {
        FilterClients();
    }

    private void FilterClients()
    {
        var filtered = string.IsNullOrWhiteSpace(SearchText)
            ? AllClients
            : AllClients.Where(c =>
                c.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                (!string.IsNullOrEmpty(c.LastName) && c.LastName.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) ||
                 c.Age.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                (!string.IsNullOrEmpty(c.Address) && c.Address.Contains(SearchText, StringComparison.OrdinalIgnoreCase))).ToList();

        Clients = new ObservableCollection<Client>(filtered);
    }
}
