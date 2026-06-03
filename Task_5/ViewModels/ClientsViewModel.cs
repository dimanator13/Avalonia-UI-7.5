using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Task_5.Services;

namespace Task_5.ViewModels;

public partial class ClientsViewModel : ViewModelBase
{
    private readonly IClientService _clientService;
    
    [NotifyCanExecuteChangedFor(nameof(DeleteClientCommand))]
    [NotifyPropertyChangedFor(nameof(HasSelectedClient))]
    [ObservableProperty] private ClientItemViewModel? _selectedClient;

    public bool HasSelectedClient => SelectedClient is not null;
    
    [ObservableProperty] private string _newClientName = string.Empty;
    
    [ObservableProperty] private string _searchText = string.Empty;
    
    public ObservableCollection<ClientItemViewModel> Clients { get; } = new();
    
    public ObservableCollection<ClientItemViewModel> FilteredClients { get; } = new();
    
    [NotifyPropertyChangedFor(nameof(HasNewClientNameError))]
    [ObservableProperty] private string _newClientNameError = string.Empty;

    public bool HasNewClientNameError => !string.IsNullOrWhiteSpace(NewClientNameError);
    
    public event Action? ClientsChanged;

    public ClientsViewModel(IClientService clientService)
    {
        _clientService = clientService;

        foreach (var client in _clientService.GetClients())
        {
            Clients.Add(new ClientItemViewModel(client));
        }
        
        RefreshFilteredClients();
    }

    [RelayCommand]
    private void AddClient()
    {
        if (!ValidateNewClientName())
            return;
        
        var client = _clientService.CreateClient(NewClientName.Trim());
        var clientViewModel = new ClientItemViewModel(client);
        
        Clients.Add(clientViewModel);
        
        NewClientName = string.Empty;
        SearchText = string.Empty;
        
        RefreshFilteredClients();
        
        SelectedClient = clientViewModel;
        
        ClientsChanged?.Invoke();
    }

    [RelayCommand(CanExecute = nameof(HasSelectedClient))]
    private void DeleteClient()
    {
        if (SelectedClient is null)
            return;
        
        _clientService.DeleteClient(SelectedClient.Model);
        Clients.Remove(SelectedClient);

        SelectedClient = null;
        
        RefreshFilteredClients();
        
        ClientsChanged?.Invoke();
    }
    
    private bool ValidateNewClientName()
    {
        if (string.IsNullOrWhiteSpace(NewClientName))
        {
            NewClientNameError = "Enter the client name";
            return false;
        }

        if (NewClientName.Trim().Length < 3)
        {
            NewClientNameError = "Name must have minimum 3 symbols";
            return false;
        }

        NewClientNameError = string.Empty;
        return true;
    }
    
    partial void OnNewClientNameChanged(string value)
    {
        if (HasNewClientNameError && value.Trim().Length >= 3)
        {
            NewClientNameError = string.Empty;
        }
    }
    
    private void RefreshFilteredClients()
    {
        FilteredClients.Clear();

        foreach (var client in Clients)
        {
            bool matchesSearch =
                string.IsNullOrWhiteSpace(SearchText) ||
                client.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);

            if (matchesSearch)
            {
                FilteredClients.Add(client);
            }
        }
    }
    
    partial void OnSearchTextChanged(string value)
    {
        RefreshFilteredClients();
    }
}