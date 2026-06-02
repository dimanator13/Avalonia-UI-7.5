using System.Collections.Generic;
using Task_5.Models;

namespace Task_5.Services;

public interface IOrderService
{
    IEnumerable<OrderItem> GetOrders();

    OrderItem CreateOrder(string clientName);

    void DeleteOrder(OrderItem client);
    
    void ChangeOrderStatus(OrderItem order, OrderStatus status);
}