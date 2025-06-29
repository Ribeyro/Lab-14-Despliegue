using Lab11.Domain.Entities;

namespace Lab.Application.Common.Interfaces.Persistence;

// Esta interfaz representa el contrato que la capa de dominio y aplicación usará,
// sin depender de ninguna implementación concreta.

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email);
    Task AddAsync(User user);
    
}