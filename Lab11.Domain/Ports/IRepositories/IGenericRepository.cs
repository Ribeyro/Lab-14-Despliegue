using System.Linq.Expressions;
using Lab11.Domain.Entities;

namespace Lab.Application.Common.Interfaces.Persistence;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(object id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task RemoveAsync(T entity);
    
    Task<List<User>> GetAllWithRolesAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
}
