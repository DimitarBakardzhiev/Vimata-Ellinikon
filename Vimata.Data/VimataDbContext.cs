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
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Medal> Medals { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseOption> ExerciseOptions { get; set; }
        public DbSet<AlternativeAnswer> AlternativeAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Lesson>()
                .Property(l => l.Title)
                .HasConversion<string>();

            modelBuilder.Entity<Medal>()
                .Property(m => m.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Exercise>()
                .HasMany(e => e.Options)
                .WithOne(o => o.Exercise)
                .HasForeignKey(o => o.ExerciseId);
                
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Medal>()
                .HasOne(m => m.Lesson)
                .WithMany(l => l.Medals)
                .HasForeignKey(m => m.LessonId);

            modelBuilder.Entity<Medal>()
                .HasOne(m => m.User)
                .WithMany(l => l.Medals)
                .HasForeignKey(m => m.UserId);

            modelBuilder.Entity<Medal>()
                .HasIndex(m => new { m.LessonId, m.UserId, m.Type }).IsUnique();
        }
    }
}
