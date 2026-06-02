using System;
using System.Collections.Generic;
using Task_5.Models;

namespace Task_5.Services;

public class InMemoryClientService : IClientService
{
    private readonly List<ClientItem> _clients;

    public InMemoryClientService(IEnumerable<ClientItem>? clients = null)
    {
        _clients = clients is null
            ? CreateDefaultClients()
            : new List<ClientItem>(clients);
    }

    public IEnumerable<ClientItem> GetClients()
    {
        return _clients;
    }

    public ClientItem CreateClient(string newClientName)
    {
        var client = new ClientItem(
            newClientName,
            "+0 000 000 00 00",
            "Email@Example.com",
            false);
        
        _clients.Add(client);

        return client;
    }
    
    public void DeleteClient(ClientItem client)
    {
        _clients.Remove(client);
    }

    private static List<ClientItem> CreateDefaultClients()
    {
        return new List<ClientItem>
        {
            new ClientItem(
                "New client",
                "+0 000 000 00 00",
                "Email@Example.com",
                false)
        };
    }
}