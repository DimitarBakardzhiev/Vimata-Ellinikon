namespace Vimata.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using Vimata.Data.Models;

    public class VimataDbContext : DbContext
    {
        public VimataDbContext(DbContextOptions<VimataDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
