namespace PDP_Students.Application.Repositories;

public interface ICRUDRepository<T>
{
    Task<T> CreateAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int Id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
}
