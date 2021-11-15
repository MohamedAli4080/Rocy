
using System;
using Microsoft.EntityFrameworkCore;
using Rocy.Models;

namespace Rocy.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }
        public DbSet<Category> Category { get; set; }
        public DbSet<ApplicationType> ApplicationTypes { get; set; }


    }
}
