using GlobalTicketManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTicketManagement.Application.Contracts.Persistence
{
    public interface ICategoryRepository:IAsyncRepository<Category>
    {
    }
}
