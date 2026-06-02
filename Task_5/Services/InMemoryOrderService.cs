using System;
using System.Collections.Generic;
using Task_5.Models;

namespace Task_5.Services;

public class InMemoryOrderService : IOrderService
{
    private readonly List<OrderItem> _orders;

    public InMemoryOrderService(ClientItem client, IEnumerable<OrderItem>? orders = null)
    {
        _orders = orders is null
            ? CreateDefaultOrders(client)
            : new List<OrderItem>(orders);
    }

    public IEnumerable<OrderItem> GetOrders()
    {
        return _orders;
    }

    public OrderItem CreateOrder(string clientName)
    {
        var order = new OrderItem(
            new Random().Next(100),
            clientName,
            DateTimeOffset.Now,
            0,
            OrderStatus.New);
        
        _orders.Add(order);

        return order;
    }

    public void DeleteOrder(OrderItem order)
    {
        _orders.Remove(order);
    }

    public void ChangeOrderStatus(OrderItem order, OrderStatus status)
    {
        order.Status = status;
    }

    private static List<OrderItem> CreateDefaultOrders(ClientItem client)
    {
        return new List<OrderItem>
        {
            new OrderItem(
                new Random().Next(100),
                client.Name,
                DateTimeOffset.Now,
                0,
                OrderStatus.New)
        };
    }
}