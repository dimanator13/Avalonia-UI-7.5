using System;
using Task_5.Models;

namespace Task_5.ViewModels;

public class ClientItemViewModel : ViewModelBase
{
    private readonly ClientItem _model;

    public ClientItem Model => _model;

    public ClientItemViewModel(ClientItem model)
    {
        _model = model;
    }
    
    public string Name
    {
        get => _model.Name;
        set
        {
            if (_model.Name == value)
                return;

            _model.Name = value;
            OnPropertyChanged();
        }
    }
    
    public string Phone
    {
        get => _model.Phone;
        set
        {
            if (_model.Phone == value)
                return;

            _model.Phone = value;
            OnPropertyChanged();
        }
    }
    
    public string Email
    {
        get => _model.Email;
        set
        {
            if (_model.Email == value)
                return;

            _model.Email = value;
            OnPropertyChanged();
        }
    }
    
    public bool IsVip
    {
        get => _model.IsVip;
        set
        {
            if (_model.IsVip == value)
                return;

            _model.IsVip = value;
            OnPropertyChanged();
        }
    }
    
    public DateTimeOffset CreatedAt => _model.CreatedAt;
}