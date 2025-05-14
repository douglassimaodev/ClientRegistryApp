using ClientRegistryApp.Models;

namespace ClientRegistryApp.Common.Messegers;

public class UpdateClientListMessage
{
    public Client Client { get; }

    public UpdateClientListMessage(Client client)
    {
        Client = client;
    }
}
