namespace ZurichInterview.Application.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(string userId, string email, string role);
}