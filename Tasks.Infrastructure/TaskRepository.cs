using System;
using System.Collections.Generic;
using System.Text;
using Tasks.Domain;
using Tasks.Domain.TestAggregate;

namespace Tasks.Infrastructure
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TasksContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public TaskRepository(TasksContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
