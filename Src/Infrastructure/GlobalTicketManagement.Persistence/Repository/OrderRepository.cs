using GlobalTicketManagement.Application.Contracts.Persistence;
using GlobalTicketManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTicketManagement.Persistence.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Order>> GetPagedOrdersForMonth(DateTime date, int page, int size)
        {
            var orders=_dbContext.Orders
                .Where(x=>x.OrderPlaced.Month==date.Month && x.OrderPlaced.Year==date.Year)
                .Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();

            return orders;
        }

        public async Task<int> GetTotalCountOfOrdersForMonth(DateTime date)
        {
            var total = await _dbContext.Orders.CountAsync(x => x.OrderPlaced.Month == date.Month && x.OrderPlaced.Year == date.Year);
            return total;
        }
    }
}
