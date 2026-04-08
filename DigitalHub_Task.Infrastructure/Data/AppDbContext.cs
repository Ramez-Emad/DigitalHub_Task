using DigitalHub_Task.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalHub_Task.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> _options) : DbContext(_options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }

    public DbSet<Enrollment> Enrollments { get; set; }

    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data for testing
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Ramez Emad", Email = "Ramez@domain.com" },
            new User { Id = 2, Name = "Ali Ahmed", Email = "Ali@future.com" }
        );

        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, Title = "Backend" },
            new Course { Id = 2, Title = "Frontend" }
        );
    }
}
