using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PDP_Students.Application.Servise;
using PDP_Students.Domain.Entities;
using PDP_Students.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PDP_Students.Infrasturucture.Servises;

public class TokenServise : ITokenServise
{
    private readonly IConfiguration _configuration;

    public TokenServise(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> GenerateRefreshTokenAsync()
    {
        return ComputeSha((DateTime.Now.ToString() + "myKey"));
    }

    public async Task<Token> GenerateTokenAsync(Student student)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, student.FullName),
            new Claim("Id", student.Id.ToString())
        };
        foreach(var role in student.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        double lifeTime = double.Parse(_configuration["JWT:AccesTokenLifeTimeInMinute"]);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddMinutes(lifeTime),
            signingCredentials: credentials,
            claims: claims
            );

        string accesToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new()
        {
            AccesToken = accesToken,
            RefreshToken = await GenerateRefreshTokenAsync(),
        };
    }

    public string ComputeSha(string value)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] getbytes = Encoding.UTF8.GetBytes(value);
        byte[] hash = sha256.ComputeHash(getbytes);

        StringBuilder builder = new StringBuilder();
        for(int i=0; i<hash.Length; i++)
        {
            builder.Append(hash[i].ToString("x2"));
        }
        return builder.ToString();
    }

    public Task<Token> NewTokenFromRefreshToken(Token token)
    {
        throw new NotImplementedException();
    }

    public async Task<Student> GetClaimFromExpiredTokenAsync(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token);
        var claims = (jsonToken as JwtSecurityToken)?.Claims;
        var userClaims = claims.ToArray();

        return new()
        {
            Id = int.Parse(userClaims.FirstOrDefault(x => x.Type.Equals("Id")).Value),
            FullName = userClaims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value
        };
    }
}
