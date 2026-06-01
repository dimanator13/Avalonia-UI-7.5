using System;

namespace Task_5.Models;

public class ClientItem
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public bool IsVip { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public ClientItem(string name, string phone, string email, bool isVip)
    {
        Name = name;
        Phone = phone;
        Email = email;
        IsVip = isVip;
        CreatedAt = DateTimeOffset.Now;
    }
}