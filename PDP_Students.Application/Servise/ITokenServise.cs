using PDP_Students.Domain.Entities;
using PDP_Students.Domain.Models;

namespace PDP_Students.Application.Servise;

public interface ITokenServise
{
    Task<Token> GenerateTokenAsync(Student student);
    Task<Student> GetClaimFromExpiredTokenAsync(string token);
    Task<string> GenerateRefreshTokenAsync();
    Task<Token> NewTokenFromRefreshToken(Token token);
    string ComputeSha(string value);
}
