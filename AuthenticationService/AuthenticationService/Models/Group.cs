﻿namespace AuthenticationService.Models;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? System { get; set; }
    public string? Description { get; set; }
}