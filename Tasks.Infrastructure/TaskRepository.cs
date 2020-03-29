using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public TaskEntity Add(TaskEntity task)
        {
            return _context.Tasks.Add(task).Entity;
        }

        public async Task<TaskEntity> GetAsync(long taskId)
        {
            var task = await _context
                                .Tasks
                                .Include(x => x.SubTasks)
                                .Include(x => x.ParentTask)
                                .FirstOrDefaultAsync(o => o.Id == taskId);
            if (task == null)
            {
                task = _context
                            .Tasks
                            .Local
                            .FirstOrDefault(o => o.Id == taskId);
            }

            return task;
        }

        public void Update(TaskEntity order)
        {
            _context.Entry(order).State = EntityState.Modified;
        }

        public async Task<IEnumerable<TaskEntity>> GetAsync()
        {
            return await _context
                                .Tasks
                                .Where(t => t.ParentTask == null)
                                .Include(x => x.SubTasks)
                                .Include(x => x.ParentTask)
                                .ToListAsync();
        }

        public void DeleteTask(long id)
        {
            var task = _context.Tasks.Find(id);
            _context.Tasks.Remove(task);
        }
    }
}
