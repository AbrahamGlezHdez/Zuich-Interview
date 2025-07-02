using ZurichInterview.Domain.Entities.Enums;

namespace ZurichInterview.Domain.Entities;

public class Policy
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public PolicyType Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public decimal Amount { get; set; }
    public PolicyStatus Status { get; set; } = PolicyStatus.Active;

    public Client Client { get; set; } = default!;
}