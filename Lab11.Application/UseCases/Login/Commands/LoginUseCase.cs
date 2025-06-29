using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lab.Application.Common.Interfaces.Persistence;
using Lab11.Domain.Interfaces;
using Lab11.Domain.Interfaces.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Lab11.Infrastructure.Implements.Services;

public class LoginUseCase(
    IUnitOfWork _unitOfWork,
    IConfiguration _configuration
) : ILoginUseCase
{
    public async Task<string> LoginAsync(string username, string password)
    {
        var user = (await _unitOfWork.Users.FindAsync(u => u.Username == username)).FirstOrDefault();
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new Exception("Credenciales inválidas.");

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim(ClaimTypes.Email, user.Email ?? "")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}