using System.Collections.Generic;
using Task_5.Models;

namespace Task_5.Services;

public interface IClientService
{
    IEnumerable<ClientItem> GetClients();

    ClientItem CreateClient();

    void DeleteClient(ClientItem client);
}