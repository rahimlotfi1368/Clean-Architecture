﻿using GlobalTicketManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTicketManagement.Identity
{
    public class IdentityDbContext:IdentityDbContext<ApplicationUser>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options):base(options) 
        {
                
        }
    }
}
