using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Domain.TestAggregate
{
    public interface ITaskRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<TaskEntity>> GetAsync();
        Task<TaskEntity> GetAsync(long taskId);
        TaskEntity Add(TaskEntity task);
        void Update(TaskEntity task);
        void DeleteTask(long id);
    }
}
