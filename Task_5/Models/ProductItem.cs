using System;

namespace Task_5.Models;

public class ProductItem
{
    public string Name { get; set; }
    public int Code { get; set; }
    public ProductCategory Category { get; set; }
    public int Price { get; set; }
    public int StockQuantity { get; set; }
    public int MinStockQuantity { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public ProductItem(string name, int code, ProductCategory category, int price, int stockQuantity, int minStockQuantity)
    {
        Name = name;
        Code = code;
        Category = category;
        Price = price;
        StockQuantity = stockQuantity;
        MinStockQuantity = minStockQuantity;
        CreatedAt = DateTimeOffset.Now;
    }
}