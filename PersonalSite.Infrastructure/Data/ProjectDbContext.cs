using Microsoft.EntityFrameworkCore;
using PersonalSite.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalSite.Infrastructure.Data
{
    public class ProjectDbContext : DbContext
    {
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<ProjectTranslationEntity> ProjectTranslations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectTranslationEntity>()
                .HasIndex(t => new { t.ProjectId, t.Language })
                .IsUnique();

            modelBuilder.Entity<ProjectEntity>()
                .HasMany(p => p.Translations)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
