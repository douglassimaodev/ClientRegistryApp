using ClientRegistryApp.Models;

namespace ClientRegistryApp.Services
{
    public class ClientService : IClientService
    {

        public List<Client> GetAll() => App.Clients;

        public void Add(Client client) => App.Clients.Add(client);

        public void Update(Client client)
        {
            var index = App.Clients.FindIndex(c => c.Id == client.Id);
            if (index >= 0) App.Clients[index] = client;
        }

        public void Delete(Guid clientId)
        {
            var client = App.Clients.FirstOrDefault(c => c.Id == clientId);
            if (client != null) App.Clients.Remove(client);
        }
    }
}
