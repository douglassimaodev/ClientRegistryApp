using ClientRegistryApp.Common.Messegers;
using ClientRegistryApp.Common.Validations.Validators;
using ClientRegistryApp.Common.Validations.Validators.Rules;
using ClientRegistryApp.Models;
using ClientRegistryApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace ClientRegistryApp.PageModels;

public partial class ClientPageModel : BasePageModel, IQueryAttributable
{
    public Guid Id { get; set; }

    [ObservableProperty]
    ValidatablePair<string> name = new();

    [ObservableProperty]
    string lastName;

    [ObservableProperty]
    ValidatablePair<string> age = new();

    [ObservableProperty]
    string address;

    private readonly IClientService _clientService;
    internal Client Client { get; set; }

    public ClientPageModel(INavigationService navigationService, IClientService clientService) : base(navigationService)
    {
        Title = "Client";

        Name.Item1.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Name is mandatory." });
        Age.Item1.Validations.Add(new NullableIsGreaterOrEqualThanRule { GreaterThan = 1, ValidationMessage = "Age is mandatory." });
        _clientService = clientService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(nameof(Id), out var idValue) && Guid.TryParse(idValue.ToString(), out var guid))
        {
            Id = guid;
            var client = _clientService.GetAll().FirstOrDefault(c => c.Id == Id);
            if (client != null)
            {
                Id = client.Id;
                Name.Item1.Value = client.Name;
                LastName = client.LastName;
                Age.Item1.Value = client.Age.ToString();
                Address = client.Address;
            }
        }
    }

    [RelayCommand]
    async Task Save()
    {
        if (IsFildesValid())
        {
            Client = new Client
            {
                Name = Name.Item1.Value,
                LastName = LastName,
                Age = Convert.ToInt32(Age.Item1.Value),
                Address = Address
            };

            if (Id == Guid.Empty)
            {
                _clientService.Add(Client);
            }
            else
            {
                Client.Id = Id;
                _clientService.Update(Client);
            }

            WeakReferenceMessenger.Default.Send(new UpdateClientListMessage(Client));
            await NavigationService.GoBackAsync();
        }
    }

    bool IsFildesValid()
    {
        var isNameValid = Name.Validate();
        var isAgeValid = Age.Validate();

        return isNameValid && isAgeValid;
    }
}
