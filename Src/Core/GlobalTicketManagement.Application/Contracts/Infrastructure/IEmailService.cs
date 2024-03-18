using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalTicketManagement.Application.Models.Email;

namespace GlobalTicketManagement.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Email email);
    }
}
