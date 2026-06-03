using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Task_5.Models;
using Task_5.Services;

namespace Task_5.ViewModels;

public partial class ProductsViewModel : ViewModelBase
{
    private readonly IProductService _productService;
    
    [NotifyPropertyChangedFor(nameof(HasSelectedProduct))]
    [NotifyCanExecuteChangedFor(nameof(DeleteProductCommand))]
    [ObservableProperty] private ProductItemViewModel? _selectedProduct;

    public bool HasSelectedProduct => SelectedProduct is not null;

    [ObservableProperty] private string _newProductName = string.Empty;
    
    [ObservableProperty] private string _searchText = string.Empty;
    
    [ObservableProperty] private ProductCategory? _selectedCategoryFilter;
    
    public ObservableCollection<ProductItemViewModel> Products { get; } = new();
    
    public ObservableCollection<ProductItemViewModel> FilteredProducts { get; } = new();

    public ObservableCollection<ProductCategory> ProductCategories { get; } = new()
    {
        ProductCategory.Electronics,
        ProductCategory.Food,
        ProductCategory.Materials,
        ProductCategory.Clothes,
        ProductCategory.Other
    };
    
    [NotifyPropertyChangedFor(nameof(HasNewProductNameError))]
    [ObservableProperty] private string _newProductNameError = string.Empty;

    public bool HasNewProductNameError => !string.IsNullOrWhiteSpace(NewProductNameError);
    
    public event Action? ProductsChanged;

    public ProductsViewModel(IProductService productService)
    {
        _productService = productService;

        foreach (var product in _productService.GetProducts())
        {
            Products.Add(new ProductItemViewModel(product));
        }
        
        RefreshFilteredProducts();
    }

    [RelayCommand]
    private void AddProduct()
    {
        if (!ValidateNewProductName())
            return;
        
        var product = _productService.CreateProduct(NewProductName.Trim());
        var productViewModel = new ProductItemViewModel(product);
        
        Products.Add(productViewModel);
        
        NewProductName = string.Empty;
        SearchText = string.Empty;
        SelectedCategoryFilter = null;
        
        RefreshFilteredProducts();
        
        SelectedProduct = productViewModel;
        
        ProductsChanged?.Invoke();
    }

    [RelayCommand(CanExecute = nameof(HasSelectedProduct))]
    private void DeleteProduct()
    {
        if (SelectedProduct is null)
            return;
        
        _productService.DeleteProduct(SelectedProduct.Model);
        Products.Remove(SelectedProduct);

        SelectedProduct = null;
        
        RefreshFilteredProducts();
        
        ProductsChanged?.Invoke();
    }
    
    [RelayCommand]
    private void ResetCategoryFilter()
    {
        SelectedCategoryFilter = null;
    }
    
    private bool ValidateNewProductName()
    {
        if (string.IsNullOrWhiteSpace(NewProductName))
        {
            NewProductNameError = "Enter the product name";
            return false;
        }

        if (NewProductName.Trim().Length < 3)
        {
            NewProductNameError = "Name must have minimum 3 symbols";
            return false;
        }

        NewProductNameError = string.Empty;
        return true;
    }
    
    partial void OnNewProductNameChanged(string value)
    {
        if (HasNewProductNameError && value.Trim().Length >= 3)
        {
            NewProductNameError = string.Empty;
        }
    }
    
    private void RefreshFilteredProducts()
    {
        FilteredProducts.Clear();

        foreach (var product in Products)
        {
            bool matchesSearch =
                string.IsNullOrWhiteSpace(SearchText) ||
                product.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);

            bool matchesCategory =
                SelectedCategoryFilter is null ||
                product.Category == SelectedCategoryFilter;

            if (matchesSearch && matchesCategory)
            {
                FilteredProducts.Add(product);
            }
        }
    }
    
    partial void OnSearchTextChanged(string value)
    {
        RefreshFilteredProducts();
    }
    
    partial void OnSelectedCategoryFilterChanged(ProductCategory? value)
    {
        RefreshFilteredProducts();
    }
}