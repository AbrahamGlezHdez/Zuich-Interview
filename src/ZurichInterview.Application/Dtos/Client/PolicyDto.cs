using ZurichInterview.Domain.Entities.Enums;

namespace ZurichInterview.Application.Dtos.Client;

public class PolicyDto
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public PolicyType Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public decimal Amount { get; set; }
    public PolicyStatus Status { get; set; }
}