using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tasks.Domain;
using Tasks.Domain.TestAggregate;

namespace Tasks.Infrastructure
{
    public class TasksContext : DbContext, IUnitOfWork
    {
        public DbSet<TaskEntity> Tasks { get; set; }

        public TasksContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TaskConfiguration());
            builder.Seed();
        }
    }
}
