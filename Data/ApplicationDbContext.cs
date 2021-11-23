
using System;
using Microsoft.EntityFrameworkCore;
using Rocy.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Rocy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }
        public DbSet<Category> Category { get; set; }
        public DbSet<ApplicationType> ApplicationTypes { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        
        
        
        

    }
}
