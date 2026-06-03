using System;

namespace Task_5.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private readonly ProductsViewModel _productsViewModel;
    private readonly ClientsViewModel _clientsViewModel;
    private readonly OrdersViewModel _ordersViewModel;

    public int ProductsCount => _productsViewModel.Products.Count;

    public int ClientsCount => _clientsViewModel.Clients.Count;

    public int OrdersCount => _ordersViewModel.Orders.Count;

    public HomeViewModel(
        ProductsViewModel productsViewModel,
        ClientsViewModel clientsViewModel,
        OrdersViewModel ordersViewModel)
    {
        _productsViewModel = productsViewModel;
        _clientsViewModel = clientsViewModel;
        _ordersViewModel = ordersViewModel;

        _productsViewModel.ProductsChanged += OnProductsChanged;
        _clientsViewModel.ClientsChanged += OnClientsChanged;
        _ordersViewModel.OrdersChanged += OnOrdersChanged;
    }

    private void OnProductsChanged()
    {
        OnPropertyChanged(nameof(ProductsCount));
    }

    private void OnClientsChanged()
    {
        OnPropertyChanged(nameof(ClientsCount));
    }

    private void OnOrdersChanged()
    {
        OnPropertyChanged(nameof(OrdersCount));
    }
}