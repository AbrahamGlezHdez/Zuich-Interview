namespace ZurichInterview.Application.Dtos.Client;

public class ClientDto
{
    public Guid Id { get; set; }
    public string IdentificationNumber { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string MiddleName { get; set; } = default!;
    public string SurName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
}