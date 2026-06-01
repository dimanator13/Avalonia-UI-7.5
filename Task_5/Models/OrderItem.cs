using System;

namespace Task_5.Models;

public class OrderItem
{
    public int Number { get; set; }
    public string ClientName { get; set; }
    public DateTimeOffset Date { get; set; }
    public int Amount { get; set; }
    public OrderStatus Status { get; set; }

    public OrderItem(int number, string clientName, DateTimeOffset date, int amount, OrderStatus status)
    {
        Number = number;
        ClientName = clientName;
        Date = date;
        Amount = amount;
        Status = status;
    }
}