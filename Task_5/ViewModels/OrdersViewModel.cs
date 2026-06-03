using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Task_5.Models;
using Task_5.Services;

namespace Task_5.ViewModels;

public partial class OrdersViewModel : ViewModelBase
{
    private readonly IOrderService _orderService;
    
    [NotifyCanExecuteChangedFor(nameof(DeleteOrderCommand))]
    [NotifyCanExecuteChangedFor(nameof(ChangeOrderStatusCommand))]
    [NotifyPropertyChangedFor(nameof(HasSelectedOrder))]
    [ObservableProperty] private OrderItemViewModel? _selectedOrder;

    public bool HasSelectedOrder => SelectedOrder is not null;

    [ObservableProperty] private string _newOrderClientName = string.Empty;
    
    [ObservableProperty] private string _searchText = string.Empty;
    
    [ObservableProperty] private OrderStatus? _selectedStatusFilter;

    public ObservableCollection<OrderItemViewModel> Orders { get; } = new();
    
    public ObservableCollection<OrderItemViewModel> FilteredOrders { get; } = new();
    
    public ObservableCollection<OrderStatus> OrderStatuses { get; } = new()
    {
        OrderStatus.New,
        OrderStatus.Paid,
        OrderStatus.Cancelled,
        OrderStatus.Completed
    };
    
    [NotifyPropertyChangedFor(nameof(HasNewOrderClientNameError))]
    [ObservableProperty] private string _newOrderClientNameError = string.Empty;

    public bool HasNewOrderClientNameError => !string.IsNullOrWhiteSpace(NewOrderClientNameError);
    
    public event Action? OrdersChanged;

    public OrdersViewModel(IOrderService orderService)
    {
        _orderService = orderService;

        foreach (var order in _orderService.GetOrders())
        {
            Orders.Add(new OrderItemViewModel(order));
        }
        
        RefreshFilteredOrders();
        
        OrdersChanged?.Invoke();
    }

    [RelayCommand]
    private void AddOrder()
    {
        if (!ValidateNewOrderClientName())
            return;

        var order = _orderService.CreateOrder(NewOrderClientName.Trim());
        var orderViewModel = new OrderItemViewModel(order);
        
        Orders.Add(orderViewModel);
        
        NewOrderClientName = string.Empty;
        SearchText = string.Empty;
        SelectedStatusFilter = null;
        
        RefreshFilteredOrders();
        
        SelectedOrder = orderViewModel;
        
        OrdersChanged?.Invoke();
    }

    [RelayCommand(CanExecute = nameof(HasSelectedOrder))]
    private void DeleteOrder()
    {
        if (SelectedOrder is null)
            return;
        
        _orderService.DeleteOrder(SelectedOrder.Model);
        Orders.Remove(SelectedOrder);

        SelectedOrder = null;
        
        RefreshFilteredOrders();
    }

    [RelayCommand(CanExecute = nameof(HasSelectedOrder))]
    private void ChangeOrderStatus(OrderStatus status)
    {
        if (SelectedOrder is null)
            return;
        
        _orderService.ChangeOrderStatus(SelectedOrder.Model, status);
        
        SelectedOrder.RefreshStatus();
        
        RefreshFilteredOrders();
    }
    
    [RelayCommand]
    private void ResetStatusFilter()
    {
        SelectedStatusFilter = null;
    }
    
    private bool ValidateNewOrderClientName()
    {
        if (string.IsNullOrWhiteSpace(NewOrderClientName))
        {
            NewOrderClientNameError = "Enter the client name";
            return false;
        }

        if (NewOrderClientName.Trim().Length < 3)
        {
            NewOrderClientNameError = "Name must have minimum 3 symbols";
            return false;
        }

        NewOrderClientNameError = string.Empty;
        return true;
    }
    
    partial void OnNewOrderClientNameChanged(string value)
    {
        if (HasNewOrderClientNameError && value.Trim().Length >= 3)
        {
            NewOrderClientNameError = string.Empty;
        }
    }
    
    private void RefreshFilteredOrders()
    {
        FilteredOrders.Clear();

        foreach (var order in Orders)
        {
            bool matchesSearch =
                string.IsNullOrWhiteSpace(SearchText) ||
                order.ClientName.Contains(SearchText, StringComparison.OrdinalIgnoreCase);

            bool matchesCategory =
                SelectedStatusFilter is null ||
                order.Status == SelectedStatusFilter;

            if (matchesSearch && matchesCategory)
            {
                FilteredOrders.Add(order);
            }
        }
        
        if (SelectedOrder is not null && !FilteredOrders.Contains(SelectedOrder))
        {
            SelectedOrder = null;
        }
    }
    
    partial void OnSearchTextChanged(string value)
    {
        RefreshFilteredOrders();
    }
    
    partial void OnSelectedStatusFilterChanged(OrderStatus? value)
    {
        RefreshFilteredOrders();
    }
}