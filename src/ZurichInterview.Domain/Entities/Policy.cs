using ZurichInterview.Domain.Entities.Enums;

namespace ZurichInterview.Domain.Entities;

public class Policy
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public PolicyType Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public decimal Amount { get; set; }
    public PolicyStatus Status { get; set; }

    public Client Client { get; set; } = default!;
}