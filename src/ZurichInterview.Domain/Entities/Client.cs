﻿namespace ZurichInterview.Domain.Entities;

public class Client
{
    public int Id { get; set; }
    public string IdentificationNumber { get; set; } = default!; // 10 dígitos
    public string Name { get; set; } = default!;
    public string MiddleName { get; set; } = default!;
    public string SurName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Address { get; set; } = default!;

    public ICollection<Policy> Policies { get; set; } = new List<Policy>();
    
    public int? UsuarioId { get; set; }   // <-- Relación con Usuario
    public Usuario? Usuario { get; set; } 
}