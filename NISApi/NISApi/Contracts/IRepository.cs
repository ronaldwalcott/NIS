using NISApi.Data.Entity.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NISApi.Contracts
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        Task<long> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(long id, UserData userData);
//        Task<bool> DeleteAsync(T entity);
        Task<bool> ExistAsync(long id);
    }
}
