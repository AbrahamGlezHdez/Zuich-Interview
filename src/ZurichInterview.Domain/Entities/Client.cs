namespace ZurichInterview.Domain.Entities;

public class Client
{
    public Guid Id { get; set; }
    public string IdentificationNumber { get; set; } = default!; // 10 dígitos
    public string Name { get; set; } = default!;
    public string MiddleName { get; set; } = default!;
    public string SurName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;

    public ICollection<Policy> Policies { get; set; } = new List<Policy>();
}