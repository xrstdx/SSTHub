﻿namespace SSTHub.Admin.Domain.Entities;

public class Customer
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } 

    public string LastName { get; set; } 

    public string Email { get; set; } 

    public string Phone { get; set; } 

    public string PasswordHash { get; set; } 
    public ICollection<Event> Events { get; set; }
}
