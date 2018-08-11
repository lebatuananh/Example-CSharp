using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityFrameworkExample.Entity.EF
{
    using System.IO;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions contextOptions) : base(contextOptions)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
        {
            public AppDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(
                    @"Server=.;Database=IdentityTest;Trusted_Connection=True;MultipleActiveResultSets=true");
                return new AppDbContext(optionsBuilder.Options);
            }
        }
    }
}
