using System.Collections.Generic;
using Task_5.Models;

namespace Task_5.Services;

public interface IProductService
{
    IEnumerable<ProductItem> GetProducts();

    ProductItem CreateProduct();

    void DeleteProduct(ProductItem product);
}