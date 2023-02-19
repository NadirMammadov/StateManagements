namespace StateManagements.Session_.Interfaces
{
    public interface IRepository<T> where T: class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task Remove(T entity);
    }
}
