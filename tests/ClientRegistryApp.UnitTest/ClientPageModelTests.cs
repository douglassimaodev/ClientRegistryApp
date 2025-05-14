using ClientRegistryApp.Common.Validations.Validators.Rules;
using ClientRegistryApp.Models;
using ClientRegistryApp.PageModels;
using ClientRegistryApp.Services;
using FluentAssertions;
using NSubstitute;


namespace ClientRegistryApp.UnitTest;

public class ClientPageModelTests
{
    private readonly INavigationService _navigationService;
    private readonly IClientService _clientService;
    private readonly ClientPageModel _viewModel;

    public ClientPageModelTests()
    {
        _navigationService = Substitute.For<INavigationService>();
        _clientService = Substitute.For<IClientService>();
        _viewModel = new ClientPageModel(_navigationService, _clientService);
    }

    [Fact]
    public void Constructor_Should_Set_Title_And_ValidationRules()
    {
        _viewModel.Title.Should().Be("Client");
        _viewModel.Name.Item1.Validations.Should().ContainSingle(x => x is IsNotNullOrEmptyRule<string>);
        _viewModel.Age.Item1.Validations.Should().ContainSingle(x => x is NullableIsGreaterOrEqualThanRule);
    }

    [Fact]
    public void ApplyQueryAttributes_Should_Populate_Fields_When_Client_Found()
    {
        var clientId = Guid.NewGuid();
        var client = new Client
        {
            Id = clientId,
            Name = "John",
            LastName = "Doe",
            Age = 30,
            Address = "123 Main St"
        };
        _clientService.GetAll().Returns(new List<Client> { client });

        var query = new Dictionary<string, object> { { nameof(ClientPageModel.Id), clientId.ToString() } };

        _viewModel.ApplyQueryAttributes(query);

        _viewModel.Id.Should().Be(clientId);
        _viewModel.Name.Item1.Value.Should().Be("John");
        _viewModel.LastName.Should().Be("Doe");
        _viewModel.Age.Item1.Value.Should().Be("30");
        _viewModel.Address.Should().Be("123 Main St");
    }

    [Fact]
    public void ApplyQueryAttributes_Should_Not_Throw_When_Client_Not_Found()
    {
        var clientId = Guid.NewGuid();
        _clientService.GetAll().Returns(new List<Client>());

        var query = new Dictionary<string, object> { { nameof(ClientPageModel.Id), clientId.ToString() } };

        Action act = () => _viewModel.ApplyQueryAttributes(query);

        act.Should().NotThrow();
        _viewModel.Id.Should().Be(clientId);
    }

    [Fact]
    public async Task Save_Should_Add_Client_When_Id_Empty_And_Fields_Valid()
    {
        _viewModel.Name.Item1.Value = "Jane";
        _viewModel.Age.Item1.Value = "25";
        _viewModel.LastName = "Smith";
        _viewModel.Address = "456 Elm St";
        _viewModel.Id = Guid.Empty;

        _viewModel.Name.Item1.Validations.Clear();
        _viewModel.Age.Item1.Validations.Clear();
        _viewModel.Name.Item1.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Name is mandatory." });
        _viewModel.Age.Item1.Validations.Add(new NullableIsGreaterOrEqualThanRule { GreaterThan = 1, ValidationMessage = "Age is mandatory." });

        await _viewModel.SaveCommand.ExecuteAsync(null);

        _clientService.Received(1).Add(Arg.Is<Client>(c =>
            c.Name == "Jane" &&
            c.LastName == "Smith" &&
            c.Age == 25 &&
            c.Address == "456 Elm St"
        ));
        await _navigationService.Received(1).GoBackAsync();
    }

    [Fact]
    public async Task Save_Should_Update_Client_When_Id_Not_Empty_And_Fields_Valid()
    {
        var clientId = Guid.NewGuid();
        _viewModel.Name.Item1.Value = "Jane";
        _viewModel.Age.Item1.Value = "25";
        _viewModel.LastName = "Smith";
        _viewModel.Address = "456 Elm St";
        _viewModel.Id = clientId;

        _viewModel.Name.Item1.Validations.Clear();
        _viewModel.Age.Item1.Validations.Clear();
        _viewModel.Name.Item1.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Name is mandatory." });
        _viewModel.Age.Item1.Validations.Add(new NullableIsGreaterOrEqualThanRule { GreaterThan = 1, ValidationMessage = "Age is mandatory." });

        await _viewModel.SaveCommand.ExecuteAsync(null);

        _clientService.Received(1).Update(Arg.Is<Client>(c =>
            c.Id == clientId &&
            c.Name == "Jane" &&
            c.LastName == "Smith" &&
            c.Age == 25 &&
            c.Address == "456 Elm St"
        ));
        await _navigationService.Received(1).GoBackAsync();
    }

    [Fact]
    public async Task Save_Should_Not_Add_Or_Update_When_Fields_Invalid()
    {
        _viewModel.Name.Item1.Value = "";
        _viewModel.Age.Item1.Value = "";
        _viewModel.Id = Guid.Empty;

        await _viewModel.SaveCommand.ExecuteAsync(null);

        _clientService.DidNotReceive().Add(Arg.Any<Client>());
        _clientService.DidNotReceive().Update(Arg.Any<Client>());
        await _navigationService.DidNotReceive().GoBackAsync();
    }
}
