using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Task_5.Services;

namespace Task_5.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private HomeViewModel HomeViewModel { get; }

    private ProductsViewModel ProductsViewModel { get; }

    private ClientsViewModel ClientsViewModel { get; }

    private OrdersViewModel OrdersViewModel { get; }

    private SettingsViewModel SettingsViewModel { get; }

    [ObservableProperty]
    private ViewModelBase _currentViewModel;

    public MainWindowViewModel()
    {
        IProductService productService = new InMemoryProductService();
        IClientService clientService = new InMemoryClientService();
        IOrderService orderService = new InMemoryOrderService();

        ProductsViewModel = new ProductsViewModel(productService);
        ClientsViewModel = new ClientsViewModel(clientService);
        OrdersViewModel = new OrdersViewModel(orderService);
        SettingsViewModel = new SettingsViewModel();

        HomeViewModel = new HomeViewModel(
            ProductsViewModel,
            ClientsViewModel,
            OrdersViewModel);

        CurrentViewModel = HomeViewModel;
    }

    [RelayCommand]
    private void ShowHome()
    {
        CurrentViewModel = HomeViewModel;
    }

    [RelayCommand]
    private void ShowProducts()
    {
        CurrentViewModel = ProductsViewModel;
    }

    [RelayCommand]
    private void ShowClients()
    {
        CurrentViewModel = ClientsViewModel;
    }

    [RelayCommand]
    private void ShowOrders()
    {
        CurrentViewModel = OrdersViewModel;
    }

    [RelayCommand]
    private void ShowSettings()
    {
        CurrentViewModel = SettingsViewModel;
    }
}