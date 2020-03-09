﻿namespace Vimata.Data
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
        public DbSet<MedalUserLesson> MedalsUsersLessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Lesson>()
                .Property(l => l.LessonTitle)
                .HasConversion<string>();

            modelBuilder.Entity<Medal>()
                .Property(m => m.Type)
                .HasConversion<string>();

            this.MedalsSeed(modelBuilder);

            modelBuilder.Entity<MedalUserLesson>()
                .HasKey(m => new { m.MedalId, m.UserId, m.LessonId });

            modelBuilder.Entity<MedalUserLesson>()
                .HasOne(m => m.Medal)
                .WithMany(m => m.UserLesson)
                .HasForeignKey(m => m.MedalId);

            modelBuilder.Entity<MedalUserLesson>()
                .HasOne(u => u.User)
                .WithMany(u => u.MedalLesson)
                .HasForeignKey(u => u.UserId);
                
            base.OnModelCreating(modelBuilder);
        }

        private void MedalsSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medal>().HasData(
                new Medal() { Id = 1, CreatedDate = DateTime.Now, Type = Medal.MedalType.Gold },
                new Medal() { Id = 2, CreatedDate = DateTime.Now, Type = Medal.MedalType.Silver },
                new Medal() { Id = 3, CreatedDate = DateTime.Now, Type = Medal.MedalType.Bronze });
        }
    }
}
