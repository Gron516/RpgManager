﻿public record class Person
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; }
    public string? Role { get; set; }
}