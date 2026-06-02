using System;
using Task_5.Models;

namespace Task_5.ViewModels;

public class OrderItemViewModel : ViewModelBase
{
    private readonly OrderItem _model;

    public OrderItem Model => _model;

    public OrderItemViewModel(OrderItem model)
    {
        _model = model;
    }
    
    public int Number
    {
        get => _model.Number;
        set
        {
            if (_model.Number == value)
                return;

            _model.Number = value;
            OnPropertyChanged();
        }
    }
    
    public string ClientName
    {
        get => _model.ClientName;
        set
        {
            if (_model.ClientName == value)
                return;

            _model.ClientName = value;
            OnPropertyChanged();
        }
    }
    
    public DateTimeOffset Date
    {
        get => _model.Date;
        set
        {
            if (_model.Date == value)
                return;

            _model.Date = value;
            OnPropertyChanged();
        }
    }
    
    public int Amount
    {
        get => _model.Amount;
        set
        {
            if (_model.Amount == value)
                return;

            _model.Amount = value;
            OnPropertyChanged();
        }
    }
    
    public OrderStatus Status
    {
        get => _model.Status;
        set
        {
            if (_model.Status == value)
                return;

            _model.Status = value;
            OnPropertyChanged();
            RefreshStatus();
        }
    }

    public bool IsPaid => Status == OrderStatus.Paid;
    
    public bool IsCancelled => Status == OrderStatus.Cancelled;
    
    public bool IsNew => Status == OrderStatus.New;
    
    public bool IsCompleted => Status == OrderStatus.Completed;

    public void RefreshStatus()
    {
        OnPropertyChanged(nameof(Status));
        OnPropertyChanged(nameof(IsPaid));
        OnPropertyChanged(nameof(IsCancelled));
        OnPropertyChanged(nameof(IsNew));
        OnPropertyChanged(nameof(IsCompleted));
    }
}