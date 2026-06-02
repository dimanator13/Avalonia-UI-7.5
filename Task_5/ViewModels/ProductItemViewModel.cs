using System;
using Task_5.Models;

namespace Task_5.ViewModels;

public class ProductItemViewModel : ViewModelBase
{
    private readonly ProductItem _model;

    public ProductItem Model => _model;

    public ProductItemViewModel(ProductItem model)
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
    
    public int Code
    {
        get => _model.Code;
        set
        {
            if (_model.Code == value)
                return;

            _model.Code = value;
            OnPropertyChanged();
        }
    }
    
    public ProductCategory Category
    {
        get => _model.Category;
        set
        {
            if (_model.Category == value)
                return;

            _model.Category = value;
            OnPropertyChanged();
        }
    }
    
    public int Price
    {
        get => _model.Price;
        set
        {
            if (_model.Price == value)
                return;

            _model.Price = value;
            OnPropertyChanged();
        }
    }
    
    public int StockQuantity
    {
        get => _model.StockQuantity;
        set
        {
            if (_model.StockQuantity == value)
                return;

            _model.StockQuantity = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsLowStock));
        }
    }
    
    public int MinStockQuantity
    {
        get => _model.MinStockQuantity;
        set
        {
            if (_model.MinStockQuantity == value)
                return;

            _model.MinStockQuantity = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsLowStock));
        }
    }

    public DateTimeOffset CreatedAt => _model.CreatedAt;

    public bool IsLowStock => StockQuantity <= MinStockQuantity;
}