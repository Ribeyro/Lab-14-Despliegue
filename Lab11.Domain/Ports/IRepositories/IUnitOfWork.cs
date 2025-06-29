using Lab11.Domain.Entities;

namespace Lab.Application.Common.Interfaces.Persistence;

// centraliza la peticion a la base de datos
public interface IUnitOfWork
{
    IGenericRepository<User> Users { get; }
    IGenericRepository<Role> Roles { get; }
    IGenericRepository<UserRole> UserRoles { get; }
    IGenericRepository<Ticket> Tickets { get; }
    IGenericRepository<Response> Responses { get; }

    Task<int> SaveChangesAsync();
}