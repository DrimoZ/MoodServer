﻿namespace Infrastructure.EntityFramework.DbEntities;

public class DbUser
{
    public string Id { get; set; }
    public string Mail { get; set; }
    public string Login { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public int Role { get; set; }
    public string? Title { get; set; }
    public string AccountId { get; set; }
    public bool isDeleted { get; set; }
}