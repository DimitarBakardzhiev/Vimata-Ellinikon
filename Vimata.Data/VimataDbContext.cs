namespace Vimata.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using Vimata.Data.Models;

    public class VimataDbContext : DbContext
    {
        public VimataDbContext(DbContextOptions<VimataDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Answer>().HasOne(a => a.Exercise).WithMany(e => e.Answers);
            modelBuilder.Entity<Exercise>().HasOne(e => e.Topic).WithMany(t => t.Exercises);

            base.OnModelCreating(modelBuilder);
        }
    }
}
