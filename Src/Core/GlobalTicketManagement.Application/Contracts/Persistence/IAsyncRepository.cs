using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTicketManagement.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> GetListAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
