using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galpi.Models;
using Galpi.Models.User;
using Galpi.Models.Wiki;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Galpi.Data
{
    public class GalpiDbContext: IdentityDbContext<GalpiUser>
    {
        public GalpiDbContext(DbContextOptions<GalpiDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Article>()
                .HasIndex(a => a.Title)
                .IsUnique();
        }

        public DbSet<Article> Articles { get; set; }

        public GalpiConfig Config { get; set; }
    }
}
