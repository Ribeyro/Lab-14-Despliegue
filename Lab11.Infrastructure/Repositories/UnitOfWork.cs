using Lab.Application.Common.Interfaces.Persistence;
using Lab11.Domain.Entities;
using Lab11.Domain.Interfaces;
using Lab11.Infrastructure.Persistence.Data;

namespace Lab11.Infrastructure.Implements.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Users = new GenericRepository<User>(_context);
        Roles = new GenericRepository<Role>(_context); 
        UserRoles = new GenericRepository<UserRole>(_context);
        Tickets = new GenericRepository<Ticket>(_context);
        Responses = new GenericRepository<Response>(_context);
    }

    public IGenericRepository<User> Users { get; }
    public IGenericRepository<Role> Roles { get; } 
    public IGenericRepository<UserRole> UserRoles { get; }
    public IGenericRepository<Ticket> Tickets { get; }
    public IGenericRepository<Response> Responses { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}