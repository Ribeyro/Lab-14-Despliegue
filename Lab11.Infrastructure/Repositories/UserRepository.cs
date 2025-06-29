using Lab.Application.Common.Interfaces.Persistence;
using Lab11.Domain.Entities;
using Lab11.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab11.Infrastructure.Implements;
//Aquí se usa ApplicationDbContext directamente, porque este repositorio está enfocado solo en User,
// y eso permite consultas más específicas que un genérico.

public class UserRepository (ApplicationDbContext _context): IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }
}