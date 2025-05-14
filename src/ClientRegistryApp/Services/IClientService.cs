using ClientRegistryApp.Models;

namespace ClientRegistryApp.Services
{
    public interface IClientService
    {
        List<Client> GetAll();
        void Add(Client client);
        void Update(Client client);
        void Delete(Guid clientId);
    }
}
