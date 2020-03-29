using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tasks.Domain.TestAggregate;

namespace Tasks.Infrastructure
{
    internal static class ContextSeed
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<TaskEntity>().HasData(
                    new TaskEntity(1, "Stay at home", "Sitting at home", new DateTime(2020, 3, 12), new DateTime(2020, 4, 12), TaskStatus.Planned),
                    new TaskEntity(2, "Learn GO", "Try new programming language", new DateTime(2020, 2, 2), new DateTime(2021, 6, 2), TaskStatus.InProgress),
                    new TaskEntity(3, "Run every day", "Running in the morning", new DateTime(2020, 1, 1), new DateTime(2020, 2, 5), TaskStatus.Completed),
                    new TaskEntity(4, "Make a business", "Create own business", new DateTime(2019, 5, 23), new DateTime(2025, 9, 8), TaskStatus.InProgress));

            // subtasks
            builder.Entity<TaskEntity>().HasData(
                    new
                    {
                        Id = 5L,
                        ParentTaskId = 1L,
                        Name = "Read a book",
                        Description = "CLR via C#",
                        StartDate = new DateTime(2020, 3, 12),
                        FinishDate = new DateTime(2020, 4, 12),
                        TaskStatus = TaskStatus.InProgress
                    },
                    new
                    {
                        Id = 6L,
                        ParentTaskId = 1L,
                        Name = "Play computer",
                        Description = "Doom Eternal",
                        StartDate = new DateTime(2020, 3, 12),
                        FinishDate = new DateTime(2020, 4, 12),
                        TaskStatus = TaskStatus.Completed
                    },

                    new
                    {
                        Id = 7L,
                        ParentTaskId = 2L,
                        Name = "Learn Goroutines",
                        Description = "CLR via C#",
                        StartDate = new DateTime(2020, 2, 2),
                        FinishDate = new DateTime(2021, 6, 2),
                        TaskStatus = TaskStatus.Planned
                    },
                    new
                    {
                        Id = 9L,
                        ParentTaskId = 2L,
                        Name = "Learn Multithreading",
                        Description = "Doom Eternal",
                        StartDate = new DateTime(2020, 2, 2),
                        FinishDate = new DateTime(2021, 6, 2),
                        TaskStatus = TaskStatus.Completed
                    },

                    new
                    {
                        Id = 10L,
                        ParentTaskId = 3L,
                        Name = "Buy shoes",
                        Description = "Need new shoes",
                        StartDate = new DateTime(2020, 1, 1),
                        FinishDate = new DateTime(2020, 2, 5),
                        TaskStatus = TaskStatus.Completed
                    },
                    new
                    {
                        Id = 11L,
                        ParentTaskId = 3L,
                        Name = "Run",
                        Description = "Just run",
                        StartDate = new DateTime(2020, 1, 1),
                        FinishDate = new DateTime(2020, 2, 5),
                        TaskStatus = TaskStatus.Completed
                    }
                );
        }
    }
}
