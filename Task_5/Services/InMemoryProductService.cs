using System;
using System.Collections.Generic;
using Task_5.Models;

namespace Task_5.Services;

public class InMemoryProductService : IProductService
{
    private readonly List<ProductItem> _products;
    
    public InMemoryProductService(IEnumerable<ProductItem>? products = null)
    {
        _products = products is null
            ? CreateDefaultProducts()
            : new List<ProductItem>(products);
    }

    public IEnumerable<ProductItem> GetProducts()
    {
        return _products;
    }

    public ProductItem CreateProduct()
    {
        var product = new ProductItem(
            "New product",
            new Random().Next(100),
            ProductCategory.Other,
            0,
            0,
            0);
        
        _products.Add(product);

        return product;
    }

    public void DeleteProduct(ProductItem product)
    {
        _products.Remove(product);
    }

    private static List<ProductItem> CreateDefaultProducts()
    {
        return new List<ProductItem>
        {
            new ProductItem(
                "New product",
                new Random().Next(100),
                ProductCategory.Other,
                0,
                0,
                0)
        };
    }
}