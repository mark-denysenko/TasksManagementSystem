using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.API.DTO;
using Tasks.Domain.TestAggregate;

namespace Tasks.API.Application.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository taskRepository;
        private readonly ILogger<TaskService> logger;

        public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
        {
            this.taskRepository = taskRepository;
            this.logger = logger;
        }

        public async Task<TaskModel> CreateTaskAsync(TaskModel task)
        {
            var entity = taskRepository.Add(ToDomainEntity(task));
            await taskRepository.UnitOfWork.SaveChangesAsync();

            return ToTaskModel(entity);
        }

        public async Task DeleteTaskAsync(long id)
        {
            taskRepository.DeleteTask(id);
            await taskRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task<TaskModel> GetTaskAsync(long id)
        {
            var task = await taskRepository.GetAsync(id);
            return ToTaskModel(task);
        }

        public async Task<IEnumerable<TaskModel>> GetTasksAsync()
        {
            var tasks = await taskRepository.GetAsync();

            return tasks.Select(ToTaskModel);
        }

        public Task<TaskModel> UpdateTaskAsync(TaskModel task)
        {
            throw new NotImplementedException();
        }

        private TaskEntity ToDomainEntity(TaskModel task)
        {
            return new TaskEntity();
        }

        private TaskModel ToTaskModel(TaskEntity task)
        {
            return new TaskModel();
        }
    }
}
