using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Services.DataAccess.EntityFramework.Entityes;

namespace Services.DataAccess.EntityFramework
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Link> Links { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;UserId=root;Password=Qweewq123;database=usersdb3;");
        }
    }
}
