using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.API.DTO;

namespace Tasks.API.Application
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskModel>> GetTasksAsync(TaskFilter filter = null);
        Task<TaskModel> GetTaskAsync(long id);
        Task<TaskModel> CreateTaskAsync(TaskModel task);
        Task<TaskModel> UpdateTaskAsync(TaskModel task);
        Task DeleteTaskAsync(long id);
    }
}
