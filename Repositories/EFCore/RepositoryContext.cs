using Entities.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryContext :IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options):
            base(options)
        {
        }

        public DbSet<Text> Texts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // güvenlik 
           
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Text>()
        .HasOne(t => t.User)
        .WithMany(u => u.Texts)
        .HasForeignKey(t => t.UserId)
        .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
