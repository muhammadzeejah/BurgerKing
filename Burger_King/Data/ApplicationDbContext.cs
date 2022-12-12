using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Burger_King.Models;

namespace Burger_King.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Burger_King.Models.Add_on> Add_on { get; set; }
        public DbSet<Burger_King.Models.Sauces> Sauces { get; set; }
        public DbSet<Burger_King.Models.Burgers> Burgers { get; set; }
    }
}
